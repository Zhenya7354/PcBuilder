import { useState, type FormEvent } from 'react'
import { Link, useLocation, useNavigate } from 'react-router'
import { getApiBaseUrl } from '../api/client'
import { ApiError } from '../api/types'
import { useAuth } from '../auth/AuthContext'

export function Login() {
  const { login } = useAuth()
  const navigate = useNavigate()
  const location = useLocation()
  const from =
    (location.state as { from?: string } | null)?.from &&
    (location.state as { from: string }).from !== '/login' &&
    (location.state as { from: string }).from !== '/register'
      ? (location.state as { from: string }).from
      : '/builds'

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState<string | null>(null)
  const [submitting, setSubmitting] = useState(false)

  async function onSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setSubmitting(true)
    try {
      await login(email.trim(), password)
      navigate(from, { replace: true })
    } catch (err) {
      if (err instanceof ApiError) {
        setError(err.message || 'Invalid email or password.')
      } else if (err instanceof TypeError) {
        setError(
          `Cannot reach the API at ${getApiBaseUrl()}. Start the backend and, for HTTPS, run: dotnet dev-certs https --trust (or set VITE_API_BASE_URL=http://localhost:5150).`,
        )
      } else if (err instanceof Error && err.message) {
        setError(err.message)
      } else {
        setError('Login failed. Check your credentials and try again.')
      }
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <div className="mx-auto max-w-md">
      <h1 className="text-3xl font-semibold tracking-tight text-steel-100">
        Sign in
      </h1>
      <p className="mt-2 text-sm text-steel-400">
        Access your builds and AI recommendations.
      </p>

      <form onSubmit={onSubmit} className="mt-8 space-y-4">
        <div>
          <label htmlFor="email" className="mb-1 block text-sm text-steel-300">
            Email
          </label>
          <input
            id="email"
            type="email"
            required
            autoComplete="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none focus:border-accent"
          />
        </div>
        <div>
          <label htmlFor="password" className="mb-1 block text-sm text-steel-300">
            Password
          </label>
          <input
            id="password"
            type="password"
            required
            minLength={8}
            autoComplete="current-password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full border border-steel-700 bg-steel-900 px-3 py-2.5 text-sm text-steel-100 outline-none focus:border-accent"
          />
        </div>

        {error ? (
          <p className="border border-danger/40 bg-danger-soft px-3 py-2 text-sm text-danger" role="alert">
            {error}
          </p>
        ) : null}

        <button
          type="submit"
          disabled={submitting}
          className="w-full bg-accent py-2.5 text-sm font-semibold text-steel-950 transition-colors hover:bg-accent-dim disabled:opacity-60"
        >
          {submitting ? 'Signing in…' : 'Sign in'}
        </button>
      </form>

      <p className="mt-6 text-sm text-steel-400">
        No account?{' '}
        <Link
          to="/register"
          state={{ from }}
          className="text-accent hover:underline"
        >
          Register
        </Link>
      </p>
    </div>
  )
}
