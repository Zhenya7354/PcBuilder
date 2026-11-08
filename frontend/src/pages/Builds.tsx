import { Link } from 'react-router'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { deleteBuild, listBuilds } from '../api/builds'
import { EmptyState, ErrorState, LoadingState } from '../components/StatusBlocks'

function partCount(build: {
  cpuId?: number | null
  cpuCoolerId?: number | null
  gpuId?: number | null
  ramId?: number | null
  hardDriveId?: number | null
  motherboardId?: number | null
  psuId?: number | null
  caseId?: number | null
  monitorId?: number | null
}): number {
  return [
    build.cpuId,
    build.cpuCoolerId,
    build.gpuId,
    build.ramId,
    build.hardDriveId,
    build.motherboardId,
    build.psuId,
    build.caseId,
    build.monitorId,
  ].filter((id) => id != null).length
}

export function Builds() {
  const queryClient = useQueryClient()
  const { data, isLoading, isError, error, refetch } = useQuery({
    queryKey: ['builds'],
    queryFn: listBuilds,
  })

  const remove = useMutation({
    mutationFn: deleteBuild,
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: ['builds'] })
    },
  })

  return (
    <div>
      <header className="mb-8 flex flex-wrap items-end justify-between gap-4">
        <div>
          <h1 className="text-3xl font-semibold tracking-tight text-steel-100">
            My builds
          </h1>
          <p className="mt-2 text-sm text-steel-400">
            Saved configurations for your account.
          </p>
        </div>
        <Link
          to="/builds/new"
          className="bg-accent px-4 py-2.5 text-sm font-semibold text-steel-950 hover:bg-accent-dim"
        >
          New build
        </Link>
      </header>

      {isLoading ? (
        <LoadingState title="Loading builds…" />
      ) : isError ? (
        <div className="space-y-3">
          <ErrorState
            title="Could not load builds"
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
      ) : !data?.length ? (
        <EmptyState
          title="No builds yet"
          detail="Create a configurator build or save an AI recommendation."
          action={
            <Link
              to="/builds/new"
              className="inline-block bg-accent px-4 py-2.5 text-sm font-semibold text-steel-950 hover:bg-accent-dim"
            >
              Create a build
            </Link>
          }
        />
      ) : (
        <ul className="space-y-3">
          {data.map((build) => (
            <li
              key={build.id}
              className="flex flex-wrap items-center justify-between gap-3 border border-steel-800 bg-steel-900/50 px-4 py-4"
            >
              <Link to={`/builds/${build.id}`} className="min-w-0 flex-1 hover:text-accent">
                <span className="block truncate text-sm font-medium text-steel-100">
                  {build.name?.trim() || `Build #${build.id}`}
                </span>
                <span className="font-mono text-xs text-steel-400">
                  {partCount(build)} / 9 slots · id {build.id}
                </span>
              </Link>
              <div className="flex gap-2">
                <Link
                  to={`/builds/${build.id}`}
                  className="border border-steel-600 px-3 py-1.5 text-sm text-steel-200 hover:border-accent"
                >
                  Edit
                </Link>
                <button
                  type="button"
                  disabled={remove.isPending}
                  onClick={() => {
                    if (
                      window.confirm(
                        `Delete “${build.name?.trim() || `Build #${build.id}`}”?`,
                      )
                    ) {
                      remove.mutate(build.id)
                    }
                  }}
                  className="border border-danger/50 px-3 py-1.5 text-sm text-danger hover:bg-danger-soft disabled:opacity-50"
                >
                  Delete
                </button>
              </div>
            </li>
          ))}
        </ul>
      )}

      {remove.isError ? (
        <p className="mt-4 text-sm text-danger" role="alert">
          {remove.error instanceof Error
            ? remove.error.message
            : 'Delete failed'}
        </p>
      ) : null}
    </div>
  )
}
