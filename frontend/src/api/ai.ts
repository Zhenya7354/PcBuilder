import { apiFetch } from './client'
import type { AiBuildRequest, BuildRecommendationResult } from './types'

export function recommendBuild(body: AiBuildRequest) {
  return apiFetch<BuildRecommendationResult>('/builds/ai/recommend', {
    method: 'POST',
    body: JSON.stringify(body),
  })
}
