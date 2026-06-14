using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class GpuService(IGpuRepository gpuRepository) : IGpuService
{
    private readonly IGpuRepository _gpuRepository = gpuRepository;

    public async Task<List<GpuEntity>> GetGpusAsync(CancellationToken cancellationToken)
    {
        return await _gpuRepository.GetAllGpusAsync(cancellationToken);
    }

    public async Task<GpuEntity> GetGpuById(int id, CancellationToken cancellationToken)
    {
        var gpu = await _gpuRepository.GetGpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Gpu not found");
        return gpu;
    }

    public async Task<GpuEntity> AddGpuAsync(GpuCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Gpu data is required");

        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

        var gpu = new GpuEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            GpuChip = dto.gpuChip,
            GpuInterface = dto.gpuInterface,
            VramGb = dto.vram_gb,
            VramType = dto.vramType,
            BaseClockMhz = dto.baseClockMhz,
            BoostClockMhz = dto.boostClockMhz,
            MemoryBusBits = dto.memoryBusBits,
            MemoryBandwithGb = dto.memoryBandwithGb,
            TdpWatts = dto.tdpWatts,
            RecommendedPsuWattage = dto.recommendedPsuWattage,
            PowerConnectors = dto.powerConnectors,
            OutputHdmi = dto.outputHdmi,
            OutputDp = dto.outputDp,
            CardLengthMm = dto.cardLengthMm,
            CardSlots = dto.cardSlots,
            ColorScheme = dto.colorScheme,
            Currency = dto.Currency,
            Price = dto.Price
        };
        await _gpuRepository.AddGpuAsync(gpu, cancellationToken);
        await _gpuRepository.SaveChangesAsync(cancellationToken);

        return gpu;
    }
    public async Task<GpuEntity> UpdateGpuAsync(int id, GpuUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Gpu data is required");

        var gpu = await _gpuRepository.GetGpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Gpu not found");

        await EnsureBrandExistsAsync(dto.BrandId ?? gpu.BrandId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dto.Name)) gpu.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.gpuChip)) gpu.GpuChip = dto.gpuChip;
        if (dto.gpuInterface.HasValue) gpu.GpuInterface = dto.gpuInterface.Value;
        if (dto.vram_gb.HasValue) gpu.VramGb = dto.vram_gb.Value;
        if (!string.IsNullOrWhiteSpace(dto.vramType)) gpu.VramType = dto.vramType;
        if (dto.baseClockMhz.HasValue) gpu.BaseClockMhz = dto.baseClockMhz.Value;
        if (dto.boostClockMhz.HasValue) gpu.BoostClockMhz = dto.boostClockMhz.Value;
        if (dto.memoryBusBits.HasValue) gpu.MemoryBusBits = dto.memoryBusBits.Value;
        if (dto.memoryBandwithGb.HasValue) gpu.MemoryBandwithGb = dto.memoryBandwithGb.Value;
        if (dto.tdpWatts.HasValue) gpu.TdpWatts = dto.tdpWatts.Value;
        if (dto.recommendedPsuWattage.HasValue) gpu.RecommendedPsuWattage = dto.recommendedPsuWattage.Value;
        if (!string.IsNullOrWhiteSpace(dto.powerConnectors)) gpu.PowerConnectors = dto.powerConnectors;
        if (dto.outputHdmi.HasValue) gpu.OutputHdmi = dto.outputHdmi.Value;
        if (dto.outputDp.HasValue) gpu.OutputDp = dto.outputDp.Value;
        if (dto.cardLengthMm.HasValue) gpu.CardLengthMm = dto.cardLengthMm.Value;
        if (dto.cardSlots.HasValue) gpu.CardSlots = dto.cardSlots.Value;
        if (dto.colorScheme.HasValue) gpu.ColorScheme = dto.colorScheme.Value;
        if(dto.Currency.HasValue) gpu.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) gpu.Price = dto.Price.Value;

        await _gpuRepository.SaveChangesAsync(cancellationToken);
        return gpu;
    }

    public async Task DeleteGpuAsync(int id, CancellationToken cancellationToken)
    {
        var gpu = await _gpuRepository.GetGpuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Gpu not found");
        await _gpuRepository.DeleteGpuAsync(gpu, cancellationToken);
        await _gpuRepository.SaveChangesAsync(cancellationToken);
    }
    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _gpuRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}


