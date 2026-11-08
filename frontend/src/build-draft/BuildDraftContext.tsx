import {
  createContext,
  useCallback,
  useContext,
  useMemo,
  useState,
  type ReactNode,
} from 'react'
import type { BuildRequest, BuildSlotKey } from '../api/types'
import {
  clearPersistedDraft,
  emptyDraft,
  filledSlotCount,
  loadDraft,
  persistDraft,
  toBuildRequest,
  type BuildDraftState,
  type DraftPartInfo,
} from './storage'

interface BuildDraftContextValue {
  draft: BuildDraftState
  filledCount: number
  setName: (name: string) => void
  setSlot: (key: BuildSlotKey, part: DraftPartInfo) => void
  clearSlot: (key: BuildSlotKey) => void
  clearAll: () => void
  setPendingSave: (pending: boolean) => void
  toBuildRequest: () => BuildRequest
}

const BuildDraftContext = createContext<BuildDraftContextValue | null>(null)

function write(next: BuildDraftState) {
  persistDraft(next)
  return next
}

export function BuildDraftProvider({ children }: { children: ReactNode }) {
  const [draft, setDraft] = useState<BuildDraftState>(() => loadDraft())

  const setName = useCallback((name: string) => {
    setDraft((prev) => write({ ...prev, name }))
  }, [])

  const setSlot = useCallback((key: BuildSlotKey, part: DraftPartInfo) => {
    setDraft((prev) =>
      write({
        ...prev,
        slots: { ...prev.slots, [key]: part },
      }),
    )
  }, [])

  const clearSlot = useCallback((key: BuildSlotKey) => {
    setDraft((prev) => {
      const slots = { ...prev.slots }
      delete slots[key]
      return write({ ...prev, slots })
    })
  }, [])

  const clearAll = useCallback(() => {
    clearPersistedDraft()
    setDraft(emptyDraft())
  }, [])

  const setPendingSave = useCallback((pendingSave: boolean) => {
    setDraft((prev) => write({ ...prev, pendingSave }))
  }, [])

  const value = useMemo<BuildDraftContextValue>(
    () => ({
      draft,
      filledCount: filledSlotCount(draft),
      setName,
      setSlot,
      clearSlot,
      clearAll,
      setPendingSave,
      toBuildRequest: () => toBuildRequest(draft),
    }),
    [draft, setName, setSlot, clearSlot, clearAll, setPendingSave],
  )

  return (
    <BuildDraftContext.Provider value={value}>
      {children}
    </BuildDraftContext.Provider>
  )
}

export function useBuildDraft(): BuildDraftContextValue {
  const ctx = useContext(BuildDraftContext)
  if (!ctx) throw new Error('useBuildDraft must be used within BuildDraftProvider')
  return ctx
}
