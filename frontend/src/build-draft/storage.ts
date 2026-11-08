import type { BuildRequest, BuildSlotKey } from '../api/types'

export interface DraftPartInfo {
  id: number
  name: string
  brandName?: string
  price?: number | null
  currency?: string | null
}

export type DraftSlots = Partial<Record<BuildSlotKey, DraftPartInfo>>

export interface BuildDraftState {
  name: string
  slots: DraftSlots
  /** When true, create the build after the next successful login. */
  pendingSave: boolean
}

export const DRAFT_STORAGE_KEY = 'pcbuilder.buildDraft'

export function emptyDraft(): BuildDraftState {
  return { name: '', slots: {}, pendingSave: false }
}

export function loadDraft(): BuildDraftState {
  try {
    const raw = localStorage.getItem(DRAFT_STORAGE_KEY)
    if (!raw) return emptyDraft()
    const parsed = JSON.parse(raw) as Partial<BuildDraftState>
    return {
      name: typeof parsed.name === 'string' ? parsed.name : '',
      slots:
        parsed.slots && typeof parsed.slots === 'object' ? parsed.slots : {},
      pendingSave: !!parsed.pendingSave,
    }
  } catch {
    return emptyDraft()
  }
}

export function persistDraft(draft: BuildDraftState) {
  localStorage.setItem(DRAFT_STORAGE_KEY, JSON.stringify(draft))
}

export function clearPersistedDraft() {
  localStorage.removeItem(DRAFT_STORAGE_KEY)
}

export function toBuildRequest(draft: BuildDraftState): BuildRequest {
  const req: BuildRequest = {}
  const name = draft.name.trim()
  if (name) req.name = name

  for (const [key, part] of Object.entries(draft.slots) as [
    BuildSlotKey,
    DraftPartInfo | undefined,
  ][]) {
    if (part?.id != null) {
      req[key] = part.id
    }
  }
  return req
}

export function filledSlotCount(draft: BuildDraftState): number {
  return Object.values(draft.slots).filter((p) => p?.id != null).length
}
