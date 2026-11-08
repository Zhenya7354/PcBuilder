import { BrowserRouter, Navigate, Route, Routes } from 'react-router'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { AuthProvider } from './auth/AuthContext'
import { ProtectedRoute } from './auth/ProtectedRoute'
import { BuildDraftProvider } from './build-draft'
import { AppShell } from './components/AppShell'
import { AiRecommend } from './pages/AiRecommend'
import { BuildEditor } from './pages/BuildEditor'
import { Builds } from './pages/Builds'
import { Catalog } from './pages/Catalog'
import { HomeBuilder } from './pages/HomeBuilder'
import { Login } from './pages/Login'
import { PartDetail } from './pages/PartDetail'
import { Register } from './pages/Register'

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 30_000,
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
})

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <AuthProvider onSessionEnd={() => queryClient.clear()}>
          <BuildDraftProvider>
            <Routes>
              <Route element={<AppShell />}>
                <Route index element={<HomeBuilder />} />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="catalog" element={<Catalog />} />
                <Route path="catalog/:type/:id" element={<PartDetail />} />
                <Route
                  path="builds"
                  element={
                    <ProtectedRoute>
                      <Builds />
                    </ProtectedRoute>
                  }
                />
                <Route
                  path="builds/new"
                  element={
                    <ProtectedRoute>
                      <BuildEditor />
                    </ProtectedRoute>
                  }
                />
                <Route
                  path="builds/:id"
                  element={
                    <ProtectedRoute>
                      <BuildEditor />
                    </ProtectedRoute>
                  }
                />
                <Route
                  path="ai"
                  element={
                    <ProtectedRoute>
                      <AiRecommend />
                    </ProtectedRoute>
                  }
                />
                <Route path="*" element={<Navigate to="/" replace />} />
              </Route>
            </Routes>
          </BuildDraftProvider>
        </AuthProvider>
      </BrowserRouter>
    </QueryClientProvider>
  )
}
