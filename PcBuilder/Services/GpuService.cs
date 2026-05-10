using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class GpuService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<Gpu>> GetGpusAsync()
        {
            return await _context.Gpu.Include(g => g.brand).ToListAsync();
        }

        public async Task<Gpu> GetGpuById(int id)
        {
            var gpu = await _context.Gpu.Include(g => g.brand).FirstOrDefaultAsync(g => g.id == id);
            if (gpu == null)
            {
                throw new ArgumentException($"GPU with ID {id} not found.");
            }
            return gpu;
        }

        public async Task<Gpu> AddGpuAsync(GpuCreateDto gpuDto)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.id == gpuDto.BrandId);
            if (!brandExists)
            {
                throw new ArgumentException("Brand with the specified ID does not exist.");
            }
            var gpu = new Gpu
            {
                name = gpuDto.Name,
                brandId = gpuDto.BrandId,
                gpuChip = gpuDto.gpuChip,
                gpuInterface = gpuDto.gpuInterface,
                vram_gb = gpuDto.vram_gb,
                vramType = gpuDto.vramType,
                baseClockMhz = gpuDto.baseClockMhz,
                boostClockMhz = gpuDto.boostClockMhz,
                memoryBusBits = gpuDto.memoryBusBits,
                memoryBandwithGb = gpuDto.memoryBandwithGb,
                tdpWatts = gpuDto.tdpWatts,
                recommendedPsuWattage = gpuDto.recommendedPsuWattage,
                powerConnectors = gpuDto.powerConnectors,
                outputHdmi = gpuDto.outputHdmi,
                outputDp = gpuDto.outputDp,
                cardLengthMm = gpuDto.cardLengthMm,
                cardSlots = gpuDto.cardSlots,
                hasRgb = gpuDto.hasRgb,
                price = gpuDto.price
            };
            _context.Gpu.Add(gpu);
            await _context.SaveChangesAsync();

            return gpu;
        }
        public async Task<Gpu> UpdateGpuAsync(int id, GpuUpdateDto gpuDto)
        {
            var gpu = await _context.Gpu.FindAsync(id);
            if(gpu is null) 
                throw new ArgumentException($"GPU with ID {id} not found.");

            if(gpuDto.BrandId.HasValue)
            {
                var brandExists = await _context.Brand.AnyAsync(b => b.id == gpuDto.BrandId.Value);
                if (!brandExists)
                {
                    throw new ArgumentException("Brand with the specified ID does not exist.");
                }
                gpu.brandId = gpuDto.BrandId.Value;
            }
            if (!string.IsNullOrWhiteSpace(gpuDto.Name)) gpu.name = gpuDto.Name;
            if (!string.IsNullOrWhiteSpace(gpuDto.gpuChip)) gpu.gpuChip = gpuDto.gpuChip;
            if (gpuDto.gpuInterface.HasValue) gpu.gpuInterface = gpuDto.gpuInterface.Value;
            if (gpuDto.vram_gb.HasValue) gpu.vram_gb = gpuDto.vram_gb.Value;
            if (!string.IsNullOrWhiteSpace(gpuDto.vramType)) gpu.vramType = gpuDto.vramType;
            if (gpuDto.baseClockMhz.HasValue) gpu.baseClockMhz = gpuDto.baseClockMhz.Value;
            if (gpuDto.boostClockMhz.HasValue) gpu.boostClockMhz = gpuDto.boostClockMhz.Value;
            if (gpuDto.memoryBusBits.HasValue) gpu.memoryBusBits = gpuDto.memoryBusBits.Value;
            if (gpuDto.memoryBandwithGb.HasValue) gpu.memoryBandwithGb = gpuDto.memoryBandwithGb.Value;
            if (gpuDto.tdpWatts.HasValue) gpu.tdpWatts = gpuDto.tdpWatts.Value;
            if (gpuDto.recommendedPsuWattage.HasValue) gpu.recommendedPsuWattage = gpuDto.recommendedPsuWattage.Value;
            if (!string.IsNullOrWhiteSpace(gpuDto.powerConnectors)) gpu.powerConnectors = gpuDto.powerConnectors;
            if (gpuDto.outputHdmi.HasValue) gpu.outputHdmi = gpuDto.outputHdmi.Value;
            if (gpuDto.outputDp.HasValue) gpu.outputDp = gpuDto.outputDp.Value;
            if (gpuDto.cardLengthMm.HasValue) gpu.cardLengthMm = gpuDto.cardLengthMm.Value;
            if (gpuDto.cardSlots.HasValue) gpu.cardSlots = gpuDto.cardSlots.Value;
            if (gpuDto.hasRgb.HasValue) gpu.hasRgb = gpuDto.hasRgb.Value;
            if (gpuDto.price.HasValue) gpu.price = gpuDto.price.Value;

            await _context.SaveChangesAsync();
            return gpu;
        }

        public async Task DeleteGpuAsync(int id)
        {
            var gpu = await _context.Gpu.FindAsync(id);
            if(gpu is null)
            {
                throw new ArgumentException($"GPU with ID {id} not found.");
            }
            _context.Gpu.Remove(gpu);
            await _context.SaveChangesAsync();
        }
    }
}
               
    