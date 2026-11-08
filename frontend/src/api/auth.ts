import { apiFetch } from './client'
import type { AuthResponse, MessageResponse } from './types'

export function register(email: string, password: string) {
  return apiFetch<MessageResponse>('/auth/register', {
    method: 'POST',
    body: JSON.stringify({ email, password }),
  })
}

export function login(email: string, password: string) {
  return apiFetch<AuthResponse>('/auth/login', {
    method: 'POST',
    body: JSON.stringify({ email, password }),
  })
}
