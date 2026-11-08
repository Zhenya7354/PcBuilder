import { useMemo, useState, type FormEvent } from 'react'
import { useNavigate } from 'react-router'
import { useMutation, useQueries, useQueryClient } from '@tanstack/react-query'
import { recommendBuild } from '../api/ai'
import { createBuild } from '../api/builds'
import { formatPrice, listParts } from '../api/catalog'
import {
  isCompatibilityError,
  type BuildRecommendationResult,
  type BuildRequest,
  type CompatibilityIssue,
  type ComponentBase,
} from '../api/types'
import { CompatibilityIssues } from '../components/CompatibilityIssues'
import { BUILD_SLOTS } from '../components/PartPicker'
import { EmptyState, ErrorState, LoadingState } from '../components/StatusBlocks'

export function AiRecommend() {
  const navigate = useNavigate()
  const queryClient = useQueryClient()

  const [prompt, setPrompt] = useState('')
  const [name, setName] = useState('')
  const [acceptAiSuggestedName, setAcceptAiSuggestedName] = useState(false)
  const [result, setResult] = useState<BuildRecommendationResult | null>(null)
  const [recommendError, setRecommendError] = useState<string | null>(null)
  const [compatIssues, setCompatIssues] = useState<CompatibilityIssue[]>([])
  const [compatMessage, setCompatMessage] = useState<string | undefined>()
  const [saveError, setSaveError] = useState<string | null>(null)

  const recommend = useMutation({
    mutationFn: () =>
      recommendBuild({
        prompt: prompt.trim(),
        name: name.trim() || undefined,
        acceptAiSuggestedName,
      }),
    onSuccess: (data) => {
      setResult(data)
      setRecommendError(null)
      setCompatIssues([])
      setCompatMessage(undefined)
      setSaveError(null)
    },
    onError: (err) => {
      setResult(null)
      setRecommendError(
        err instanceof Error ? err.message : 'Recommendation failed',
      )
    },
  })

  const save = useMutation({
    mutationFn: (body: BuildRequest) => createBuild(body),
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: ['builds'] })
      navigate('/builds')
    },
    onError: (err) => {
      if (isCompatibilityError(err)) {
        setCompatMessage(err.body.message ?? err.message)
        setCompatIssues(err.body.issues)
        setSaveError(null)
        return
      }
      setCompatIssues([])
      setSaveError(err instanceof Error ? err.message : 'Save failed')
    },
  })

  const filledSlots = useMemo(() => {
    if (!result) return []
    return BUILD_SLOTS.filter((s) => result.build[s.key] != null)
  }, [result])

  const partQueries = useQueries({
    queries: filledSlots.map((slot) => ({
      queryKey: ['catalog', slot.catalogType],
      queryFn: () => listParts(slot.catalogType),
      enabled: !!result,
    })),
  })

  const resolvedParts = useMemo(() => {
    if (!result) return [] as { label: string; part: ComponentBase | null; id: number }[]
    return filledSlots.map((slot, index) => {
      const id = result.build[slot.key] as number
      const parts = partQueries[index]?.data
      const part = parts?.find((p) => p.id === id) ?? null
      return { label: slot.label, part, id }
    })
  }, [result, filledSlots, partQueries])

  function onSubmit(e: FormEvent) {
    e.preventDefault()
    if (!prompt.trim()) return
    recommend.mutate()
  }

  return (
    <div>
      <header className="mb-8">
        <h1 className="text-3xl font-semibold tracking-tight text-steel-100">
          AI recommend
        </h1>
        <p className="mt-2 max-w-xl text-sm text-steel-400">
          Describe the PC you want. The API suggests parts but does not save them —
          review the result, then save as your build.
        </p>
      </header>

      <form onSubmit={onSubmit} className="max-w-2xl space-y-4">
        <div>
          <label htmlFor="ai-prompt" className="mb-1 block text-sm text-steel-300">
            Prompt
          </label>
          <textarea
            id="ai-prompt"
            required
            rows={4}
            value={prompt}
            onChange={(e) => setPrompt(e.target.value)}
            placeholder="Gaming PC for 1440p, around 1500 EUR, prefer NVIDIA…"
            className="w-full border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none focus:border-accent"
          />
        </div>
        <div>
          <label htmlFor="ai-name" className="mb-1 block text-sm text-steel-300">
            Build name (optional)
          </label>
          <input
            id="ai-name"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            className="w-full border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none focus:border-accent"
          />
        </div>
        <label className="flex items-center gap-2 text-sm text-steel-300">
          <input
            type="checkbox"
            checked={acceptAiSuggestedName}
            onChange={(e) => setAcceptAiSuggestedName(e.target.checked)}
            className="accent-accent"
          />
          Accept AI-suggested name when name is empty
        </label>
        <button
          type="submit"
          disabled={recommend.isPending || !prompt.trim()}
          className="bg-accent px-5 py-2.5 text-sm font-semibold text-steel-950 hover:bg-accent-dim disabled:opacity-60"
        >
          {recommend.isPending ? 'Asking AI…' : 'Get recommendation'}
        </button>
      </form>

      {recommend.isPending ? (
        <div className="mt-8">
          <LoadingState
            title="Generating recommendation…"
            detail="This can take a few seconds while Gemini picks parts."
          />
        </div>
      ) : null}

      {recommendError ? (
        <div className="mt-8">
          <ErrorState title="Recommendation failed" detail={recommendError} />
        </div>
      ) : null}

      {result ? (
        <section className="mt-10 space-y-6">
          <div className="flex flex-wrap items-baseline gap-3">
            <h2 className="text-xl font-semibold text-steel-100">Result</h2>
            <span className="font-mono text-xs uppercase tracking-wider text-accent">
              {result.status}
            </span>
            {result.build.name ? (
              <span className="text-sm text-steel-400">
                Suggested name: {result.build.name}
              </span>
            ) : null}
          </div>

          {result.notes.length > 0 ? (
            <div>
              <h3 className="mb-2 text-sm font-medium text-steel-300">Notes</h3>
              <ul className="list-disc space-y-1 pl-5 text-sm text-steel-300">
                {result.notes.map((note, i) => (
                  <li key={i}>{note}</li>
                ))}
              </ul>
            </div>
          ) : (
            <EmptyState title="No notes returned" detail="Parts may still be present below." />
          )}

          <div>
            <h3 className="mb-3 text-sm font-medium text-steel-300">
              Suggested parts
            </h3>
            {resolvedParts.length === 0 ? (
              <EmptyState
                title="No parts in recommendation"
                detail="Try a more specific prompt or check API status."
              />
            ) : (
              <ul className="divide-y divide-steel-800 border border-steel-800">
                {resolvedParts.map(({ label, part, id }) => (
                  <li
                    key={label}
                    className="flex items-center justify-between gap-3 px-4 py-3"
                  >
                    <span>
                      <span className="block font-mono text-xs uppercase tracking-wider text-steel-500">
                        {label}
                      </span>
                      <span className="text-sm text-steel-100">
                        {part?.name ?? `Part #${id}`}
                      </span>
                      {part?.brand?.name ? (
                        <span className="mt-0.5 block text-xs text-steel-400">
                          {part.brand.name}
                        </span>
                      ) : null}
                    </span>
                    <span className="font-mono text-xs text-accent">
                      {part
                        ? formatPrice(part.price, part.currency)
                        : '—'}
                    </span>
                  </li>
                ))}
              </ul>
            )}
          </div>

          {compatIssues.length > 0 ? (
            <CompatibilityIssues issues={compatIssues} message={compatMessage} />
          ) : null}

          {saveError ? (
            <p className="border border-danger/40 bg-danger-soft px-3 py-2 text-sm text-danger" role="alert">
              {saveError}
            </p>
          ) : null}

          <button
            type="button"
            disabled={save.isPending || filledSlots.length === 0}
            onClick={() => {
              if (!result) return
              const body: BuildRequest = {
                ...result.build,
                name:
                  name.trim() ||
                  result.build.name ||
                  undefined,
              }
              save.mutate(body)
            }}
            className="bg-accent px-5 py-2.5 text-sm font-semibold text-steel-950 hover:bg-accent-dim disabled:opacity-60"
          >
            {save.isPending ? 'Saving…' : 'Save as my build'}
          </button>
        </section>
      ) : null}
    </div>
  )
}
