using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class CpuService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;
        public async Task<List<Cpu>> GetAllCpuAsync()
        {
            return await _context.Cpu.Include(c => c.brand).ToListAsync();
        }
        public async Task<Cpu> GetCpuByIdAsync(int id)
        {
            var cpu = await _context.Cpu.Include(c => c.brand).FirstOrDefaultAsync(c => c.id == id);
            if (cpu == null)
            {
                throw new ArgumentException($"CPU with ID {id} not found.");
            }
            return cpu;
        }
        public async Task<Cpu> AddCpuAsync(CpuCreateDto cpuDto)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.id == cpuDto.BrandId);
            if (!brandExists)
            {
                throw new ArgumentException("Brand with the specified ID does not exist.");
            }
            var cpu = new Cpu
            {
                name = cpuDto.Name,
                brandId = cpuDto.BrandId,
                modelNumber = cpuDto.ModelNumber,
                socket = cpuDto.Socket,
                chipsetsSupported = cpuDto.ChipsetsSupported,
                cores = cpuDto.Cores,
                threads = cpuDto.Threads,
                baseClockGhz = cpuDto.BaseClockGhz,
                boostClockGhz = cpuDto.BoostClockGhz,
                l3CacheMb = cpuDto.L3CacheMb,
                tdpWatts = cpuDto.TdpWatts,
                memoryType = cpuDto.MemoryType,
                maxMemoryGb = cpuDto.MaxMemoryGb,
                maxMemorySpeedMhz = cpuDto.MaxMemorySpeedMhz,
                memoryChannels = cpuDto.MemoryChannels,
                integratedGraphics = cpuDto.IntegratedGraphics,
                igpuModel = cpuDto.IgpuModel,
                pcieVersion = cpuDto.PcieVersion,
                pcieLanes = cpuDto.PcieLanes,
                includesCooler = cpuDto.IncludesCooler,
                launchedYear = cpuDto.LaunchedYear,
                priceUsd = cpuDto.PriceUsd
            };
            _context.Cpu.Add(cpu);
            await _context.SaveChangesAsync();

            return cpu;
        }
        public async Task<Cpu> UpdateCpuAsync(int id, CpuUpdateDto cpuDto)
        {
            var cpu = await _context.Cpu.FindAsync(id);
            if (cpu is null)
                throw new ArgumentException("CPU with the specified ID does not exist.");

            if (cpuDto.BrandId.HasValue)
            {
                var brandExists = await _context.Brand.AnyAsync(b => b.id == cpuDto.BrandId.Value);
                if (!brandExists)
                {
                    throw new ArgumentException("Brand with the specified ID does not exist.");
                }

                cpu.brandId = cpuDto.BrandId.Value;
            }

            if (cpuDto.Socket.HasValue) cpu.socket = cpuDto.Socket.Value;
            if (cpuDto.MemoryType.HasValue) cpu.memoryType = cpuDto.MemoryType.Value;
            if (cpuDto.IntegratedGraphics.HasValue) cpu.integratedGraphics = cpuDto.IntegratedGraphics.Value;
            if (cpuDto.IncludesCooler.HasValue) cpu.includesCooler = cpuDto.IncludesCooler.Value;
            if (!string.IsNullOrWhiteSpace(cpuDto.Name)) cpu.name = cpuDto.Name;
            if (!string.IsNullOrWhiteSpace(cpuDto.ModelNumber)) cpu.modelNumber = cpuDto.ModelNumber;
            if (cpuDto.ChipsetsSupported is { Count: > 0 }) cpu.chipsetsSupported = cpuDto.ChipsetsSupported;
            if (cpuDto.Cores.HasValue) cpu.cores = cpuDto.Cores.Value;
            if (cpuDto.Threads.HasValue) cpu.threads = cpuDto.Threads.Value;
            if (cpuDto.BaseClockGhz.HasValue) cpu.baseClockGhz = cpuDto.BaseClockGhz.Value;
            if (cpuDto.BoostClockGhz.HasValue) cpu.boostClockGhz = cpuDto.BoostClockGhz.Value;
            if (cpuDto.L3CacheMb.HasValue) cpu.l3CacheMb = cpuDto.L3CacheMb.Value;
            if (cpuDto.TdpWatts.HasValue) cpu.tdpWatts = cpuDto.TdpWatts.Value;
            if (cpuDto.MaxMemoryGb.HasValue) cpu.maxMemoryGb = cpuDto.MaxMemoryGb.Value;
            if (cpuDto.MaxMemorySpeedMhz.HasValue) cpu.maxMemorySpeedMhz = cpuDto.MaxMemorySpeedMhz.Value;
            if (cpuDto.MemoryChannels.HasValue) cpu.memoryChannels = cpuDto.MemoryChannels.Value;
            if (!string.IsNullOrWhiteSpace(cpuDto.IgpuModel)) cpu.igpuModel = cpuDto.IgpuModel;
            if (!string.IsNullOrWhiteSpace(cpuDto.PcieVersion)) cpu.pcieVersion = cpuDto.PcieVersion;
            if (cpuDto.PcieLanes.HasValue) cpu.pcieLanes = cpuDto.PcieLanes.Value;
            if (cpuDto.LaunchedYear.HasValue) cpu.launchedYear = cpuDto.LaunchedYear.Value;
            if (cpuDto.PriceUsd.HasValue) cpu.priceUsd = cpuDto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return cpu;
        }
        public async Task DeleteCpuAsync(int id)
        {
            var cpu = await _context.Cpu.FindAsync(id);
            if (cpu == null)
            {
                throw new ArgumentException($"CPU with ID {id} not found.");
            }
            _context.Cpu.Remove(cpu);
            await _context.SaveChangesAsync();
        }
    }
}
