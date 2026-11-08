import type { ReactNode } from 'react'

interface StatusProps {
  title: string
  detail?: string
  action?: ReactNode
}

export function LoadingState({ title, detail }: StatusProps) {
  return (
    <div
      className="flex flex-col items-start gap-2 border border-steel-800 bg-steel-900/60 px-5 py-8"
      role="status"
      aria-live="polite"
    >
      <div className="h-1 w-24 animate-pulse bg-accent/70" />
      <p className="text-sm font-medium text-steel-100">{title}</p>
      {detail ? <p className="text-sm text-steel-400">{detail}</p> : null}
    </div>
  )
}

export function EmptyState({ title, detail, action }: StatusProps) {
  return (
    <div className="border border-dashed border-steel-700 bg-steel-900/40 px-5 py-10">
      <p className="text-sm font-medium text-steel-100">{title}</p>
      {detail ? <p className="mt-1 text-sm text-steel-400">{detail}</p> : null}
      {action ? <div className="mt-4">{action}</div> : null}
    </div>
  )
}

export function ErrorState({ title, detail }: StatusProps) {
  return (
    <div
      className="border border-danger/40 bg-danger-soft px-5 py-6"
      role="alert"
    >
      <p className="text-sm font-medium text-danger">{title}</p>
      {detail ? <p className="mt-1 text-sm text-steel-300">{detail}</p> : null}
    </div>
  )
}
