using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class CpuCoolerService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync()
    {
        return await _context.CpuCooler.Include(c => c.Brand).ToListAsync();
    }

    public async Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id)
    {
        var cpuCooler = await _context.CpuCooler.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
        if (cpuCooler is null)
        {
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");
        }

        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreate dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var cpuCooler = new CpuCoolerEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            CoolerType = dto.CoolerType,
            SocketsSupported = dto.SocketsSupported,
            RadiatorSizeMm = dto.RadiatorSizeMm,
            FanCount = dto.FanCount,
            FanSizeMm = dto.FanSizeMm,
            MaxTdpWatts = dto.MaxTdpWatts,
            HeightMm = dto.HeightMm,
            HasRgb = dto.HasRgb,
            NoiseLevelDb = dto.NoiseLevelDb,
            PriceUsd = dto.PriceUsd
        };

        _context.CpuCooler.Add(cpuCooler);
        await _context.SaveChangesAsync();
        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdate dto)
    {
        var cpuCooler = await _context.CpuCooler.FindAsync(id);
        if (cpuCooler is null)
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        if (dto.BrandId.HasValue)
        {
            await EnsureBrandExistsAsync(dto.BrandId.Value);
            cpuCooler.BrandId = dto.BrandId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Name)) cpuCooler.Name = dto.Name;
        if (dto.CoolerType.HasValue) cpuCooler.CoolerType = dto.CoolerType.Value;
        if (dto.SocketsSupported is { Count: > 0 }) cpuCooler.SocketsSupported = dto.SocketsSupported;
        if (dto.RadiatorSizeMm.HasValue) cpuCooler.RadiatorSizeMm = dto.RadiatorSizeMm.Value;
        if (dto.FanCount.HasValue) cpuCooler.FanCount = dto.FanCount.Value;
        if (dto.FanSizeMm.HasValue) cpuCooler.FanSizeMm = dto.FanSizeMm.Value;
        if (dto.MaxTdpWatts.HasValue) cpuCooler.MaxTdpWatts = dto.MaxTdpWatts.Value;
        if (dto.HeightMm.HasValue) cpuCooler.HeightMm = dto.HeightMm.Value;
        if (dto.HasRgb.HasValue) cpuCooler.HasRgb = dto.HasRgb.Value;
        if (dto.NoiseLevelDb.HasValue) cpuCooler.NoiseLevelDb = dto.NoiseLevelDb.Value;
        if (dto.PriceUsd.HasValue) cpuCooler.PriceUsd = dto.PriceUsd.Value;

        await _context.SaveChangesAsync();
        return cpuCooler;
    }

    public async Task DeleteCpuCoolerAsync(int id)
    {
        var cpuCooler = await _context.CpuCooler.FindAsync(id);
        if (cpuCooler is null)
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        _context.CpuCooler.Remove(cpuCooler);
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
