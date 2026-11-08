import { Link, NavLink, Outlet, useLocation, useNavigate } from 'react-router'
import { useAuth } from '../auth/AuthContext'

const navClass = ({ isActive }: { isActive: boolean }) =>
  [
    'text-sm tracking-wide transition-colors',
    isActive ? 'text-accent' : 'text-steel-300 hover:text-steel-100',
  ].join(' ')

function focusBuilderSection() {
  const el = document.getElementById('builder')
  if (!el) return
  el.scrollIntoView({ behavior: 'smooth', block: 'start' })
  // Make the section reachable for keyboard users after scroll.
  if (!el.hasAttribute('tabindex')) el.setAttribute('tabindex', '-1')
  el.focus({ preventScroll: true })
}

export function AppShell() {
  const { isAuthenticated, session, logout } = useAuth()
  const location = useLocation()
  const navigate = useNavigate()

  function onBuildPc() {
    const onHome = location.pathname === '/'
    if (onHome) {
      focusBuilderSection()
      return
    }
    navigate('/#builder')
  }

  return (
    <div className="relative min-h-screen">
      <div
        aria-hidden
        className="pointer-events-none absolute inset-0 bg-[radial-gradient(ellipse_at_top,_#222831_0%,_transparent_55%),linear-gradient(160deg,_#0b0d10_0%,_#12151a_45%,_#0b0d10_100%)]"
      />
      <div
        aria-hidden
        className="pointer-events-none absolute inset-0 opacity-[0.04]"
        style={{
          backgroundImage:
            'repeating-linear-gradient(90deg, #fff 0 1px, transparent 1px 48px), repeating-linear-gradient(0deg, #fff 0 1px, transparent 1px 48px)',
        }}
      />

      <header className="relative z-10 border-b border-steel-800/80 bg-steel-950/70 backdrop-blur-sm">
        <div className="mx-auto flex max-w-6xl flex-wrap items-center gap-x-4 gap-y-3 px-4 py-4 sm:px-6">
          <Link
            to="/"
            className="shrink-0 font-semibold tracking-tight text-steel-100 transition-colors hover:text-accent"
          >
            PcBuilder
          </Link>

          <nav className="ml-6 flex flex-wrap items-center gap-x-5 gap-y-2 sm:ml-8">
            <NavLink to="/catalog" className={navClass}>
              Catalog
            </NavLink>
            <NavLink to="/builds" className={navClass}>
              Builds
            </NavLink>
            <NavLink to="/ai" className={navClass}>
              AI
            </NavLink>
            {!isAuthenticated ? (
              <NavLink to="/login" className={navClass}>
                Sign in
              </NavLink>
            ) : null}
          </nav>

          <div className="ml-auto flex min-w-0 max-w-full flex-wrap items-center justify-end gap-x-4 gap-y-2">
            <button
              type="button"
              onClick={onBuildPc}
              className="shrink-0 bg-accent px-4 py-2 text-sm font-semibold text-steel-950 transition-colors hover:bg-accent-dim"
            >
              Build PC
            </button>
            {isAuthenticated ? (
              <div
                aria-label="Account"
                className="flex min-w-0 max-w-full items-center gap-3"
              >
                {session?.email ? (
                  <span
                    className="min-w-0 max-w-[9rem] truncate font-mono text-xs text-steel-400 sm:max-w-[14rem]"
                    title={session.email}
                  >
                    {session.email}
                  </span>
                ) : null}
                <button
                  type="button"
                  onClick={logout}
                  className="shrink-0 text-sm text-steel-300 transition-colors hover:text-accent"
                >
                  Sign out
                </button>
              </div>
            ) : null}
          </div>
        </div>
      </header>

      <main className="relative z-10 mx-auto max-w-6xl px-4 py-8 sm:px-6 sm:py-10">
        <Outlet />
      </main>
    </div>
  )
}
