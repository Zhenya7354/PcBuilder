using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class CpuService(ICpuRepository cpuRepository) : ICpuService
{
    private readonly ICpuRepository _cpuRepository = cpuRepository;
    public async Task<List<CpuEntity>> GetAllCpuAsync(CancellationToken cancellationToken)
    {
        return await _cpuRepository.GetAllCpusAsync(cancellationToken);
    }
    public async Task<CpuEntity> GetCpuByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cpu = await _cpuRepository.GetCpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Cpu not found");
        return cpu;
    }
    public async Task<CpuEntity> AddCpuAsync(CpuCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null) 
            throw new ArgumentNullException("Cpu data is required");

        if (!await _cpuRepository.BrandExistsAsync(dto.BrandId, cancellationToken))
            throw new KeyNotFoundException("Brand not found");
        var cpu = new CpuEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            ModelNumber = dto.ModelNumber,
            Socket = dto.Socket,
            ChipsetsSupported = dto.ChipsetsSupported,
            Cores = dto.Cores,
            Threads = dto.Threads,
            BaseClockGhz = dto.BaseClockGhz,
            BoostClockGhz = dto.BoostClockGhz,
            L3CacheMb = dto.L3CacheMb,
            TdpWatts = dto.TdpWatts,
            MemoryType = dto.MemoryType,
            MaxMemoryGb = dto.MaxMemoryGb,
            MaxMemorySpeedMhz = dto.MaxMemorySpeedMhz,
            MemoryChannels = dto.MemoryChannels,
            IntegratedGraphics = dto.IntegratedGraphics,
            IgpuModel = dto.IgpuModel,
            PcieVersion = dto.PcieVersion,
            PcieLanes = dto.PcieLanes,
            IncludesCooler = dto.IncludesCooler,
            LaunchedYear = dto.LaunchedYear,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _cpuRepository.AddCpuAsync(cpu, cancellationToken);
        await _cpuRepository.SaveChangesAsync(cancellationToken);
        return cpu;
    }
    public async Task<CpuEntity> UpdateCpuAsync(int id, CpuUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Cpu data is required");

        var cpu = await _cpuRepository.GetCpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Cpu not found");

        await EnsureBrandExistsAsync(dto.BrandId ?? cpu.BrandId, cancellationToken);

        if (dto.Socket.HasValue) cpu.Socket = dto.Socket.Value;
        if (dto.MemoryType.HasValue) cpu.MemoryType = dto.MemoryType.Value;
        if (dto.IntegratedGraphics.HasValue) cpu.IntegratedGraphics = dto.IntegratedGraphics.Value;
        if (dto.IncludesCooler.HasValue) cpu.IncludesCooler = dto.IncludesCooler.Value;
        if (!string.IsNullOrWhiteSpace(dto.Name)) cpu.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.ModelNumber)) cpu.ModelNumber = dto.ModelNumber;
        if (dto.ChipsetsSupported is { Count: > 0 }) cpu.ChipsetsSupported = dto.ChipsetsSupported;
        if (dto.Cores.HasValue) cpu.Cores = dto.Cores.Value;
        if (dto.Threads.HasValue) cpu.Threads = dto.Threads.Value;
        if (dto.BaseClockGhz.HasValue) cpu.BaseClockGhz = dto.BaseClockGhz.Value;
        if (dto.BoostClockGhz.HasValue) cpu.BoostClockGhz = dto.BoostClockGhz.Value;
        if (dto.L3CacheMb.HasValue) cpu.L3CacheMb = dto.L3CacheMb.Value;
        if (dto.TdpWatts.HasValue) cpu.TdpWatts = dto.TdpWatts.Value;
        if (dto.MaxMemoryGb.HasValue) cpu.MaxMemoryGb = dto.MaxMemoryGb.Value;
        if (dto.MaxMemorySpeedMhz.HasValue) cpu.MaxMemorySpeedMhz = dto.MaxMemorySpeedMhz.Value;
        if (dto.MemoryChannels.HasValue) cpu.MemoryChannels = dto.MemoryChannels.Value;
        if (!string.IsNullOrWhiteSpace(dto.IgpuModel)) cpu.IgpuModel = dto.IgpuModel;
        if (!string.IsNullOrWhiteSpace(dto.PcieVersion)) cpu.PcieVersion = dto.PcieVersion;
        if (dto.PcieLanes.HasValue) cpu.PcieLanes = dto.PcieLanes.Value;
        if (dto.LaunchedYear.HasValue) cpu.LaunchedYear = dto.LaunchedYear.Value;
        if(dto.Currency.HasValue) cpu.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) cpu.Price = dto.Price.Value;

        await _cpuRepository.SaveChangesAsync(cancellationToken);
        return cpu;
    }
    public async Task DeleteCpuAsync(int id, CancellationToken cancellationToken)
    {
        var cpu = await _cpuRepository.GetCpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Cpu not found");
        await _cpuRepository.DeleteCpuAsync(cpu, cancellationToken);
        await _cpuRepository.SaveChangesAsync(cancellationToken);
    }
    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _cpuRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
