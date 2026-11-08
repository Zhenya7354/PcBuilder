import { ApiError } from './types'

const AUTH_STORAGE_KEY = 'pcbuilder.auth'

export interface StoredSession {
  token: string
  email: string
  roles: string[]
  expiresAt: string
}

export function getApiBaseUrl(): string {
  return import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5150'
}

function isExpired(expiresAt: string): boolean {
  const ms = Date.parse(expiresAt)
  if (Number.isNaN(ms)) return false
  return ms <= Date.now()
}

export function loadSession(): StoredSession | null {
  try {
    const raw = localStorage.getItem(AUTH_STORAGE_KEY)
    if (!raw) return null
    const session = JSON.parse(raw) as StoredSession
    if (session.expiresAt && isExpired(session.expiresAt)) {
      localStorage.removeItem(AUTH_STORAGE_KEY)
      return null
    }
    return session
  } catch {
    return null
  }
}

export function saveSession(session: StoredSession): void {
  localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(session))
}

export function clearSession(): void {
  localStorage.removeItem(AUTH_STORAGE_KEY)
}

type UnauthorizedHandler = () => void

let onUnauthorized: UnauthorizedHandler | null = null

export function setUnauthorizedHandler(handler: UnauthorizedHandler | null): void {
  onUnauthorized = handler
}

function isAuthCredentialPath(path: string): boolean {
  const normalized = (path.startsWith('/') ? path : `/${path}`).split('?')[0]
  return normalized === '/auth/login' || normalized === '/auth/register'
}

/** Prefer message, then ProblemDetails detail/title, then status text. */
export function parseApiErrorMessage(
  body: unknown,
  status: number,
  statusText: string,
): string {
  if (typeof body === 'object' && body !== null) {
    const obj = body as Record<string, unknown>
    for (const key of ['message', 'detail', 'title'] as const) {
      const value = obj[key]
      if (typeof value === 'string' && value.trim()) return value
    }
  }
  if (typeof body === 'string' && body.trim()) return body
  return statusText?.trim() || `Request failed (${status})`
}

async function readBody(response: Response): Promise<unknown> {
  const text = await response.text()
  if (!text) return null
  try {
    return JSON.parse(text) as unknown
  } catch {
    return text
  }
}

export async function apiFetch<T>(
  path: string,
  init: RequestInit = {},
): Promise<T> {
  const base = getApiBaseUrl().replace(/\/$/, '')
  const url = `${base}${path.startsWith('/') ? path : `/${path}`}`

  const headers = new Headers(init.headers)
  if (!headers.has('Content-Type') && init.body) {
    headers.set('Content-Type', 'application/json')
  }

  const session = loadSession()
  if (session?.token) {
    headers.set('Authorization', `Bearer ${session.token}`)
  }

  const response = await fetch(url, { ...init, headers })

  if (response.status === 401) {
    const body = await readBody(response)
    const message = parseApiErrorMessage(
      body,
      401,
      response.statusText || 'Unauthorized',
    )

    // Failed login/register must not clear session or redirect.
    // Only treat as expired session when a bearer token was actually sent.
    const sentToken = !!session?.token
    if (sentToken && !isAuthCredentialPath(path)) {
      clearSession()
      onUnauthorized?.()
    }

    throw new ApiError(401, message, body)
  }

  if (response.status === 204) {
    return undefined as T
  }

  const body = await readBody(response)

  if (!response.ok) {
    const message = parseApiErrorMessage(
      body,
      response.status,
      response.statusText || `Request failed (${response.status})`,
    )
    throw new ApiError(response.status, message, body)
  }

  return body as T
}
