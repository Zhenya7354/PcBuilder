using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class PsuService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<PsuEntity>> GetAllPsusAsync()
    {
        return await _context.Psu.Include(p => p.Brand).ToListAsync();
    }

    public async Task<PsuEntity> GetPsuByIdAsync(int id)
    {
        var psu = await _context.Psu.Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
        if (psu is null)
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        return psu;
    }

    public async Task<PsuEntity> AddPsuAsync(PsuCreate dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var psu = new PsuEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            Wattage = dto.Wattage,
            Efficiency = dto.Efficiency,
            Modularity = dto.Modularity,
            AtxVersion = dto.AtxVersion,
            Has16Pin = dto.Has16Pin,
            EpsConnectors = dto.EpsConnectors,
            SataConnectors = dto.SataConnectors,
            Pcie8PinConnectors = dto.Pcie8PinConnectors,
            FanSizeMm = dto.FanSizeMm,
            LengthMm = dto.LengthMm,
            PriceUsd = dto.PriceUsd
        };

        _context.Psu.Add(psu);
        await _context.SaveChangesAsync();
        return psu;
    }

    public async Task<PsuEntity> UpdatePsuAsync(int id, PsuUpdate dto)
    {
        var psu = await _context.Psu.FindAsync(id);
        if (psu is null)
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        if (dto.BrandId.HasValue)
        {
            await EnsureBrandExistsAsync(dto.BrandId.Value);
            psu.BrandId = dto.BrandId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Name)) psu.Name = dto.Name;
        if (dto.Wattage.HasValue) psu.Wattage = dto.Wattage.Value;
        if (dto.Efficiency.HasValue) psu.Efficiency = dto.Efficiency.Value;
        if (dto.Modularity.HasValue) psu.Modularity = dto.Modularity.Value;
        if (!string.IsNullOrWhiteSpace(dto.AtxVersion)) psu.AtxVersion = dto.AtxVersion;
        if (dto.Has16Pin.HasValue) psu.Has16Pin = dto.Has16Pin.Value;
        if (dto.EpsConnectors.HasValue) psu.EpsConnectors = dto.EpsConnectors.Value;
        if (dto.SataConnectors.HasValue) psu.SataConnectors = dto.SataConnectors.Value;
        if (dto.Pcie8PinConnectors.HasValue) psu.Pcie8PinConnectors = dto.Pcie8PinConnectors.Value;
        if (dto.FanSizeMm.HasValue) psu.FanSizeMm = dto.FanSizeMm.Value;
        if (dto.LengthMm.HasValue) psu.LengthMm = dto.LengthMm.Value;
        if (dto.PriceUsd.HasValue) psu.PriceUsd = dto.PriceUsd.Value;

        await _context.SaveChangesAsync();
        return psu;
    }

    public async Task DeletePsuAsync(int id)
    {
        var psu = await _context.Psu.FindAsync(id);
        if (psu is null)
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        _context.Psu.Remove(psu);
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
