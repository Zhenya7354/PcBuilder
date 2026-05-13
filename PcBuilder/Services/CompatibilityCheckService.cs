using PcBuilder.Data;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class CompatibilityCheckService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;
        public async Task<CompatibilityCheckResponse> CheckCpuToMotherboardCompatibilityAsync(int cpuId, int motherboardId)
        {
            var cpu = await _context.Cpu.FindAsync(cpuId) ??
                throw new ArgumentException($"Cpu with provided ID {cpuId} does not exist");
            var motherboard = await _context.Motherboard.FindAsync(motherboardId) ??
                throw new ArgumentException($"Motherboard with provided ID {motherboardId} does not exist");
            var issues = new List<CompatibilityIssue>();

            if (motherboard.Socket != cpu.Socket)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.Socket),
                    Message = $"CPU socket {cpu.Socket} is not compatible with motherboard socket {motherboard.Socket}",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (!cpu.ChipsetsSupported.Contains(motherboard.Chipset))
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(motherboard.Chipset),
                    Message = $"CPU does not support motherboard chipset {motherboard.Chipset}",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (motherboard.MemoryType != cpu.MemoryType)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.MemoryType),
                    Message = $"CPU memory type {cpu.MemoryType} is not compatible with motherboard memory type {motherboard.MemoryType}",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (motherboard.MaxMemorySpeedMhz <= cpu.MaxMemorySpeedMhz)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.MaxMemorySpeedMhz),
                    Message = $"CPU max memory speed {cpu.MaxMemorySpeedMhz} is is higher than motherboard max memory speed {motherboard.MaxMemorySpeedMhz}",
                    Severity = CompatibilityServerity.Warning
                });
            }

            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckCpuCoolerToCpuCompatibilityAsync(int cpuId, int cpuCoolerId)
        {
            var cpu = await _context.Cpu.FindAsync(cpuId) ??
                throw new ArgumentException($"CPU with provided ID {cpuId} does not exist");
            var cpuCooler = await _context.CpuCooler.FindAsync(cpuCoolerId) ??
                throw new ArgumentException($"CPU cooler with provided ID {cpuCoolerId} does not exist");
            var issues = new List<CompatibilityIssue>();
            if (cpuCooler.SocketsSupported.Contains(cpu.Socket))
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.Socket),
                    Message = $"CPU cooler socket {cpuCooler.CoolerType} is not compatible with CPU socket {cpu.Socket}",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (cpuCooler.MaxTdpWatts < cpu.TdpWatts)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.TdpWatts),
                    Message = $"CPU cooler max TDP {cpuCooler.MaxTdpWatts} W is lower than CPU TDP {cpu.TdpWatts} W",
                    Severity = CompatibilityServerity.Warning
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckCpuToRamCompatibility(int cpuId, int ramId)
        {
            var cpu = await _context.Cpu.FindAsync(cpuId) ??
                throw new ArgumentException($"CPU with provided ID {cpuId} does not exist");
            var ram = await _context.Ram.FindAsync(ramId) ??
                throw new ArgumentException($"RAM with provided ID {ramId} does not exist");
            var issues = new List<CompatibilityIssue>();

            if(ram.SpeedMhz > cpu.MaxMemorySpeedMhz)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.MaxMemorySpeedMhz),
                    Message = $"CPU max memory speed {cpu.MaxMemorySpeedMhz} is lower than RAM speed {ram.SpeedMhz}",
                    Severity = CompatibilityServerity.Warning
                });
            }
            if(ram.KitCount != cpu.MemoryChannels)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpu.MemoryChannels),
                    Message = $"CPU memory channels {cpu.MemoryChannels} do not match RAM kit count {ram.KitCount}. Ram will not run in optimal channel",
                    Severity = CompatibilityServerity.Warning
                });
            }
                return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckRamToMotherboardCompatibilityAsync(int ramId, int motherboardId)
        {
            var ram = await _context.Ram.FindAsync(ramId) ??
                throw new ArgumentException($"RAM with provided ID {ramId} does not exist");
            var motherboard = await _context.Motherboard.FindAsync(motherboardId) ??
                throw new ArgumentException($"Motherboard with provided ID {motherboardId} does not exist");
            var issues = new List<CompatibilityIssue>();
            if (motherboard.MemoryType != ram.MemoryType)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(ram.MemoryType),
                    Message = $"RAM memory type {ram.MemoryType} is not compatible with motherboard memory type {motherboard.MemoryType}",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (ram.SpeedMhz > motherboard.MaxMemorySpeedMhz)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(ram.SpeedMhz),
                    Message = $"RAM speed {ram.SpeedMhz} is higher than motherboard max memory speed {motherboard.MaxMemorySpeedMhz}",
                    Severity = CompatibilityServerity.Warning
                });
            }
            if (ram.KitCount > motherboard.MemorySlots)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(ram.KitCount),
                    Message = $"RAM module count {ram.KitCount} exceeds motherboard memory slots {motherboard.MemorySlots}",
                    Severity = CompatibilityServerity.Warning
                });
            }
            if (ram.CapacityGb * ram.KitCount > motherboard.MaxMemoryGb)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(ram.CapacityGb),
                    Message = $"Total RAM capacity {ram.CapacityGb * ram.KitCount} GB exceeds motherboard max memory capacity {motherboard.MaxMemoryGb} GB",
                    Severity = CompatibilityServerity.Warning
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }

        public async Task<CompatibilityCheckResponse> CheckCaseToMotherboardCompatibility(int caseId, int motherboardId)
        {
            var pcCase = await _context.PcCase.FindAsync(caseId) ??
                throw new ArgumentException($"PC case with provided ID {caseId} does not exist");
            var motherboard = await _context.Motherboard.FindAsync(motherboardId) ??
                throw new ArgumentException($"Motherboard with provided ID {motherboardId} does not exist");
            var issues = new List<CompatibilityIssue>();

            if (!pcCase.SupportedFormFactors.Contains(motherboard.FormFactor))
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(motherboard.FormFactor),
                    Message = $"PC case does not support motherboard form factor {motherboard.FormFactor}",
                    Severity = CompatibilityServerity.Error
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckCaseToCpuCoolerCompatibility(int caseId, int cpuCoolerId)
        {
            var pcCase = await _context.PcCase.FindAsync(caseId) ??
                throw new ArgumentException($"PC case with provided ID {caseId} does not exist");
            var cpuCooler = await _context.CpuCooler.FindAsync(cpuCoolerId) ??
                throw new ArgumentException($"CPU cooler with provided ID {cpuCoolerId} does not exist");
            var issues = new List<CompatibilityIssue>();
            if (cpuCooler.HeightMm > pcCase.MaxCpuCoolerHeightMm)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(cpuCooler.HeightMm),
                    Message = $"CPU cooler height {cpuCooler.HeightMm} mm exceeds PC case max CPU cooler height {pcCase.MaxCpuCoolerHeightMm} mm",
                    Severity = CompatibilityServerity.Error
                });
            }
            if (cpuCooler.CoolerType == Enums.CoolerType.Liquid)
            {
                if (!pcCase.RadiatorSupportMm.Contains(cpuCooler.RadiatorSizeMm.ToString() ?? "0"))
                {
                    issues.Add(new CompatibilityIssue()
                    {
                        Field = nameof(cpuCooler.RadiatorSizeMm),
                        Message = $"CPU cooler radiator size {cpuCooler.RadiatorSizeMm} mm is not supported by PC case",
                        Severity = CompatibilityServerity.Error
                    });
                }
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckCaseToGpuCompatibility(int caseId, int gpuId)
        {
            var pcCase = await _context.PcCase.FindAsync(caseId) ??
                throw new ArgumentException($"PC case with provided ID {caseId} does not exist");
            var gpu = await _context.Gpu.FindAsync(gpuId) ??
                throw new ArgumentException($"GPU with provided ID {gpuId} does not exist");
            var issues = new List<CompatibilityIssue>();
            if (gpu.CardLengthMm > pcCase.MaxGpuLengthMm)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(gpu.CardLengthMm),
                    Message = $"GPU length {gpu.CardLengthMm} mm exceeds PC case max GPU length {pcCase.MaxGpuLengthMm} mm",
                    Severity = CompatibilityServerity.Error
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }
        public async Task<CompatibilityCheckResponse> CheckCaseToPsuCompatibility(int caseId, int psuId)
        {
            var pcCase = await _context.PcCase.FindAsync(caseId) ??
                throw new ArgumentException($"PC case with provided ID {caseId} does not exist");
            var psu = await _context.Psu.FindAsync(psuId) ??
                throw new ArgumentException($"PSU with provided ID {psuId} does not exist");
            var issues = new List<CompatibilityIssue>();
            if (psu.LengthMm > pcCase.MaxPsuLengthMm)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(psu.LengthMm),
                    Message = $"PSU length {psu.LengthMm} mm exceeds PC case max PSU length {pcCase.MaxPsuLengthMm} mm",
                    Severity = CompatibilityServerity.Error
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }

        public async Task<CompatibilityCheckResponse> CheckPsuToGpuCompatibility(int psuId, int gpuId)
        {
            var psu = await _context.Psu.FindAsync(psuId) ??
                throw new ArgumentException($"PSU with provided ID {psuId} does not exist");
            var gpu = await _context.Gpu.FindAsync(gpuId) ??
                throw new ArgumentException($"GPU with provided ID {gpuId} does not exist");
            
            var issues = new List<CompatibilityIssue>();
            if (psu.Wattage < gpu.RecommendedPsuWattage)
            {
                issues.Add(new CompatibilityIssue()
                {
                    Field = nameof(psu.Wattage),
                    Message = $"PSU wattage {psu.Wattage} W is lower than GPU {gpu.RecommendedPsuWattage} wattage",
                    Severity = CompatibilityServerity.Error
                });
            }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
        }

    }
}

