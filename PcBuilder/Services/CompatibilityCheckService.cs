using PcBuilder.Enums;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class CompatibilityCheckService(ICompatibilityCheckRepository repository) : ICompatibilityCheckService
{
    private readonly ICompatibilityCheckRepository _repository = repository;
    public async Task<CompatibilityCheckResponse> CheckCpuToMotherboardCompatibilityAsync(int cpuId, int motherboardId, CancellationToken cancellationToken)
    {
        var cpu = await _repository.GetCpuByIdAsync(cpuId, cancellationToken) ??
            throw new KeyNotFoundException($"Cpu with provided ID {cpuId} does not exist");
        var motherboard = await _repository.GetMotherboardByIdAsync(motherboardId, cancellationToken) ??
            throw new KeyNotFoundException($"Motherboard with provided ID {motherboardId} does not exist");
        var issues = new List<CompatibilityIssue>();

        if (motherboard.Socket != cpu.Socket)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.Socket),
                Message = $"CPU socket {cpu.Socket} is not compatible with motherboard socket {motherboard.Socket}",
                Severity = CompatibilitySeverity.Error
            });
        }
        if (!cpu.ChipsetsSupported.Contains(motherboard.Chipset))
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(motherboard.Chipset),
                Message = $"CPU does not support motherboard chipset {motherboard.Chipset}",
                Severity = CompatibilitySeverity.Error
            });
        }
        if (motherboard.MemoryType != cpu.MemoryType)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.MemoryType),
                Message = $"CPU memory type {cpu.MemoryType} is not compatible with motherboard memory type {motherboard.MemoryType}",
                Severity = CompatibilitySeverity.Error
            });
        }
        if (motherboard.MaxMemorySpeedMhz <= cpu.MaxMemorySpeedMhz)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.MaxMemorySpeedMhz),
                Message = $"CPU max memory speed {cpu.MaxMemorySpeedMhz} is is higher than motherboard max memory speed {motherboard.MaxMemorySpeedMhz}",
                Severity = CompatibilitySeverity.Warning
            });
        }

        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckCpuCoolerToCpuCompatibilityAsync(int cpuId, int cpuCoolerId, CancellationToken cancellationToken)
    {
        var cpu = await _repository.GetCpuByIdAsync(cpuId, cancellationToken) ??
            throw new KeyNotFoundException($"CPU with provided ID {cpuId} does not exist");
        var cpuCooler = await _repository.GetCpuCoolerByIdAsync(cpuCoolerId, cancellationToken) ??
            throw new KeyNotFoundException($"CPU cooler with provided ID {cpuCoolerId} does not exist");
        var issues = new List<CompatibilityIssue>();
        if (!cpuCooler.SocketsSupported.Contains(cpu.Socket))
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.Socket),
                Message = $"CPU cooler socket {cpuCooler.SocketsSupported} is not compatible with CPU socket {cpu.Socket}",
                Severity = CompatibilitySeverity.Error
            });
        }
        if (cpuCooler.MaxTdpWatts < cpu.TdpWatts)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.TdpWatts),
                Message = $"CPU cooler max TDP {cpuCooler.MaxTdpWatts} W is lower than CPU TDP {cpu.TdpWatts} W",
                Severity = CompatibilitySeverity.Warning
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckCpuToRamCompatibilityAsync(int cpuId, int ramId, CancellationToken cancellationToken)
    {
        var cpu = await _repository.GetCpuByIdAsync(cpuId, cancellationToken) ??
            throw new KeyNotFoundException($"CPU with provided ID {cpuId} does not exist");
        var ram = await _repository.GetRamByIdAsync(ramId, cancellationToken) ??
            throw new KeyNotFoundException($"RAM with provided ID {ramId} does not exist");
        var issues = new List<CompatibilityIssue>();

        if(ram.SpeedMhz > cpu.MaxMemorySpeedMhz)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.MaxMemorySpeedMhz),
                Message = $"CPU max memory speed {cpu.MaxMemorySpeedMhz} is lower than RAM speed {ram.SpeedMhz}",
                Severity = CompatibilitySeverity.Warning
            });
        }
        if(ram.KitCount != cpu.MemoryChannels)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpu.MemoryChannels),
                Message = $"CPU memory channels {cpu.MemoryChannels} do not match RAM kit count {ram.KitCount}. Ram will not run in optimal channel",
                Severity = CompatibilitySeverity.Warning
            });
        }
            return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckRamToMotherboardCompatibilityAsync(int ramId, int motherboardId, CancellationToken cancellationToken)
    {
        var ram = await _repository.GetRamByIdAsync(ramId, cancellationToken) ??
            throw new KeyNotFoundException($"RAM with provided ID {ramId} does not exist");
        var motherboard = await _repository.GetMotherboardByIdAsync(motherboardId, cancellationToken) ??
            throw new KeyNotFoundException($"Motherboard with provided ID {motherboardId} does not exist");
        var issues = new List<CompatibilityIssue>();
        if (motherboard.MemoryType != ram.MemoryType)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(ram.MemoryType),
                Message = $"RAM memory type {ram.MemoryType} is not compatible with motherboard memory type {motherboard.MemoryType}",
                Severity = CompatibilitySeverity.Error
            });
        }
        if (ram.SpeedMhz > motherboard.MaxMemorySpeedMhz)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(ram.SpeedMhz),
                Message = $"RAM speed {ram.SpeedMhz} is higher than motherboard max memory speed {motherboard.MaxMemorySpeedMhz}",
                Severity = CompatibilitySeverity.Warning
            });
        }
        if (ram.KitCount > motherboard.MemorySlots)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(ram.KitCount),
                Message = $"RAM module count {ram.KitCount} exceeds motherboard memory slots {motherboard.MemorySlots}",
                Severity = CompatibilitySeverity.Warning
            });
        }
        if (ram.CapacityGb * ram.KitCount > motherboard.MaxMemoryGb)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(ram.CapacityGb),
                Message = $"Total RAM capacity {ram.CapacityGb * ram.KitCount} GB exceeds motherboard max memory capacity {motherboard.MaxMemoryGb} GB",
                Severity = CompatibilitySeverity.Warning
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }

    public async Task<CompatibilityCheckResponse> CheckCaseToMotherboardCompatibilityAsync(int caseId, int motherboardId, CancellationToken cancellationToken)
    {
        var pcCase = await _repository.GetCaseByIdAsync(caseId, cancellationToken) ??
            throw new KeyNotFoundException($"PC case with provided ID {caseId} does not exist");
        var motherboard = await _repository.GetMotherboardByIdAsync(motherboardId, cancellationToken) ??
            throw new KeyNotFoundException($"Motherboard with provided ID {motherboardId} does not exist");
        var issues = new List<CompatibilityIssue>();

        if (!pcCase.SupportedFormFactors.Contains(motherboard.FormFactor))
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(motherboard.FormFactor),
                Message = $"PC case does not support motherboard form factor {motherboard.FormFactor}",
                Severity = CompatibilitySeverity.Error
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckCaseToCpuCoolerCompatibilityAsync(int caseId, int cpuCoolerId, CancellationToken cancellationToken)
    {
        var pcCase = await _repository.GetCaseByIdAsync(caseId, cancellationToken) ??
            throw new KeyNotFoundException($"PC case with provided ID {caseId} does not exist");
        var cpuCooler = await _repository.GetCpuCoolerByIdAsync(cpuCoolerId, cancellationToken) ??
            throw new KeyNotFoundException($"CPU cooler with provided ID {cpuCoolerId} does not exist");
        var issues = new List<CompatibilityIssue>();
        if (cpuCooler.HeightMm > pcCase.MaxCpuCoolerHeightMm)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(cpuCooler.HeightMm),
                Message = $"CPU cooler height {cpuCooler.HeightMm} mm exceeds PC case max CPU cooler height {pcCase.MaxCpuCoolerHeightMm} mm",
                Severity = CompatibilitySeverity.Error
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
                    Severity = CompatibilitySeverity.Error
                });
            }
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckCaseToGpuCompatibilityAsync(int caseId, int gpuId, CancellationToken cancellationToken)
    {
        var pcCase = await _repository.GetCaseByIdAsync(caseId, cancellationToken) ??
            throw new KeyNotFoundException($"PC case with provided ID {caseId} does not exist");
        var gpu = await _repository.GetGpuByIdAsync(gpuId, cancellationToken) ??
            throw new KeyNotFoundException($"GPU with provided ID {gpuId} does not exist");
        var issues = new List<CompatibilityIssue>();
        if (gpu.CardLengthMm > pcCase.MaxGpuLengthMm)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(gpu.CardLengthMm),
                Message = $"GPU length {gpu.CardLengthMm} mm exceeds PC case max GPU length {pcCase.MaxGpuLengthMm} mm",
                Severity = CompatibilitySeverity.Error
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }
    public async Task<CompatibilityCheckResponse> CheckCaseToPsuCompatibilityAsync(int caseId, int psuId, CancellationToken cancellationToken)
    {
        var pcCase = await _repository.GetCaseByIdAsync(caseId, cancellationToken) ??
            throw new KeyNotFoundException($"PC case with provided ID {caseId} does not exist");
        var psu = await _repository.GetPsuByIdAsync(psuId, cancellationToken) ??
            throw new KeyNotFoundException($"PSU with provided ID {psuId} does not exist");
        var issues = new List<CompatibilityIssue>();
        if (psu.LengthMm > pcCase.MaxPsuLengthMm)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(psu.LengthMm),
                Message = $"PSU length {psu.LengthMm} mm exceeds PC case max PSU length {pcCase.MaxPsuLengthMm} mm",
                Severity = CompatibilitySeverity.Error
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }

    public async Task<CompatibilityCheckResponse> CheckPsuToGpuCompatibilityAsync(int psuId, int gpuId, CancellationToken cancellationToken)
    {
        var psu = await _repository.GetPsuByIdAsync(psuId, cancellationToken) ??
            throw new KeyNotFoundException($"PSU with provided ID {psuId} does not exist");
        var gpu = await _repository.GetGpuByIdAsync(gpuId, cancellationToken) ??
            throw new KeyNotFoundException($"GPU with provided ID {gpuId} does not exist");

        var issues = new List<CompatibilityIssue>();
        if (psu.Wattage < gpu.RecommendedPsuWattage)
        {
            issues.Add(new CompatibilityIssue()
            {
                Field = nameof(psu.Wattage),
                Message = $"PSU wattage {psu.Wattage} W is lower than GPU {gpu.RecommendedPsuWattage} wattage",
                Severity = CompatibilitySeverity.Error
            });
        }
        return issues.Count == 0 ? CompatibilityCheckResponse.Success() : CompatibilityCheckResponse.Failure(issues);
    }

}