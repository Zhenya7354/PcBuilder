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
            return await _context.Ram.Include(r => r.Brand).ToListAsync();
        }

        public async Task<Ram> GetRamByIdAsync(int id)
        {
            var ram = await _context.Ram.Include(r => r.Brand).FirstOrDefaultAsync(r => r.Id == id);
            if (ram is null)
                throw new KeyNotFoundException($"RAM with ID {id} not found.");

            return ram;
        }

        public async Task<Ram> AddRamAsync(RamCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var ram = new Ram
            {
                Name = dto.Name,
                BrandId = dto.BrandId,
                MemoryType = dto.MemoryType,
                CapacityGb = dto.CapacityGb,
                KitCount = dto.KitCount,
                SpeedMhz = dto.SpeedMhz,
                CasLatency = dto.CasLatency,
                Voltage = dto.Voltage,
                HasRgb = dto.HasRgb,
                HasEcc = dto.HasEcc,
                HeightMm = dto.HeightMm,
                PriceUsd = dto.PriceUsd
            };

            _context.Ram.Add(ram);
            await _context.SaveChangesAsync();
            return ram;
        }

        public async Task<Ram> UpdateRamAsync(int id, RamUpdateDto dto)
        {
            var ram = await _context.Ram.FindAsync(id);
            if (ram is null)
                throw new KeyNotFoundException($"RAM with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                ram.BrandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) ram.Name = dto.Name;
            if (dto.MemoryType.HasValue) ram.MemoryType = dto.MemoryType.Value;
            if (dto.CapacityGb.HasValue) ram.CapacityGb = dto.CapacityGb.Value;
            if (dto.KitCount.HasValue) ram.KitCount = dto.KitCount.Value;
            if (dto.SpeedMhz.HasValue) ram.SpeedMhz = dto.SpeedMhz.Value;
            if (dto.CasLatency.HasValue) ram.CasLatency = dto.CasLatency.Value;
            if (dto.Voltage.HasValue) ram.Voltage = dto.Voltage.Value;
            if (dto.HasRgb.HasValue) ram.HasRgb = dto.HasRgb.Value;
            if (dto.HasEcc.HasValue) ram.HasEcc = dto.HasEcc.Value;
            if (dto.HeightMm.HasValue) ram.HeightMm = dto.HeightMm.Value;
            if (dto.PriceUsd.HasValue) ram.PriceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return ram;
        }

        public async Task DeleteRamAsync(int id)
        {
            var ram = await _context.Ram.FindAsync(id);
            if (ram is null)
                throw new KeyNotFoundException($"RAM with ID {id} not found.");

            _context.Ram.Remove(ram);
            await _context.SaveChangesAsync();
        }

        private async Task EnsureBrandExistsAsync(int brandId)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.Id == brandId);
            if (!brandExists)
            {
                throw new KeyNotFoundException("Brand with the specified ID does not exist.");
            }
        }
    }
}
