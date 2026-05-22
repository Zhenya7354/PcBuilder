using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class CpuService(PcDbContext context)
{
    private readonly PcDbContext _context = context;
    public async Task<List<CpuEntity>> GetAllCpuAsync()
    {
        return await _context.Cpu.Include(c => c.Brand).ToListAsync();
    }
    public async Task<CpuEntity> GetCpuByIdAsync(int id)
    {
        var cpu = await _context.Cpu.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
        if (cpu == null)
        {
            throw new KeyNotFoundException($"CPU with ID {id} not found.");
        }
        return cpu;
    }
    public async Task<CpuEntity> AddCpuAsync(CpuCreate cpuDto)
    {
        var brandExists = await _context.Brand.AnyAsync(b => b.Id == cpuDto.BrandId);
        await EnsureBrandExistsAsync(cpuDto.BrandId);
        var cpu = new CpuEntity
        {
            Name = cpuDto.Name,
            BrandId = cpuDto.BrandId,
            ModelNumber = cpuDto.ModelNumber,
            Socket = cpuDto.Socket,
            ChipsetsSupported = cpuDto.ChipsetsSupported,
            Cores = cpuDto.Cores,
            Threads = cpuDto.Threads,
            BaseClockGhz = cpuDto.BaseClockGhz,
            BoostClockGhz = cpuDto.BoostClockGhz,
            L3CacheMb = cpuDto.L3CacheMb,
            TdpWatts = cpuDto.TdpWatts,
            MemoryType = cpuDto.MemoryType,
            MaxMemoryGb = cpuDto.MaxMemoryGb,
            MaxMemorySpeedMhz = cpuDto.MaxMemorySpeedMhz,
            MemoryChannels = cpuDto.MemoryChannels,
            IntegratedGraphics = cpuDto.IntegratedGraphics,
            IgpuModel = cpuDto.IgpuModel,
            PcieVersion = cpuDto.PcieVersion,
            PcieLanes = cpuDto.PcieLanes,
            IncludesCooler = cpuDto.IncludesCooler,
            LaunchedYear = cpuDto.LaunchedYear,
            PriceUsd = cpuDto.PriceUsd
        };
        _context.Cpu.Add(cpu);
        await _context.SaveChangesAsync();

        return cpu;
    }
    public async Task<CpuEntity> UpdateCpuAsync(int id, CpuUpdate cpuDto)
    {
        var cpu = await _context.Cpu.FindAsync(id);
        if (cpu is null)
            throw new KeyNotFoundException($"CPU with Id {id} not found");

        if (cpuDto.BrandId.HasValue)
        {
            await EnsureBrandExistsAsync(cpuDto.BrandId.Value);
            cpu.BrandId = cpuDto.BrandId.Value;
        }

        if (cpuDto.Socket.HasValue) cpu.Socket = cpuDto.Socket.Value;
        if (cpuDto.MemoryType.HasValue) cpu.MemoryType = cpuDto.MemoryType.Value;
        if (cpuDto.IntegratedGraphics.HasValue) cpu.IntegratedGraphics = cpuDto.IntegratedGraphics.Value;
        if (cpuDto.IncludesCooler.HasValue) cpu.IncludesCooler = cpuDto.IncludesCooler.Value;
        if (!string.IsNullOrWhiteSpace(cpuDto.Name)) cpu.Name = cpuDto.Name;
        if (!string.IsNullOrWhiteSpace(cpuDto.ModelNumber)) cpu.ModelNumber = cpuDto.ModelNumber;
        if (cpuDto.ChipsetsSupported is { Count: > 0 }) cpu.ChipsetsSupported = cpuDto.ChipsetsSupported;
        if (cpuDto.Cores.HasValue) cpu.Cores = cpuDto.Cores.Value;
        if (cpuDto.Threads.HasValue) cpu.Threads = cpuDto.Threads.Value;
        if (cpuDto.BaseClockGhz.HasValue) cpu.BaseClockGhz = cpuDto.BaseClockGhz.Value;
        if (cpuDto.BoostClockGhz.HasValue) cpu.BoostClockGhz = cpuDto.BoostClockGhz.Value;
        if (cpuDto.L3CacheMb.HasValue) cpu.L3CacheMb = cpuDto.L3CacheMb.Value;
        if (cpuDto.TdpWatts.HasValue) cpu.TdpWatts = cpuDto.TdpWatts.Value;
        if (cpuDto.MaxMemoryGb.HasValue) cpu.MaxMemoryGb = cpuDto.MaxMemoryGb.Value;
        if (cpuDto.MaxMemorySpeedMhz.HasValue) cpu.MaxMemorySpeedMhz = cpuDto.MaxMemorySpeedMhz.Value;
        if (cpuDto.MemoryChannels.HasValue) cpu.MemoryChannels = cpuDto.MemoryChannels.Value;
        if (!string.IsNullOrWhiteSpace(cpuDto.IgpuModel)) cpu.IgpuModel = cpuDto.IgpuModel;
        if (!string.IsNullOrWhiteSpace(cpuDto.PcieVersion)) cpu.PcieVersion = cpuDto.PcieVersion;
        if (cpuDto.PcieLanes.HasValue) cpu.PcieLanes = cpuDto.PcieLanes.Value;
        if (cpuDto.LaunchedYear.HasValue) cpu.LaunchedYear = cpuDto.LaunchedYear.Value;
        if (cpuDto.PriceUsd.HasValue) cpu.PriceUsd = cpuDto.PriceUsd.Value;

        await _context.SaveChangesAsync();
        return cpu;
    }
    public async Task DeleteCpuAsync(int id)
    {
        var cpu = await _context.Cpu.FindAsync(id);
        if (cpu == null)
        {
            throw new KeyNotFoundException($"CPU with ID {id} not found.");
        }
        _context.Cpu.Remove(cpu);
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
