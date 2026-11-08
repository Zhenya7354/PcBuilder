import { Link, useNavigate, useParams, useSearchParams } from 'react-router'
import { useQuery } from '@tanstack/react-query'
import {
  CATALOG_LABELS,
  formatPrice,
  getCatalogItem,
  isCatalogType,
} from '../api/catalog'
import type { CatalogType } from '../api/types'
import {
  catalogTypeToSlotKey,
  isPickMode,
  useBuildDraft,
} from '../build-draft'
import { ErrorState, LoadingState } from '../components/StatusBlocks'

function SpecRows({ part }: { part: Record<string, unknown> }) {
  const skip = new Set(['id', 'brandId', 'brand', 'name', 'currency', 'price'])
  const entries = Object.entries(part).filter(
    ([key, value]) => !skip.has(key) && value != null && value !== '',
  )

  if (!entries.length) {
    return <p className="text-sm text-steel-400">No additional specs.</p>
  }

  return (
    <dl className="grid gap-3 sm:grid-cols-2">
      {entries.map(([key, value]) => (
        <div key={key} className="border-b border-steel-800 pb-2">
          <dt className="font-mono text-xs uppercase tracking-wider text-steel-500">
            {key}
          </dt>
          <dd className="mt-1 text-sm text-steel-100">
            {Array.isArray(value)
              ? value.join(', ')
              : typeof value === 'boolean'
                ? value
                  ? 'Yes'
                  : 'No'
                : String(value)}
          </dd>
        </div>
      ))}
    </dl>
  )
}

export function PartDetail() {
  const { type = '', id = '' } = useParams()
  const [searchParams] = useSearchParams()
  const picking = isPickMode(searchParams.get('pick'))
  const navigate = useNavigate()
  const { setSlot } = useBuildDraft()
  const numericId = Number(id)
  const validType =
    isCatalogType(type) && type !== 'brands'
      ? (type as Exclude<CatalogType, 'brands'>)
      : null

  const { data, isLoading, isError, error, refetch } = useQuery({
    queryKey: ['catalog', validType, numericId],
    queryFn: () => getCatalogItem(validType!, numericId),
    enabled: !!validType && Number.isFinite(numericId) && numericId > 0,
  })

  if (!validType || !Number.isFinite(numericId) || numericId <= 0) {
    return (
      <ErrorState
        title="Invalid part link"
        detail="Unknown catalog type or id."
      />
    )
  }

  const backHref = picking
    ? `/catalog?type=${validType}&pick=1`
    : `/catalog?type=${validType}`

  function addToBuild() {
    if (!data || !validType) return
    const key = catalogTypeToSlotKey(validType)
    setSlot(key, {
      id: data.id,
      name: data.name,
      brandName: data.brand?.name ?? undefined,
      price: data.price,
      currency: data.currency,
    })
    navigate('/')
  }

  return (
    <div>
      <Link
        to={backHref}
        className="text-sm text-steel-400 hover:text-accent"
      >
        ← Back to {CATALOG_LABELS[validType]}
      </Link>

      {isLoading ? (
        <div className="mt-6">
          <LoadingState title="Loading part…" />
        </div>
      ) : isError ? (
        <div className="mt-6 space-y-3">
          <ErrorState
            title="Could not load part"
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
      ) : data ? (
        <article className="mt-6">
          {picking ? (
            <div className="mb-6 border border-accent/40 bg-accent-soft px-4 py-3">
              <p className="text-sm font-medium text-accent">
                Selecting for your build
              </p>
            </div>
          ) : null}

          <p className="font-mono text-xs uppercase tracking-wider text-accent">
            {CATALOG_LABELS[validType]}
          </p>
          <h1 className="mt-2 text-3xl font-semibold tracking-tight text-steel-100">
            {data.name}
          </h1>
          <p className="mt-2 text-sm text-steel-400">
            {data.brand?.name ?? 'Unknown brand'}
            <span className="mx-2 text-steel-700">·</span>
            <span className="font-mono text-accent">
              {formatPrice(data.price, data.currency)}
            </span>
          </p>

          {picking ? (
            <button
              type="button"
              onClick={addToBuild}
              className="mt-6 bg-accent px-5 py-2.5 text-sm font-semibold text-steel-950 transition-colors hover:bg-accent-dim"
            >
              Add to build
            </button>
          ) : (
            <button
              type="button"
              onClick={addToBuild}
              className="mt-6 border border-steel-600 px-5 py-2.5 text-sm text-steel-200 transition-colors hover:border-accent hover:text-accent"
            >
              Add to build
            </button>
          )}

          <div className="mt-8">
            <h2 className="mb-4 text-sm font-medium text-steel-300">Specs</h2>
            <SpecRows part={data as unknown as Record<string, unknown>} />
          </div>
        </article>
      ) : (
        <div className="mt-6">
          <ErrorState title="Part not found" />
        </div>
      )}
    </div>
  )
}
