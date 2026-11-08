import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useRef,
  useState,
  type ReactNode,
} from 'react'
import { useNavigate } from 'react-router'
import * as authApi from '../api/auth'
import {
  clearSession,
  loadSession,
  saveSession,
  setUnauthorizedHandler,
  type StoredSession,
} from '../api/client'

interface AuthContextValue {
  session: StoredSession | null
  isAuthenticated: boolean
  login: (email: string, password: string) => Promise<void>
  register: (email: string, password: string) => Promise<void>
  logout: () => void
}

const AuthContext = createContext<AuthContextValue | null>(null)

interface AuthProviderProps {
  children: ReactNode
  /** Clear React Query (and similar) when the session ends. */
  onSessionEnd?: () => void
}

export function AuthProvider({ children, onSessionEnd }: AuthProviderProps) {
  const [session, setSession] = useState<StoredSession | null>(() => loadSession())
  const navigate = useNavigate()
  const onSessionEndRef = useRef(onSessionEnd)
  onSessionEndRef.current = onSessionEnd

  const endSession = useCallback(() => {
    clearSession()
    setSession(null)
    onSessionEndRef.current?.()
  }, [])

  // Drop expired tokens on load (loadSession clears storage when past expiresAt).
  useEffect(() => {
    const valid = loadSession()
    if (!valid) {
      setSession((prev) => {
        if (prev) onSessionEndRef.current?.()
        return null
      })
    }
  }, [])

  useEffect(() => {
    setUnauthorizedHandler(() => {
      setSession(null)
      onSessionEndRef.current?.()
      navigate('/login', { replace: true })
    })
    return () => setUnauthorizedHandler(null)
  }, [navigate])

  const login = useCallback(async (email: string, password: string) => {
    const res = await authApi.login(email, password)
    const next: StoredSession = {
      token: res.token,
      email: res.email,
      roles: res.roles,
      expiresAt: res.expiresAt,
    }
    saveSession(next)
    setSession(next)
  }, [])

  const register = useCallback(async (email: string, password: string) => {
    await authApi.register(email, password)
  }, [])

  const logout = useCallback(() => {
    endSession()
  }, [endSession])

  const value = useMemo(
    () => ({
      session,
      isAuthenticated: !!session?.token,
      login,
      register,
      logout,
    }),
    [session, login, register, logout],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth(): AuthContextValue {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}
