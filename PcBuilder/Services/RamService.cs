using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class RamService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<Ram>> GetAllRamAsync()
        {
            return await _context.Ram.Include(r => r.brand).ToListAsync();
        }

        public async Task<Ram> GetRamByIdAsync(int id)
        {
            var ram = await _context.Ram.Include(r => r.brand).FirstOrDefaultAsync(r => r.id == id);
            if (ram is null)
                throw new ArgumentException($"RAM with ID {id} not found.");

            return ram;
        }

        public async Task<Ram> AddRamAsync(RamCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var ram = new Ram
            {
                name = dto.Name,
                brandId = dto.BrandId,
                memoryType = dto.MemoryType,
                capacityGb = dto.CapacityGb,
                kitCount = dto.KitCount,
                speedMhz = dto.SpeedMhz,
                casLatency = dto.CasLatency,
                voltage = dto.Voltage,
                hasRgb = dto.HasRgb,
                hasEcc = dto.HasEcc,
                heightMm = dto.HeightMm,
                priceUsd = dto.PriceUsd
            };

            _context.Ram.Add(ram);
            await _context.SaveChangesAsync();
            return ram;
        }

        public async Task<Ram> UpdateRamAsync(int id, RamUpdateDto dto)
        {
            var ram = await _context.Ram.FindAsync(id);
            if (ram is null)
                throw new ArgumentException($"RAM with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                ram.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) ram.name = dto.Name;
            if (dto.MemoryType.HasValue) ram.memoryType = dto.MemoryType.Value;
            if (dto.CapacityGb.HasValue) ram.capacityGb = dto.CapacityGb.Value;
            if (dto.KitCount.HasValue) ram.kitCount = dto.KitCount.Value;
            if (dto.SpeedMhz.HasValue) ram.speedMhz = dto.SpeedMhz.Value;
            if (dto.CasLatency.HasValue) ram.casLatency = dto.CasLatency.Value;
            if (dto.Voltage.HasValue) ram.voltage = dto.Voltage.Value;
            if (dto.HasRgb.HasValue) ram.hasRgb = dto.HasRgb.Value;
            if (dto.HasEcc.HasValue) ram.hasEcc = dto.HasEcc.Value;
            if (dto.HeightMm.HasValue) ram.heightMm = dto.HeightMm.Value;
            if (dto.PriceUsd.HasValue) ram.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return ram;
        }

        public async Task DeleteRamAsync(int id)
        {
            var ram = await _context.Ram.FindAsync(id);
            if (ram is null)
                throw new ArgumentException($"RAM with ID {id} not found.");

            _context.Ram.Remove(ram);
            await _context.SaveChangesAsync();
        }

        private async Task EnsureBrandExistsAsync(int brandId)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.id == brandId);
            if (!brandExists)
            {
                throw new ArgumentException("Brand with the specified ID does not exist.");
            }
        }
    }
}
