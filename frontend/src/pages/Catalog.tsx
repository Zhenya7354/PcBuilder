import { useMemo, useState } from 'react'
import { Link, useNavigate, useSearchParams } from 'react-router'
import { useQuery } from '@tanstack/react-query'
import {
  CATALOG_LABELS,
  CATALOG_TYPES,
  filterPartsByQuery,
  formatPrice,
  isCatalogType,
  listCatalog,
} from '../api/catalog'
import type { CatalogType, ComponentBase } from '../api/types'
import {
  catalogTypeToSlotKey,
  isPickMode,
  useBuildDraft,
} from '../build-draft'
import { EmptyState, ErrorState, LoadingState } from '../components/StatusBlocks'

export function Catalog() {
  const [searchParams, setSearchParams] = useSearchParams()
  const navigate = useNavigate()
  const { setSlot } = useBuildDraft()

  const typeParam = searchParams.get('type') ?? 'cpus'
  const picking = isPickMode(searchParams.get('pick'))
  const activeType: Exclude<CatalogType, 'brands'> = isCatalogType(typeParam) &&
    typeParam !== 'brands'
    ? typeParam
    : 'cpus'

  const [query, setQuery] = useState('')

  const { data, isLoading, isError, error, refetch } = useQuery({
    queryKey: ['catalog', activeType],
    queryFn: () => listCatalog(activeType) as Promise<ComponentBase[]>,
  })

  const filtered = useMemo(
    () => filterPartsByQuery(data ?? [], query),
    [data, query],
  )

  const slotLabel =
    BUILD_SLOT_LABEL[activeType] ?? CATALOG_LABELS[activeType]

  function selectType(type: Exclude<CatalogType, 'brands'>) {
    const next: Record<string, string> = { type }
    if (picking) next.pick = '1'
    setSearchParams(next)
    setQuery('')
  }

  function selectPart(part: ComponentBase) {
    const key = catalogTypeToSlotKey(activeType)
    setSlot(key, {
      id: part.id,
      name: part.name,
      brandName: part.brand?.name ?? undefined,
      price: part.price,
      currency: part.currency,
    })
    navigate('/')
  }

  return (
    <div>
      <header className="mb-8">
        <h1 className="text-3xl font-semibold tracking-tight text-steel-100">
          Catalog
        </h1>
        <p className="mt-2 max-w-xl text-sm text-steel-400">
          Browse seeded components. Search filters by part name or brand on this page.
        </p>
      </header>

      {picking ? (
        <div
          className="mb-6 border border-accent/40 bg-accent-soft px-4 py-3"
          role="status"
        >
          <p className="text-sm font-medium text-accent">
            Selecting a {slotLabel} for your build
          </p>
          <p className="mt-1 text-xs text-steel-300">
            Choose a part below to add it to your draft board.
            {' '}
            <Link to="/" className="underline hover:text-accent">
              Cancel and return home
            </Link>
          </p>
        </div>
      ) : null}

      {picking ? (
        <p className="mb-6 text-sm text-steel-400">
          Showing{' '}
          <span className="font-medium text-steel-200">
            {CATALOG_LABELS[activeType]}
          </span>
          {' '}
          only. Return home to pick a different slot.
        </p>
      ) : (
        <div
          className="mb-6 flex flex-wrap gap-2"
          role="tablist"
          aria-label="Part types"
        >
          {CATALOG_TYPES.map((type) => {
            const active = type === activeType
            return (
              <button
                key={type}
                type="button"
                role="tab"
                aria-selected={active}
                onClick={() => selectType(type)}
                className={[
                  'border px-3 py-1.5 text-sm transition-colors',
                  active
                    ? 'border-accent bg-accent-soft text-accent'
                    : 'border-steel-700 text-steel-300 hover:border-steel-500 hover:text-steel-100',
                ].join(' ')}
              >
                {CATALOG_LABELS[type]}
              </button>
            )
          })}
        </div>
      )}

      <label className="sr-only" htmlFor="catalog-search">
        Search catalog
      </label>
      <input
        id="catalog-search"
        type="search"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        placeholder="Search by name or brand"
        className="mb-6 w-full max-w-md border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none placeholder:text-steel-500 focus:border-accent"
      />

      {isLoading ? (
        <LoadingState title="Loading catalog…" detail={`Fetching ${CATALOG_LABELS[activeType]}`} />
      ) : isError ? (
        <div className="space-y-3">
          <ErrorState
            title="Could not load catalog"
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
          title="No parts found"
          detail={
            query
              ? 'Nothing matches your search.'
              : 'This category is empty in the API.'
          }
        />
      ) : (
        <ul className="divide-y divide-steel-800 border border-steel-800">
          {filtered.map((part) =>
            picking ? (
              <li key={part.id}>
                <button
                  type="button"
                  onClick={() => selectPart(part)}
                  className="flex w-full items-center justify-between gap-4 px-4 py-3 text-left transition-colors hover:bg-steel-900"
                >
                  <span>
                    <span className="block text-sm font-medium text-steel-100">
                      {part.name}
                    </span>
                    <span className="text-xs text-steel-400">
                      {part.brand?.name ?? 'Unknown brand'}
                    </span>
                  </span>
                  <span className="flex shrink-0 items-center gap-3">
                    <span className="font-mono text-xs text-accent">
                      {formatPrice(part.price, part.currency)}
                    </span>
                    <span className="bg-accent px-2.5 py-1 text-xs font-semibold text-steel-950">
                      Select
                    </span>
                  </span>
                </button>
              </li>
            ) : (
              <li key={part.id}>
                <Link
                  to={`/catalog/${activeType}/${part.id}`}
                  className="flex items-center justify-between gap-4 px-4 py-3 transition-colors hover:bg-steel-900"
                >
                  <span>
                    <span className="block text-sm font-medium text-steel-100">
                      {part.name}
                    </span>
                    <span className="text-xs text-steel-400">
                      {part.brand?.name ?? 'Unknown brand'}
                    </span>
                  </span>
                  <span className="font-mono text-xs text-accent">
                    {formatPrice(part.price, part.currency)}
                  </span>
                </Link>
              </li>
            ),
          )}
        </ul>
      )}
    </div>
  )
}

/** Singular-ish labels for the pick banner. */
const BUILD_SLOT_LABEL: Record<Exclude<CatalogType, 'brands'>, string> = {
  cpus: 'CPU',
  gpus: 'GPU',
  motherboards: 'motherboard',
  rams: 'RAM kit',
  psus: 'PSU',
  'pc-cases': 'case',
  'cpu-coolers': 'CPU cooler',
  'hard-drives': 'storage drive',
  'pc-monitors': 'monitor',
}
