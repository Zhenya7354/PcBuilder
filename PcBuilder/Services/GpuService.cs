using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services;

public class GpuService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<Gpu>> GetGpusAsync()
    {
        return await _context.Gpu.Include(g => g.Brand).ToListAsync();
    }

    public async Task<Gpu> GetGpuById(int id)
    {
        var gpu = await _context.Gpu.Include(g => g.Brand).FirstOrDefaultAsync(g => g.Id == id);
        if (gpu == null)
        {
            throw new KeyNotFoundException($"GPU with ID {id} not found.");
        }
        return gpu;
    }

    public async Task<Gpu> AddGpuAsync(GpuCreateDto gpuDto)
    {
        var brandExists = await _context.Brand.AnyAsync(b => b.Id == gpuDto.BrandId);
        if (!brandExists)
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
        var gpu = new Gpu
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
        _context.Gpu.Add(gpu);
        await _context.SaveChangesAsync();

        return gpu;
    }
    public async Task<Gpu> UpdateGpuAsync(int id, GpuUpdateDto gpuDto)
    {
        var gpu = await _context.Gpu.FindAsync(id);
        if(gpu is null) 
            throw new KeyNotFoundException($"GPU with ID {id} not found.");

        if(gpuDto.BrandId.HasValue)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.Id == gpuDto.BrandId.Value);
            if (!brandExists)
            {
                throw new KeyNotFoundException("Brand with the specified ID does not exist.");
            }
            gpu.BrandId = gpuDto.BrandId.Value;
        }
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

        await _context.SaveChangesAsync();
        return gpu;
    }

    public async Task DeleteGpuAsync(int id)
    {
        var gpu = await _context.Gpu.FindAsync(id);
        if(gpu is null)
        {
            throw new KeyNotFoundException($"GPU with ID {id} not found.");
        }
        _context.Gpu.Remove(gpu);
        await _context.SaveChangesAsync();
    }
}
               
    
