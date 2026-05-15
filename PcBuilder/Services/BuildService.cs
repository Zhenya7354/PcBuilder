using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class BuildService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;
        public async Task<Build> GetBuildByIdAsync(string userId, int buildId)
        {
            return await BuildWithAllComponents().FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
                throw new KeyNotFoundException($"Build with Id {buildId} not found");
        }
        public async Task<List<Build>> GetUserBuildsAsync(string userId)
        {
            return await BuildWithAllComponents().Where(b => b.UserId == userId).ToListAsync();
        }
        public async Task<Build> AddBuildAsync(string userId, BuildDto dto)
        {
            var build = new Build
            {
                Name = dto.Name,
                CpuId = dto.CpuId,
                CpuCoolerId = dto.CpuCoolerId,
                GpuId = dto.GpuId,
                RamId = dto.RamId,
                HardDriveId = dto.HardDriveId,
                MotherboardId = dto.MotherboardId,
                PsuId = dto.PsuId,
                CaseId = dto.CaseId,
                MonitorId = dto.MonitorId,
                UserId = userId
            };
            _context.Build.Add(build);
            await _context.SaveChangesAsync();
            return build;
        }

        public async Task<Build> UpdateBuildAsync(int buildId, string userId, BuildDto dto)
        {
            var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
                throw new KeyNotFoundException($"Build with Id {buildId} for user with Id {userId} not found");

            if(!string.IsNullOrEmpty(dto.Name)) { build.Name = dto.Name; }
            if (dto.CpuId.HasValue) build.CpuId = dto.CpuId;
            if (dto.CpuCoolerId.HasValue) build.CpuCoolerId = dto.CpuCoolerId;
            if (dto.GpuId.HasValue) build.GpuId = dto.GpuId;
            if (dto.RamId.HasValue) build.RamId = dto.RamId;
            if (dto.HardDriveId.HasValue) build.HardDriveId = dto.HardDriveId;
            if (dto.MotherboardId.HasValue) build.MotherboardId = dto.MotherboardId;
            if (dto.PsuId.HasValue) build.PsuId = dto.PsuId;
            if (dto.CaseId.HasValue) build.CaseId = dto.CaseId;
            if (dto.MonitorId.HasValue) build.MonitorId = dto.MonitorId;

            await _context.SaveChangesAsync();
            return build;
        }

        public async Task DeleteBuildAsync(int buildId, string userId)
        {
            var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
                throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");
            _context.Remove(build);
            await _context.SaveChangesAsync();
        }

        public async Task<Build> SetComponentAsync(int buildId, string userId, BuildComponentDto dto)
        {
            var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
                throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");
            if (!await ComponentExists(dto.ComponentType, dto.ComponentId))
            {
                throw new KeyNotFoundException($"Component with Id {dto.ComponentId} not found for type {dto.ComponentType}");
            }
            ApplyComponent(build, dto.ComponentType, dto.ComponentId);
            await _context.SaveChangesAsync();
            return build;
        }
        public async Task<Build> RemoveComponentAsync(int buildId, string userId, BuildComponentType componentType)
        {
            var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
                throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");
            ApplyComponent(build, componentType, null);
            await _context.SaveChangesAsync();
            return build;
        }

        private void ApplyComponent(Build build, BuildComponentType componentType, int? componentId)
        {
            switch (componentType)
            {
                case BuildComponentType.Cpu:
                    build.CpuId = componentId;
                    break;
                case BuildComponentType.CpuCooler:
                    build.CpuCoolerId = componentId;
                    break;
                case BuildComponentType.Gpu:
                    build.GpuId = componentId;
                    break;
                case BuildComponentType.Ram:
                    build.RamId = componentId;
                    break;
                case BuildComponentType.HardDrive:
                    build.HardDriveId = componentId;
                    break;
                case BuildComponentType.Motherboard:
                    build.MotherboardId = componentId;
                    break;
                case BuildComponentType.Psu:
                    build.PsuId = componentId;
                    break;
                case BuildComponentType.PcCase:
                    build.CaseId = componentId;
                    break;
                case BuildComponentType.PcMonitor:
                    build.MonitorId = componentId;
                    break;
                default:
                    throw new ArgumentException($"Invalid component type: {componentType}");
            }
        }
        private async Task<bool> ComponentExists(BuildComponentType componentType, int? componentId)
        {
            return componentType switch
            {
                BuildComponentType.Cpu => await _context.Cpu.AnyAsync(c => c.Id == componentId),
                BuildComponentType.CpuCooler => await _context.CpuCooler.AnyAsync(c => c.Id == componentId),
                BuildComponentType.Gpu => await _context.Gpu.AnyAsync(g => g.Id == componentId),
                BuildComponentType.Ram => await _context.Ram.AnyAsync(r => r.Id == componentId),
                BuildComponentType.HardDrive => await _context.HardDrive.AnyAsync(h => h.Id == componentId),
                BuildComponentType.Motherboard => await _context.Motherboard.AnyAsync(m => m.Id == componentId),
                BuildComponentType.Psu => await _context.Psu.AnyAsync(p => p.Id == componentId),
                BuildComponentType.PcCase => await _context.PcCase.AnyAsync(c => c.Id == componentId),
                BuildComponentType.PcMonitor => await _context.PcMonitor.AnyAsync(m => m.Id == componentId),
                _ => throw new ArgumentException($"Invalid component type: {componentType}")
            };
        }

        private IQueryable<Build> BuildWithAllComponents()
        {
            return _context.Build.Include(b => b.Cpu)
                        .Include(b => b.CpuCooler)
                        .Include(b => b.Gpu)
                        .Include(b => b.Ram)
                        .Include(b => b.HardDrive)
                        .Include(b => b.Motherboard)
                        .Include(b => b.Psu)
                        .Include(b => b.PcCase)
                        .Include(b => b.Monitor);
        }
    }
}
