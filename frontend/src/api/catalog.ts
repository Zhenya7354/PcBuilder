import { apiFetch } from './client'
import type { Brand, CatalogPart, CatalogType, ComponentBase } from './types'

export const CATALOG_TYPES: Exclude<CatalogType, 'brands'>[] = [
  'cpus',
  'gpus',
  'motherboards',
  'rams',
  'psus',
  'pc-cases',
  'cpu-coolers',
  'hard-drives',
  'pc-monitors',
]

export const CATALOG_LABELS: Record<CatalogType, string> = {
  cpus: 'CPUs',
  gpus: 'GPUs',
  motherboards: 'Motherboards',
  rams: 'RAM',
  psus: 'PSUs',
  'pc-cases': 'Cases',
  'cpu-coolers': 'CPU Coolers',
  'hard-drives': 'Storage',
  'pc-monitors': 'Monitors',
  brands: 'Brands',
}

const ENDPOINTS: Record<CatalogType, string> = {
  cpus: '/cpus',
  gpus: '/gpus',
  motherboards: '/motherboards',
  rams: '/rams',
  psus: '/psus',
  'pc-cases': '/pc-cases',
  'cpu-coolers': '/cpu-coolers',
  'hard-drives': '/hard-drives',
  'pc-monitors': '/pc-monitors',
  brands: '/brands',
}

export function isCatalogType(value: string): value is CatalogType {
  return value in ENDPOINTS
}

export function listCatalog(type: CatalogType) {
  return apiFetch<CatalogPart[] | Brand[]>(ENDPOINTS[type])
}

export function getCatalogItem(type: Exclude<CatalogType, 'brands'>, id: number) {
  return apiFetch<CatalogPart>(`${ENDPOINTS[type]}/${id}`)
}

export function listParts(type: Exclude<CatalogType, 'brands'>) {
  return apiFetch<ComponentBase[]>(ENDPOINTS[type])
}

export function filterPartsByQuery<T extends ComponentBase>(
  parts: T[],
  query: string,
): T[] {
  const q = query.trim().toLowerCase()
  if (!q) return parts
  return parts.filter((p) => {
    const brand = p.brand?.name?.toLowerCase() ?? ''
    return p.name.toLowerCase().includes(q) || brand.includes(q)
  })
}

export function formatPrice(
  price?: number | null,
  currency?: string | null,
): string {
  if (price == null) return '—'
  const code = currency ?? 'USD'
  try {
    return new Intl.NumberFormat(undefined, {
      style: 'currency',
      currency: code,
      maximumFractionDigits: 0,
    }).format(price)
  } catch {
    return `${price} ${code}`
  }
}
