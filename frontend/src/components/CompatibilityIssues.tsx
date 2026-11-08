import type { CompatibilityIssue } from '../api/types'

interface Props {
  issues: CompatibilityIssue[]
  message?: string
}

export function CompatibilityIssues({ issues, message }: Props) {
  if (!issues.length) return null

  const errors = issues.filter((i) => i.severity === 'Error')
  const warnings = issues.filter((i) => i.severity === 'Warning')

  return (
    <div className="space-y-4 border border-steel-800 bg-steel-900/70 p-4" role="alert">
      {message ? (
        <p className="text-sm font-medium text-steel-100">{message}</p>
      ) : (
        <p className="text-sm font-medium text-steel-100">Compatibility issues</p>
      )}

      {errors.length > 0 ? (
        <section>
          <h3 className="mb-2 font-mono text-xs uppercase tracking-wider text-danger">
            Errors ({errors.length})
          </h3>
          <ul className="space-y-2">
            {errors.map((issue, idx) => (
              <li
                key={`e-${issue.field}-${idx}`}
                className="border-l-2 border-danger bg-danger-soft px-3 py-2 text-sm"
              >
                <span className="font-mono text-xs text-steel-400">{issue.field}</span>
                <p className="text-steel-100">{issue.message}</p>
              </li>
            ))}
          </ul>
        </section>
      ) : null}

      {warnings.length > 0 ? (
        <section>
          <h3 className="mb-2 font-mono text-xs uppercase tracking-wider text-warn">
            Warnings ({warnings.length})
          </h3>
          <ul className="space-y-2">
            {warnings.map((issue, idx) => (
              <li
                key={`w-${issue.field}-${idx}`}
                className="border-l-2 border-warn bg-warn-soft px-3 py-2 text-sm"
              >
                <span className="font-mono text-xs text-steel-400">{issue.field}</span>
                <p className="text-steel-100">{issue.message}</p>
              </li>
            ))}
          </ul>
        </section>
      ) : null}
    </div>
  )
}
