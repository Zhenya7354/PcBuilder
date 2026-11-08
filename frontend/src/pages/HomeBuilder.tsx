import { useEffect, useRef, useState } from 'react'
import { Link, useLocation, useNavigate } from 'react-router'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { createBuild } from '../api/builds'
import { formatPrice } from '../api/catalog'
import {
  isCompatibilityError,
  type CompatibilityIssue,
} from '../api/types'
import { useAuth } from '../auth/AuthContext'
import {
  BUILD_SLOTS,
  pickCatalogUrl,
  useBuildDraft,
} from '../build-draft'
import { CompatibilityIssues } from '../components/CompatibilityIssues'

/** Survives Strict Mode remount; blocks effect re-entry after clear+mutate (and after onError restore). */
let autoSaveLatch = false

export function HomeBuilder() {
  const { isAuthenticated } = useAuth()
  const { draft, filledCount, setName, clearSlot, clearAll, setPendingSave, toBuildRequest } =
    useBuildDraft()
  const navigate = useNavigate()
  const location = useLocation()
  const queryClient = useQueryClient()
  const builderRef = useRef<HTMLElement>(null)

  const [compatIssues, setCompatIssues] = useState<CompatibilityIssue[]>([])
  const [compatMessage, setCompatMessage] = useState<string | undefined>()
  const [formError, setFormError] = useState<string | null>(null)

  const createMutation = useMutation({
    mutationFn: () => createBuild(toBuildRequest()),
    onSuccess: () => {
      autoSaveLatch = false
      setCompatIssues([])
      setCompatMessage(undefined)
      setFormError(null)
      setPendingSave(false)
      clearAll()
      void queryClient.invalidateQueries({ queryKey: ['builds'] })
      navigate('/builds')
    },
    onError: (err) => {
      // Restore so Create can retry; latch stays set so the effect does not loop.
      setPendingSave(true)
      if (isCompatibilityError(err)) {
        setCompatIssues(err.body.issues)
        setCompatMessage(err.body.message)
        setFormError(null)
        return
      }
      setCompatIssues([])
      setCompatMessage(undefined)
      setFormError(err instanceof Error ? err.message : 'Could not create build.')
    },
  })

  // Scroll / focus when arriving with #builder (e.g. navbar CTA from another page).
  useEffect(() => {
    if (location.hash !== '#builder') return
    const el = builderRef.current
    if (!el) return
    el.scrollIntoView({ behavior: 'smooth', block: 'start' })
    el.focus({ preventScroll: true })
  }, [location.hash])

  // Complete a guest save after login redirected back home.
  // Clear pendingSave before mutate so remounts do not double-create; onError restores
  // for Create retry. Latch covers Strict Mode double-effect and restore re-entry.
  useEffect(() => {
    if (!isAuthenticated || !draft.pendingSave) return
    if (filledCount === 0) {
      setPendingSave(false)
      return
    }
    if (autoSaveLatch) return
    autoSaveLatch = true
    setPendingSave(false)
    createMutation.mutate()
    // eslint-disable-next-line react-hooks/exhaustive-deps -- fire once when pending + auth + non-empty
  }, [isAuthenticated, draft.pendingSave, filledCount])

  function onCreateBuild() {
    setFormError(null)
    setCompatIssues([])
    setCompatMessage(undefined)

    if (filledCount === 0) {
      setPendingSave(false)
      setFormError('Add at least one part before creating a build.')
      return
    }

    if (!isAuthenticated) {
      autoSaveLatch = false
      setPendingSave(true)
      navigate('/login', { state: { from: '/' } })
      return
    }

    createMutation.mutate()
  }

  return (
    <div>
      <header className="mb-10 max-w-2xl">
        <h1 className="text-4xl font-bold tracking-tight text-steel-100 sm:text-5xl">
          Build your PC. Verify compatibility.
        </h1>
        <p className="mt-4 text-base text-steel-300 sm:text-lg">
          Pick parts for each slot, check they fit together, then save a build
          when you are ready.
        </p>
      </header>

      <section
        id="builder"
        ref={builderRef}
        tabIndex={-1}
        className="scroll-mt-24 outline-none"
        aria-labelledby="builder-heading"
      >
        <div className="mb-6 max-w-md">
          <label
            htmlFor="draft-name"
            className="mb-1 block text-sm text-steel-300"
          >
            Build name <span className="text-steel-500">(optional)</span>
          </label>
          <input
            id="draft-name"
            type="text"
            value={draft.name}
            onChange={(e) => setName(e.target.value)}
            placeholder="e.g. 1440p gaming"
            className="w-full border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none placeholder:text-steel-500 focus:border-accent"
          />
        </div>

        <div className="mb-4 flex flex-wrap items-end justify-between gap-3">
          <div>
            <h2
              id="builder-heading"
              className="text-lg font-semibold tracking-tight text-steel-100"
            >
              Draft board
            </h2>
            <p className="mt-1 text-sm text-steel-400">
              {filledCount} / {BUILD_SLOTS.length} slots filled
              {!isAuthenticated ? ' · saved in this browser' : null}
            </p>
          </div>
          {filledCount > 0 ? (
            <button
              type="button"
              onClick={() => {
                clearAll()
                setPendingSave(false)
                setFormError(null)
                setCompatIssues([])
                setCompatMessage(undefined)
              }}
              className="text-sm text-steel-400 transition-colors hover:text-accent"
            >
              Clear draft
            </button>
          ) : null}
        </div>

        <ul className="divide-y divide-steel-800 border border-steel-800 bg-steel-900/50">
          {BUILD_SLOTS.map((slot) => {
            const part = draft.slots[slot.key]
            return (
              <li
                key={slot.key}
                className="flex flex-wrap items-center justify-between gap-3 px-4 py-3.5 sm:px-5"
              >
                <div className="min-w-0 flex-1">
                  <p className="font-mono text-xs uppercase tracking-wider text-steel-500">
                    {slot.label}
                  </p>
                  {part ? (
                    <>
                      <p className="mt-1 truncate text-sm font-medium text-steel-100">
                        {part.name}
                      </p>
                      <p className="text-xs text-steel-400">
                        {part.brandName ?? 'Unknown brand'}
                        {part.price != null ? (
                          <>
                            <span className="mx-1.5 text-steel-700">·</span>
                            <span className="font-mono text-accent">
                              {formatPrice(part.price, part.currency)}
                            </span>
                          </>
                        ) : null}
                      </p>
                    </>
                  ) : (
                    <p className="mt-1 text-sm text-steel-500">Empty</p>
                  )}
                </div>

                <div className="flex shrink-0 items-center gap-2">
                  {part ? (
                    <>
                      <Link
                        to={pickCatalogUrl(slot.catalogType)}
                        className="border border-steel-600 px-3 py-1.5 text-sm text-steel-200 transition-colors hover:border-accent hover:text-accent"
                      >
                        Change
                      </Link>
                      <button
                        type="button"
                        onClick={() => {
                          clearSlot(slot.key)
                          if (filledCount <= 1) setPendingSave(false)
                        }}
                        className="border border-steel-700 px-3 py-1.5 text-sm text-steel-400 transition-colors hover:border-danger hover:text-danger"
                      >
                        Remove
                      </button>
                    </>
                  ) : (
                    <Link
                      to={pickCatalogUrl(slot.catalogType)}
                      className="bg-accent px-3 py-1.5 text-sm font-semibold text-steel-950 transition-colors hover:bg-accent-dim"
                    >
                      Add
                    </Link>
                  )}
                </div>
              </li>
            )
          })}
        </ul>

        <div className="mt-8 space-y-4 border-t border-steel-800 pt-8">
          {compatIssues.length > 0 ? (
            <CompatibilityIssues issues={compatIssues} message={compatMessage} />
          ) : null}

          {formError ? (
            <p
              className="border border-danger/40 bg-danger-soft px-3 py-2 text-sm text-danger"
              role="alert"
            >
              {formError}
            </p>
          ) : null}

          <div className="flex flex-wrap items-center gap-3">
            <button
              type="button"
              onClick={onCreateBuild}
              disabled={createMutation.isPending}
              className="bg-accent px-5 py-2.5 text-sm font-semibold text-steel-950 transition-colors hover:bg-accent-dim disabled:opacity-60"
            >
              {createMutation.isPending ? 'Saving…' : 'Create build'}
            </button>
            {!isAuthenticated ? (
              <p className="text-xs text-steel-500">
                You will sign in to save. Your draft stays in this browser.
              </p>
            ) : null}
          </div>
        </div>
      </section>
    </div>
  )
}
