import { apiFetch } from './client'
import type { BuildEntity, BuildRequest } from './types'

export function listBuilds() {
  return apiFetch<BuildEntity[]>('/builds')
}

export function getBuild(id: number) {
  return apiFetch<BuildEntity>(`/builds/${id}`)
}

export function createBuild(body: BuildRequest) {
  return apiFetch<BuildEntity>('/builds', {
    method: 'POST',
    body: JSON.stringify(body),
  })
}

export function updateBuild(id: number, body: BuildRequest) {
  return apiFetch<BuildEntity>(`/builds/${id}`, {
    method: 'PUT',
    body: JSON.stringify(body),
  })
}

export function deleteBuild(id: number) {
  return apiFetch<void>(`/builds/${id}`, { method: 'DELETE' })
}

export function buildEntityToRequest(build: BuildEntity): BuildRequest {
  return {
    name: build.name ?? undefined,
    cpuId: build.cpuId ?? undefined,
    cpuCoolerId: build.cpuCoolerId ?? undefined,
    gpuId: build.gpuId ?? undefined,
    ramId: build.ramId ?? undefined,
    hardDriveId: build.hardDriveId ?? undefined,
    motherboardId: build.motherboardId ?? undefined,
    psuId: build.psuId ?? undefined,
    caseId: build.caseId ?? undefined,
    monitorId: build.monitorId ?? undefined,
  }
}
