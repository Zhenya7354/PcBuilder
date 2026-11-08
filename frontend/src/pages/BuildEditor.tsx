import { useEffect, useMemo, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import {
  buildEntityToRequest,
  createBuild,
  getBuild,
  updateBuild,
} from '../api/builds'
import { formatPrice, listParts } from '../api/catalog'
import {
  isCompatibilityError,
  type BuildRequest,
  type CompatibilityIssue,
  type ComponentBase,
} from '../api/types'
import { CompatibilityIssues } from '../components/CompatibilityIssues'
import {
  BUILD_SLOTS,
  PartPicker,
  type SlotConfig,
} from '../components/PartPicker'
import { ErrorState, LoadingState } from '../components/StatusBlocks'

type PartCache = Partial<Record<keyof BuildRequest, ComponentBase | null>>

function emptyDraft(): BuildRequest {
  return { name: '' }
}

export function BuildEditor() {
  const { id } = useParams()
  const isNew = !id || id === 'new'
  const buildId = !isNew ? Number(id) : NaN
  const navigate = useNavigate()
  const queryClient = useQueryClient()

  const [draft, setDraft] = useState<BuildRequest>(emptyDraft)
  const [partCache, setPartCache] = useState<PartCache>({})
  const [pickerSlot, setPickerSlot] = useState<SlotConfig | null>(null)
  const [compatIssues, setCompatIssues] = useState<CompatibilityIssue[]>([])
  const [compatMessage, setCompatMessage] = useState<string | undefined>()
  const [formError, setFormError] = useState<string | null>(null)

  const existing = useQuery({
    queryKey: ['builds', buildId],
    queryFn: () => getBuild(buildId),
    enabled: !isNew && Number.isFinite(buildId),
  })

  useEffect(() => {
    if (!existing.data) return
    setDraft(buildEntityToRequest(existing.data))
    setPartCache({
      cpuId: existing.data.cpu ?? null,
      cpuCoolerId: existing.data.cpuCooler ?? null,
      gpuId: existing.data.gpu ?? null,
      ramId: existing.data.ram ?? null,
      hardDriveId: existing.data.hardDrive ?? null,
      motherboardId: existing.data.motherboard ?? null,
      psuId: existing.data.psu ?? null,
      caseId: existing.data.pcCase ?? null,
      monitorId: existing.data.monitor ?? null,
    })
  }, [existing.data])

  // Hydrate names/brands when nested entities are missing or brand is null
  useEffect(() => {
    if (!existing.data) return
    const build = existing.data
    const nestedForSlot = (slot: SlotConfig): ComponentBase | null | undefined => {
      if (slot.key === 'caseId') return build.pcCase
      if (slot.key === 'cpuId') return build.cpu
      if (slot.key === 'cpuCoolerId') return build.cpuCooler
      if (slot.key === 'gpuId') return build.gpu
      if (slot.key === 'ramId') return build.ram
      if (slot.key === 'hardDriveId') return build.hardDrive
      if (slot.key === 'motherboardId') return build.motherboard
      if (slot.key === 'psuId') return build.psu
      return build.monitor
    }
    const slotsNeedingLookup = BUILD_SLOTS.filter((slot) => {
      const idVal = build[slot.key]
      if (idVal == null) return false
      const nested = nestedForSlot(slot)
      // Fetch catalog when nested missing, or present but brand not hydrated
      return !nested || nested.brand == null
    })
    if (!slotsNeedingLookup.length) return

    let cancelled = false
    void (async () => {
      const updates: PartCache = {}
      for (const slot of slotsNeedingLookup) {
        const idVal = build[slot.key]
        if (idVal == null) continue
        try {
          const parts = await listParts(slot.catalogType)
          const fromCatalog = parts.find((p) => p.id === idVal)
          if (!fromCatalog) continue
          const nested = nestedForSlot(slot)
          // Prefer catalog brand/name when nested lacks brand; keep nested otherwise merged
          updates[slot.key] = nested
            ? {
                ...nested,
                name: nested.name || fromCatalog.name,
                brand: nested.brand ?? fromCatalog.brand,
                price: nested.price ?? fromCatalog.price,
                currency: nested.currency ?? fromCatalog.currency,
              }
            : fromCatalog
        } catch {
          /* ignore hydrate errors */
        }
      }
      if (!cancelled && Object.keys(updates).length) {
        setPartCache((prev) => ({ ...prev, ...updates }))
      }
    })()
    return () => {
      cancelled = true
    }
  }, [existing.data])

  const save = useMutation({
    mutationFn: async () => {
      const body: BuildRequest = {
        ...draft,
        name: draft.name?.trim() || undefined,
      }
      if (isNew) return createBuild(body)
      return updateBuild(buildId, body)
    },
    onSuccess: (saved) => {
      setCompatIssues([])
      setCompatMessage(undefined)
      setFormError(null)
      void queryClient.invalidateQueries({ queryKey: ['builds'] })
      if (isNew) {
        navigate(`/builds/${saved.id}`, { replace: true })
      } else {
        void queryClient.invalidateQueries({ queryKey: ['builds', buildId] })
      }
    },
    onError: (err) => {
      if (isCompatibilityError(err)) {
        setCompatMessage(err.body.message ?? err.message)
        setCompatIssues(err.body.issues)
        setFormError(null)
        return
      }
      setCompatIssues([])
      setFormError(err instanceof Error ? err.message : 'Save failed')
    },
  })

  const filledCount = useMemo(
    () => BUILD_SLOTS.filter((s) => draft[s.key] != null).length,
    [draft],
  )

  if (!isNew && !Number.isFinite(buildId)) {
    return <ErrorState title="Invalid build id" />
  }

  if (!isNew && existing.isLoading) {
    return <LoadingState title="Loading build…" />
  }

  if (!isNew && existing.isError) {
    return (
      <div className="space-y-3">
        <ErrorState
          title="Could not load build"
          detail={
            existing.error instanceof Error
              ? existing.error.message
              : 'Unknown error'
          }
        />
        <Link to="/builds" className="text-sm text-accent hover:underline">
          Back to builds
        </Link>
      </div>
    )
  }

  return (
    <div>
      <Link to="/builds" className="text-sm text-steel-400 hover:text-accent">
        ← My builds
      </Link>

      <header className="mt-4 mb-8">
        <h1 className="text-3xl font-semibold tracking-tight text-steel-100">
          {isNew ? 'New build' : draft.name?.trim() || `Build #${buildId}`}
        </h1>
        <p className="mt-2 text-sm text-steel-400">
          Pick parts for each slot. Compatibility is checked when you save.
        </p>
      </header>

      <div className="mb-6">
        <label htmlFor="build-name" className="mb-1 block text-sm text-steel-300">
          Build name
        </label>
        <input
          id="build-name"
          type="text"
          value={draft.name ?? ''}
          onChange={(e) =>
            setDraft((d) => ({ ...d, name: e.target.value }))
          }
          placeholder="e.g. 1440p gaming"
          className="w-full max-w-md border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none focus:border-accent"
        />
      </div>

      <p className="mb-3 font-mono text-xs text-steel-500">
        {filledCount} / {BUILD_SLOTS.length} slots filled
      </p>

      <ul className="mb-8 space-y-2">
        {BUILD_SLOTS.map((slot) => {
          const selectedId = draft[slot.key] as number | null | undefined
          const part = partCache[slot.key]
          return (
            <li key={slot.key}>
              <button
                type="button"
                onClick={() => setPickerSlot(slot)}
                className="flex w-full items-center justify-between gap-3 border border-steel-800 bg-steel-900/60 px-4 py-3 text-left transition-colors hover:border-steel-600"
              >
                <span>
                  <span className="block font-mono text-xs uppercase tracking-wider text-steel-500">
                    {slot.label}
                  </span>
                  <span className="mt-0.5 block text-sm text-steel-100">
                    {part?.name ??
                      (selectedId != null
                        ? `Part #${selectedId}`
                        : 'Empty — click to pick')}
                  </span>
                  {part?.brand?.name ? (
                    <span className="text-xs text-steel-400">
                      {part.brand.name}
                    </span>
                  ) : null}
                </span>
                <span className="shrink-0 font-mono text-xs text-accent">
                  {part
                    ? formatPrice(part.price, part.currency)
                    : selectedId != null
                      ? 'Selected'
                      : 'Choose'}
                </span>
              </button>
            </li>
          )
        })}
      </ul>

      {compatIssues.length > 0 ? (
        <div className="mb-6">
          <CompatibilityIssues issues={compatIssues} message={compatMessage} />
        </div>
      ) : null}

      {formError ? (
        <p className="mb-4 border border-danger/40 bg-danger-soft px-3 py-2 text-sm text-danger" role="alert">
          {formError}
        </p>
      ) : null}

      {!isNew && save.isSuccess && compatIssues.length === 0 && !formError ? (
        <p className="mb-4 text-sm text-ok" role="status">
          Build saved.
        </p>
      ) : null}

      <div className="flex flex-wrap gap-3">
        <button
          type="button"
          disabled={save.isPending}
          onClick={() => save.mutate()}
          className="bg-accent px-5 py-2.5 text-sm font-semibold text-steel-950 hover:bg-accent-dim disabled:opacity-60"
        >
          {save.isPending ? 'Saving…' : isNew ? 'Create build' : 'Save changes'}
        </button>
        <Link
          to="/builds"
          className="border border-steel-600 px-5 py-2.5 text-sm text-steel-200 hover:border-steel-400"
        >
          Cancel
        </Link>
      </div>

      <PartPicker
        open={!!pickerSlot}
        slot={pickerSlot}
        selectedId={
          pickerSlot
            ? (draft[pickerSlot.key] as number | null | undefined)
            : null
        }
        onClose={() => setPickerSlot(null)}
        onSelect={(partId, part) => {
          if (!pickerSlot) return
          setDraft((d) => ({ ...d, [pickerSlot.key]: partId }))
          setPartCache((c) => ({ ...c, [pickerSlot.key]: part }))
          setPickerSlot(null)
        }}
        onClear={() => {
          if (!pickerSlot) return
          setDraft((d) => ({ ...d, [pickerSlot.key]: null }))
          setPartCache((c) => ({ ...c, [pickerSlot.key]: null }))
          setPickerSlot(null)
        }}
      />
    </div>
  )
}
