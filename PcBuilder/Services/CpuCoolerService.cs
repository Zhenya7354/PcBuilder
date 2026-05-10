using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class CpuCoolerService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<CpuCooler>> GetAllCpuCoolersAsync()
        {
            return await _context.CpuCooler.Include(c => c.brand).ToListAsync();
        }

        public async Task<CpuCooler> GetCpuCoolerByIdAsync(int id)
        {
            var cpuCooler = await _context.CpuCooler.Include(c => c.brand).FirstOrDefaultAsync(c => c.id == id);
            if (cpuCooler is null)
            {
                throw new ArgumentException($"CPU cooler with ID {id} not found.");
            }

            return cpuCooler;
        }

        public async Task<CpuCooler> AddCpuCoolerAsync(CpuCoolerCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var cpuCooler = new CpuCooler
            {
                name = dto.Name,
                brandId = dto.BrandId,
                coolerType = dto.CoolerType,
                socketsSupported = dto.SocketsSupported,
                radiatorSizeMm = dto.RadiatorSizeMm,
                fanCount = dto.FanCount,
                fanSizeMm = dto.FanSizeMm,
                maxTdpWatts = dto.MaxTdpWatts,
                heightMm = dto.HeightMm,
                hasRgb = dto.HasRgb,
                noiseLevelDb = dto.NoiseLevelDb,
                priceUsd = dto.PriceUsd
            };

            _context.CpuCooler.Add(cpuCooler);
            await _context.SaveChangesAsync();
            return cpuCooler;
        }

        public async Task<CpuCooler> UpdateCpuCoolerAsync(int id, CpuCoolerUpdateDto dto)
        {
            var cpuCooler = await _context.CpuCooler.FindAsync(id);
            if (cpuCooler is null)
                throw new ArgumentException($"CPU cooler with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                cpuCooler.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) cpuCooler.name = dto.Name;
            if (dto.CoolerType.HasValue) cpuCooler.coolerType = dto.CoolerType.Value;
            if (dto.SocketsSupported is { Count: > 0 }) cpuCooler.socketsSupported = dto.SocketsSupported;
            if (dto.RadiatorSizeMm.HasValue) cpuCooler.radiatorSizeMm = dto.RadiatorSizeMm.Value;
            if (dto.FanCount.HasValue) cpuCooler.fanCount = dto.FanCount.Value;
            if (dto.FanSizeMm.HasValue) cpuCooler.fanSizeMm = dto.FanSizeMm.Value;
            if (dto.MaxTdpWatts.HasValue) cpuCooler.maxTdpWatts = dto.MaxTdpWatts.Value;
            if (dto.HeightMm.HasValue) cpuCooler.heightMm = dto.HeightMm.Value;
            if (dto.HasRgb.HasValue) cpuCooler.hasRgb = dto.HasRgb.Value;
            if (dto.NoiseLevelDb.HasValue) cpuCooler.noiseLevelDb = dto.NoiseLevelDb.Value;
            if (dto.PriceUsd.HasValue) cpuCooler.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return cpuCooler;
        }

        public async Task DeleteCpuCoolerAsync(int id)
        {
            var cpuCooler = await _context.CpuCooler.FindAsync(id);
            if (cpuCooler is null)
                throw new ArgumentException($"CPU cooler with ID {id} not found.");

            _context.CpuCooler.Remove(cpuCooler);
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
