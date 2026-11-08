import type { BuildSlotKey, CatalogType } from '../api/types'

export interface SlotConfig {
  key: BuildSlotKey
  label: string
  catalogType: Exclude<CatalogType, 'brands'>
}

/** Canonical build slots in board order. */
export const BUILD_SLOTS: SlotConfig[] = [
  { key: 'cpuId', label: 'CPU', catalogType: 'cpus' },
  { key: 'cpuCoolerId', label: 'CPU Cooler', catalogType: 'cpu-coolers' },
  { key: 'motherboardId', label: 'Motherboard', catalogType: 'motherboards' },
  { key: 'ramId', label: 'RAM', catalogType: 'rams' },
  { key: 'gpuId', label: 'GPU', catalogType: 'gpus' },
  { key: 'hardDriveId', label: 'Storage', catalogType: 'hard-drives' },
  { key: 'psuId', label: 'PSU', catalogType: 'psus' },
  { key: 'caseId', label: 'Case', catalogType: 'pc-cases' },
  { key: 'monitorId', label: 'Monitor', catalogType: 'pc-monitors' },
]

const TYPE_TO_SLOT: Record<Exclude<CatalogType, 'brands'>, BuildSlotKey> = {
  cpus: 'cpuId',
  'cpu-coolers': 'cpuCoolerId',
  motherboards: 'motherboardId',
  rams: 'ramId',
  gpus: 'gpuId',
  'hard-drives': 'hardDriveId',
  psus: 'psuId',
  'pc-cases': 'caseId',
  'pc-monitors': 'monitorId',
}

export function catalogTypeToSlotKey(
  type: Exclude<CatalogType, 'brands'>,
): BuildSlotKey {
  return TYPE_TO_SLOT[type]
}

export function slotKeyToCatalogType(
  key: BuildSlotKey,
): Exclude<CatalogType, 'brands'> {
  const slot = BUILD_SLOTS.find((s) => s.key === key)
  if (!slot) throw new Error(`Unknown slot key: ${key}`)
  return slot.catalogType
}

export function pickCatalogUrl(catalogType: Exclude<CatalogType, 'brands'>) {
  return `/catalog?type=${catalogType}&pick=1`
}

export function isPickMode(value: string | null): boolean {
  return value === '1' || value === 'true'
}
