using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class PsuService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<Psu>> GetAllPsusAsync()
        {
            return await _context.Psu.Include(p => p.brand).ToListAsync();
        }

        public async Task<Psu> GetPsuByIdAsync(int id)
        {
            var psu = await _context.Psu.Include(p => p.brand).FirstOrDefaultAsync(p => p.id == id);
            if (psu is null)
                throw new ArgumentException($"PSU with ID {id} not found.");

            return psu;
        }

        public async Task<Psu> AddPsuAsync(PsuCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var psu = new Psu
            {
                name = dto.Name,
                brandId = dto.BrandId,
                wattage = dto.Wattage,
                efficiency = dto.Efficiency,
                modularity = dto.Modularity,
                atxVersion = dto.AtxVersion,
                has16Pin = dto.Has16Pin,
                epsConnectors = dto.EpsConnectors,
                sataConnectors = dto.SataConnectors,
                pcie8PinConnectors = dto.Pcie8PinConnectors,
                fanSizeMm = dto.FanSizeMm,
                lengthMm = dto.LengthMm,
                priceUsd = dto.PriceUsd
            };

            _context.Psu.Add(psu);
            await _context.SaveChangesAsync();
            return psu;
        }

        public async Task<Psu> UpdatePsuAsync(int id, PsuUpdateDto dto)
        {
            var psu = await _context.Psu.FindAsync(id);
            if (psu is null)
                throw new ArgumentException($"PSU with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                psu.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) psu.name = dto.Name;
            if (dto.Wattage.HasValue) psu.wattage = dto.Wattage.Value;
            if (dto.Efficiency.HasValue) psu.efficiency = dto.Efficiency.Value;
            if (dto.Modularity.HasValue) psu.modularity = dto.Modularity.Value;
            if (!string.IsNullOrWhiteSpace(dto.AtxVersion)) psu.atxVersion = dto.AtxVersion;
            if (dto.Has16Pin.HasValue) psu.has16Pin = dto.Has16Pin.Value;
            if (dto.EpsConnectors.HasValue) psu.epsConnectors = dto.EpsConnectors.Value;
            if (dto.SataConnectors.HasValue) psu.sataConnectors = dto.SataConnectors.Value;
            if (dto.Pcie8PinConnectors.HasValue) psu.pcie8PinConnectors = dto.Pcie8PinConnectors.Value;
            if (dto.FanSizeMm.HasValue) psu.fanSizeMm = dto.FanSizeMm.Value;
            if (dto.LengthMm.HasValue) psu.lengthMm = dto.LengthMm.Value;
            if (dto.PriceUsd.HasValue) psu.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return psu;
        }

        public async Task DeletePsuAsync(int id)
        {
            var psu = await _context.Psu.FindAsync(id);
            if (psu is null)
                throw new ArgumentException($"PSU with ID {id} not found.");

            _context.Psu.Remove(psu);
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
