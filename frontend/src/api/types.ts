/** Shared catalog component shape (camelCase JSON from API). */
export interface Brand {
  id: number
  name: string
}

export interface ComponentBase {
  id: number
  brandId: number
  name: string
  brand?: Brand | null
  currency?: string | null
  price?: number | null
}

export interface Cpu extends ComponentBase {
  modelNumber?: string | null
  socket: string
  chipsetsSupported?: string[]
  cores: number
  threads: number
  baseClockGhz: number
  boostClockGhz?: number | null
  l3CacheMb?: number | null
  tdpWatts: number
  memoryType: string
  maxMemoryGb: number
  maxMemorySpeedMhz: number
  memoryChannels: number
  integratedGraphics: boolean
  igpuModel?: string | null
  pcieVersion?: string | null
  pcieLanes?: number | null
  includesCooler: boolean
  launchedYear?: number | null
}

export interface Gpu extends ComponentBase {
  gpuChip: string
  gpuInterface: string
  vramGb: number
  vramType: string
  baseClockMhz?: number | null
  boostClockMhz: number
  memoryBusBits: number
  memoryBandwithGb?: number | null
  tdpWatts: number
  recommendedPsuWattage: number
  powerConnectors?: string | null
  outputHdmi: number
  outputDp: number
  cardLengthMm?: number | null
  cardSlots: number
  colorScheme: string
}

export interface Motherboard extends ComponentBase {
  socket: string
  chipset: string
  formFactor: string
  memoryType: string
  memorySlots: number
  maxMemoryGb: number
  maxMemorySpeedMhz: number
  pcieX16Slots: number
  pcieX1Slots: number
  m2Slots: number
  sataPorts: number
  hasWifi: boolean
  hasBluetooth: boolean
  lanSpeedGbps: number
}

export interface Ram extends ComponentBase {
  memoryType: string
  capacityGb: number
  kitCount: number
  speedMhz: number
  casLatency?: number | null
  voltage?: number | null
  colorScheme: string
  hasEcc: boolean
  heightMm?: number | null
}

export interface Psu extends ComponentBase {
  wattage: number
  efficiency: string
  modularity: string
  atxVersion: string
  has16Pin: boolean
  epsConnectors: number
  sataConnectors: number
  pcie8PinConnectors: number
  fanSizeMm: number
  lengthMm?: number | null
}

export interface PcCase extends ComponentBase {
  supportedFormFactors: string[]
  maxGpuLengthMm: number
  maxCpuCoolerHeightMm: number
  maxPsuLengthMm: number
  driveBays35Inch: number
  driveBays25Inch: number
  frontUsbA: number
  frontUsbC: number
  radiatorSupportMm: string[]
  hasGlassPanel: boolean
  includedFans: number
  colorScheme: string
}

export interface CpuCooler extends ComponentBase {
  coolerType: string
  socketsSupported: string[]
  radiatorSizeMm?: number | null
  fanCount: number
  fanSizeMm: number
  maxTdpWatts: number
  heightMm?: number | null
  colorScheme: string
  noiseLevelDb?: number | null
}

export interface HardDrive extends ComponentBase {
  capacityGb: number
  driveInterface: string
  formFactor: string
  pcDriveType: string
  readSpeedMbS?: number | null
  writeSpeedMbs?: number | null
  rpm?: number | null
  cacheMb?: number | null
}

export interface PcMonitor extends ComponentBase {
  screenSizeInch: number
  resolutionWidth: number
  resolutionHeight: number
  panelType: string
  refreshRateHz: number
  responseTimeMs?: number | null
  hdrSupport?: string | null
  syncTechnologies: string[]
  hdmiPorts: number
  dpPorts: number
}

export type CatalogPart =
  | Cpu
  | Gpu
  | Motherboard
  | Ram
  | Psu
  | PcCase
  | CpuCooler
  | HardDrive
  | PcMonitor
  | ComponentBase

export type CatalogType =
  | 'cpus'
  | 'gpus'
  | 'motherboards'
  | 'rams'
  | 'psus'
  | 'pc-cases'
  | 'cpu-coolers'
  | 'hard-drives'
  | 'pc-monitors'
  | 'brands'

export type BuildSlotKey =
  | 'cpuId'
  | 'cpuCoolerId'
  | 'gpuId'
  | 'ramId'
  | 'hardDriveId'
  | 'motherboardId'
  | 'psuId'
  | 'caseId'
  | 'monitorId'

export interface BuildRequest {
  name?: string | null
  cpuId?: number | null
  cpuCoolerId?: number | null
  gpuId?: number | null
  ramId?: number | null
  hardDriveId?: number | null
  motherboardId?: number | null
  psuId?: number | null
  caseId?: number | null
  monitorId?: number | null
}

export interface BuildEntity {
  id: number
  name?: string | null
  cpuId?: number | null
  cpuCoolerId?: number | null
  gpuId?: number | null
  ramId?: number | null
  hardDriveId?: number | null
  motherboardId?: number | null
  psuId?: number | null
  caseId?: number | null
  monitorId?: number | null
  userId?: string | null
  cpu?: Cpu | null
  cpuCooler?: CpuCooler | null
  gpu?: Gpu | null
  ram?: Ram | null
  hardDrive?: HardDrive | null
  motherboard?: Motherboard | null
  psu?: Psu | null
  pcCase?: PcCase | null
  monitor?: PcMonitor | null
}

export type CompatibilitySeverity = 'Error' | 'Warning'

export interface CompatibilityIssue {
  field: string
  message: string
  severity: CompatibilitySeverity
}

export interface CompatibilityErrorBody {
  message: string
  issues: CompatibilityIssue[]
}

export interface AuthResponse {
  token: string
  email: string
  roles: string[]
  expiresAt: string
}

export interface MessageResponse {
  message: string
}

export type BuildRecommendationStatus =
  | 'Pending'
  | 'InProgress'
  | 'PartiallyCompleted'
  | 'Completed'
  | 'Failed'

export interface AiBuildRequest {
  prompt: string
  name?: string | null
  acceptAiSuggestedName?: boolean
}

export interface BuildRecommendationResult {
  build: BuildRequest
  notes: string[]
  status: BuildRecommendationStatus
}

export class ApiError extends Error {
  status: number
  body: unknown

  constructor(status: number, message: string, body?: unknown) {
    super(message)
    this.name = 'ApiError'
    this.status = status
    this.body = body
  }
}

export function isCompatibilityError(
  err: unknown,
): err is ApiError & { body: CompatibilityErrorBody } {
  if (!(err instanceof ApiError) || err.status !== 400) return false
  const body = err.body as CompatibilityErrorBody | null
  return (
    !!body &&
    typeof body === 'object' &&
    Array.isArray(body.issues)
  )
}
