using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Services;

public class BuildService(PcDbContext context, CompatibilityCheckService compatibilityCheckService)
{
    private readonly PcDbContext _context = context;
    private readonly CompatibilityCheckService _compatibilityCheckService = compatibilityCheckService;
    public async Task<BuildEntity> GetBuildByIdAsync(string userId, int buildId)
    {
        return await BuildWithAllComponents().FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found");
    }
    public async Task<List<BuildEntity>> GetUserBuildsAsync(string userId)
    {
        return await BuildWithAllComponents().Where(b => b.UserId == userId).ToListAsync();
    }
    public async Task<BuildEntity> AddBuildAsync(string userId, Build dto)
    {
        var build = new BuildEntity
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

    public async Task<BuildEntity> UpdateBuildAsync(int buildId, string userId, Build dto)
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

    public async Task<BuildEntity> SetComponentAsync(int buildId, string userId, BuildComponent dto)
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
    public async Task<BuildEntity> RemoveComponentAsync(int buildId, string userId, BuildComponentType componentType)
    {
        var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");
        ApplyComponent(build, componentType, null);
        await _context.SaveChangesAsync();
        return build;
    }

    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksAsync(Build dto)
    {
        var checks = new List<(int? IdA, int? IdB, Func<int, int, Task<CompatibilityCheckResponse>> Check)> {
            (dto.CpuId, dto.MotherboardId, _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync),
            (dto.CpuId, dto.CpuCoolerId, _compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync),
            (dto.CpuId, dto.RamId, _compatibilityCheckService.CheckCpuToRamCompatibilityAsync),
            (dto.RamId, dto.MotherboardId, _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync),
            (dto.CaseId, dto.MotherboardId, _compatibilityCheckService.CheckCaseToMotherboardCompatibilityAsync),
            (dto.CaseId, dto.CpuCoolerId, _compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync),
            (dto.CaseId, dto.GpuId, _compatibilityCheckService.CheckCaseToGpuCompatibilityAsync),
            (dto.CaseId, dto.PsuId, _compatibilityCheckService.CheckCaseToPsuCompatibilityAsync),
            (dto.PsuId, dto.GpuId, _compatibilityCheckService.CheckPsuToGpuCompatibilityAsync)
        };

        var tasks = checks
            .Where(c => c.IdA.HasValue && c.IdB.HasValue)
            .Select(c => c.Check(c.IdA!.Value, c.IdB!.Value));

        var results =  await Task.WhenAll(tasks);
        return results.Where(r => !r.IsCompatible).SelectMany(r => r.Issues).ToList();
    }
    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksForUpdateAsync(int buildId, string userId, Build dto)
    {
        var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");

        var mergedDto = new Build
        {
            Name = dto.Name ?? build.Name,
            CpuId = dto.CpuId ?? build.CpuId,
            MotherboardId = dto.MotherboardId ?? build.MotherboardId,
            RamId = dto.RamId ?? build.RamId,
            GpuId = dto.GpuId ?? build.GpuId,
            CpuCoolerId = dto.CpuCoolerId ?? build.CpuCoolerId,
            CaseId = dto.CaseId ?? build.CaseId,
            PsuId = dto.PsuId ?? build.PsuId,
            MonitorId = dto.MonitorId ?? build.MonitorId,
            HardDriveId = dto.HardDriveId ?? build.HardDriveId
        };

        return await RunCompatibilityChecksAsync(mergedDto);
    }

    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksForComponentUpdateAsync(int buildId, string userId, BuildComponent dto)
    {
        var build = await _context.Build.FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {userId}");
        var mergedDto = new Build
        {
            Name = build.Name,
            CpuId = build.CpuId,
            MotherboardId = build.MotherboardId,
            RamId = build.RamId,
            GpuId = build.GpuId,
            CpuCoolerId = build.CpuCoolerId,
            CaseId = build.CaseId,
            PsuId = build.PsuId,
            MonitorId = build.MonitorId,
            HardDriveId = build.HardDriveId
        };
        switch (dto.ComponentType)
        {
            case BuildComponentType.Cpu: mergedDto.CpuId = dto.ComponentId; break;
            case BuildComponentType.CpuCooler: mergedDto.CpuCoolerId = dto.ComponentId; break;
            case BuildComponentType.Gpu: mergedDto.GpuId = dto.ComponentId; break;
            case BuildComponentType.Ram: mergedDto.RamId = dto.ComponentId; break;
            case BuildComponentType.HardDrive: mergedDto.HardDriveId = dto.ComponentId; break;
            case BuildComponentType.Motherboard: mergedDto.MotherboardId = dto.ComponentId; break;
            case BuildComponentType.Psu: mergedDto.PsuId = dto.ComponentId; break;
            case BuildComponentType.PcCase: mergedDto.CaseId = dto.ComponentId; break;
            case BuildComponentType.PcMonitor: mergedDto.MonitorId = dto.ComponentId; break;
            default: throw new ArgumentException($"Invalid component type: {dto.ComponentType}");
        };
        return await RunCompatibilityChecksAsync(mergedDto);
    }

    private void ApplyComponent(BuildEntity build, BuildComponentType componentType, int? componentId)
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

    private IQueryable<BuildEntity> BuildWithAllComponents()
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