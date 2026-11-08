import { useEffect, useMemo, useRef, useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import {
  CATALOG_LABELS,
  filterPartsByQuery,
  formatPrice,
  listParts,
} from '../api/catalog'
import type { ComponentBase } from '../api/types'
import {
  BUILD_SLOTS,
  type SlotConfig,
} from '../build-draft'
import { EmptyState, ErrorState, LoadingState } from './StatusBlocks'

export { BUILD_SLOTS, type SlotConfig }

interface PartPickerProps {
  open: boolean
  slot: SlotConfig | null
  selectedId?: number | null
  onClose: () => void
  onSelect: (id: number, part: ComponentBase) => void
  onClear: () => void
}

export function PartPicker({
  open,
  slot,
  selectedId,
  onClose,
  onSelect,
  onClear,
}: PartPickerProps) {
  const [query, setQuery] = useState('')
  const searchRef = useRef<HTMLInputElement>(null)

  const { data, isLoading, isError, error, refetch } = useQuery({
    queryKey: ['catalog', slot?.catalogType],
    queryFn: () => listParts(slot!.catalogType),
    enabled: open && !!slot,
  })

  const filtered = useMemo(
    () => filterPartsByQuery(data ?? [], query),
    [data, query],
  )

  useEffect(() => {
    if (!open) {
      setQuery('')
      return
    }
    const frame = requestAnimationFrame(() => {
      searchRef.current?.focus()
    })
    return () => cancelAnimationFrame(frame)
  }, [open, slot?.key])

  useEffect(() => {
    if (!open) return
    function onKeyDown(e: KeyboardEvent) {
      if (e.key === 'Escape') {
        e.preventDefault()
        onClose()
      }
    }
    window.addEventListener('keydown', onKeyDown)
    return () => window.removeEventListener('keydown', onKeyDown)
  }, [open, onClose])

  if (!open || !slot) return null

  return (
    <div
      className="fixed inset-0 z-50 flex items-end justify-center sm:items-center"
      role="dialog"
      aria-modal="true"
      aria-labelledby="part-picker-title"
    >
      <button
        type="button"
        className="absolute inset-0 bg-steel-950/80"
        aria-label="Close part picker"
        onClick={onClose}
      />
      <div className="relative flex max-h-[85vh] w-full max-w-xl flex-col border border-steel-700 bg-steel-900 shadow-2xl sm:mx-4">
        <div className="flex items-start justify-between gap-4 border-b border-steel-800 px-4 py-4">
          <div>
            <p className="font-mono text-xs uppercase tracking-wider text-accent">
              Select part
            </p>
            <h2 id="part-picker-title" className="text-lg font-semibold text-steel-100">
              {slot.label}
            </h2>
            <p className="text-sm text-steel-400">
              {CATALOG_LABELS[slot.catalogType]}
            </p>
          </div>
          <button
            type="button"
            onClick={onClose}
            className="text-sm text-steel-400 hover:text-steel-100"
          >
            Close
          </button>
        </div>

        <div className="border-b border-steel-800 px-4 py-3">
          <label className="sr-only" htmlFor="part-search">
            Search parts
          </label>
          <input
            ref={searchRef}
            id="part-search"
            type="search"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Filter by name or brand"
            className="w-full border border-steel-700 bg-steel-950 px-3 py-2 text-sm text-steel-100 outline-none placeholder:text-steel-500 focus:border-accent"
          />
        </div>

        <div className="flex-1 overflow-y-auto px-4 py-3">
          {isLoading ? (
            <LoadingState title="Loading parts…" />
          ) : isError ? (
            <div className="space-y-3">
              <ErrorState
                title="Could not load parts"
                detail={error instanceof Error ? error.message : 'Unknown error'}
              />
              <button
                type="button"
                onClick={() => refetch()}
                className="text-sm text-accent hover:underline"
              >
                Retry
              </button>
            </div>
          ) : filtered.length === 0 ? (
            <EmptyState
              title="No matching parts"
              detail="Try a different search term."
            />
          ) : (
            <ul className="space-y-2">
              {filtered.map((part) => {
                const selected = part.id === selectedId
                return (
                  <li key={part.id}>
                    <button
                      type="button"
                      onClick={() => onSelect(part.id, part)}
                      className={[
                        'flex w-full items-start justify-between gap-3 border px-3 py-3 text-left transition-colors',
                        selected
                          ? 'border-accent bg-accent-soft'
                          : 'border-steel-800 bg-steel-850 hover:border-steel-600',
                      ].join(' ')}
                    >
                      <span>
                        <span className="block text-sm font-medium text-steel-100">
                          {part.name}
                        </span>
                        <span className="mt-0.5 block text-xs text-steel-400">
                          {part.brand?.name ?? 'Unknown brand'}
                        </span>
                      </span>
                      <span className="shrink-0 font-mono text-xs text-accent">
                        {formatPrice(part.price, part.currency)}
                      </span>
                    </button>
                  </li>
                )
              })}
            </ul>
          )}
        </div>

        <div className="flex items-center justify-between gap-3 border-t border-steel-800 px-4 py-3">
          <button
            type="button"
            onClick={onClear}
            disabled={!selectedId}
            className="text-sm text-steel-400 hover:text-danger disabled:opacity-40"
          >
            Clear slot
          </button>
          <button
            type="button"
            onClick={onClose}
            className="border border-steel-600 px-4 py-2 text-sm text-steel-100 hover:border-steel-400"
          >
            Done
          </button>
        </div>
      </div>
    </div>
  )
}
