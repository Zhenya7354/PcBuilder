using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Services;

public class GpuService(IGpuRepository gpuRepository)
{
    private readonly IGpuRepository _gpuRepository = gpuRepository;

    public async Task<List<GpuEntity>> GetGpusAsync()
    {
        return await _gpuRepository.GetAllGpusAsync();
    }

    public async Task<GpuEntity> GetGpuById(int id)
    {
        var gpu = await _gpuRepository.GetGpuByIdAsync(id) ??
            throw new KeyNotFoundException("Gpu not found");
        return gpu;
    }

    public async Task<GpuEntity> AddGpuAsync(GpuCreate gpuDto)
    {
        if (!await _gpuRepository.BrandExistsAsync(gpuDto.BrandId))
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");

        var gpu = new GpuEntity
        {
            Name = gpuDto.Name,
            BrandId = gpuDto.BrandId,
            GpuChip = gpuDto.gpuChip,
            GpuInterface = gpuDto.gpuInterface,
            VramGb = gpuDto.vram_gb,
            VramType = gpuDto.vramType,
            BaseClockMhz = gpuDto.baseClockMhz,
            BoostClockMhz = gpuDto.boostClockMhz,
            MemoryBusBits = gpuDto.memoryBusBits,
            MemoryBandwithGb = gpuDto.memoryBandwithGb,
            TdpWatts = gpuDto.tdpWatts,
            RecommendedPsuWattage = gpuDto.recommendedPsuWattage,
            PowerConnectors = gpuDto.powerConnectors,
            OutputHdmi = gpuDto.outputHdmi,
            OutputDp = gpuDto.outputDp,
            CardLengthMm = gpuDto.cardLengthMm,
            CardSlots = gpuDto.cardSlots,
            HasRgb = gpuDto.hasRgb,
            Price = gpuDto.price
        };
        await _gpuRepository.AddGpuAsync(gpu);
        await _gpuRepository.SaveChangesAsync();

        return gpu;
    }
    public async Task<GpuEntity> UpdateGpuAsync(int id, GpuUpdate gpuDto)
    {
        var gpu = await _gpuRepository.GetGpuByIdAsync(id) ??
            throw new KeyNotFoundException("Gpu not found");

        if (!await _gpuRepository.BrandExistsAsync(gpuDto.BrandId ?? gpu.BrandId))
            throw new KeyNotFoundException("Brand not found");

        if (!string.IsNullOrWhiteSpace(gpuDto.Name)) gpu.Name = gpuDto.Name;
        if (!string.IsNullOrWhiteSpace(gpuDto.gpuChip)) gpu.GpuChip = gpuDto.gpuChip;
        if (gpuDto.gpuInterface.HasValue) gpu.GpuInterface = gpuDto.gpuInterface.Value;
        if (gpuDto.vram_gb.HasValue) gpu.VramGb = gpuDto.vram_gb.Value;
        if (!string.IsNullOrWhiteSpace(gpuDto.vramType)) gpu.VramType = gpuDto.vramType;
        if (gpuDto.baseClockMhz.HasValue) gpu.BaseClockMhz = gpuDto.baseClockMhz.Value;
        if (gpuDto.boostClockMhz.HasValue) gpu.BoostClockMhz = gpuDto.boostClockMhz.Value;
        if (gpuDto.memoryBusBits.HasValue) gpu.MemoryBusBits = gpuDto.memoryBusBits.Value;
        if (gpuDto.memoryBandwithGb.HasValue) gpu.MemoryBandwithGb = gpuDto.memoryBandwithGb.Value;
        if (gpuDto.tdpWatts.HasValue) gpu.TdpWatts = gpuDto.tdpWatts.Value;
        if (gpuDto.recommendedPsuWattage.HasValue) gpu.RecommendedPsuWattage = gpuDto.recommendedPsuWattage.Value;
        if (!string.IsNullOrWhiteSpace(gpuDto.powerConnectors)) gpu.PowerConnectors = gpuDto.powerConnectors;
        if (gpuDto.outputHdmi.HasValue) gpu.OutputHdmi = gpuDto.outputHdmi.Value;
        if (gpuDto.outputDp.HasValue) gpu.OutputDp = gpuDto.outputDp.Value;
        if (gpuDto.cardLengthMm.HasValue) gpu.CardLengthMm = gpuDto.cardLengthMm.Value;
        if (gpuDto.cardSlots.HasValue) gpu.CardSlots = gpuDto.cardSlots.Value;
        if (gpuDto.hasRgb.HasValue) gpu.HasRgb = gpuDto.hasRgb.Value;
        if (gpuDto.price.HasValue) gpu.Price = gpuDto.price.Value;

        await _gpuRepository.SaveChangesAsync();
        return gpu;
    }

    public async Task DeleteGpuAsync(int id)
    {
        var gpu = await _gpuRepository.GetGpuByIdAsync(id) ??
            throw new KeyNotFoundException("Gpu not found");
        await _gpuRepository.DeleteGpuAsync(gpu);
        await _gpuRepository.SaveChangesAsync();
    }
}


