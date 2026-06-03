using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Services.Interfaces;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Services;

public class BuildService : IBuildService
{
    private readonly IBuildRepository _buildRepository;
    private readonly ICompatibilityCheckService _compatibilityCheckService;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly UserContextResponse _userContext;

    public BuildService(IBuildRepository buildRepository, ICompatibilityCheckService compatibilityCheckService, IUserContextAccessor userContextAccessor)
    {
        _buildRepository = buildRepository;
        _compatibilityCheckService = compatibilityCheckService;
        _userContextAccessor = userContextAccessor;
        _userContext = userContextAccessor.GetUserContext();
    }

    public async Task<BuildEntity> GetBuildByIdAsync(int buildId)
    {
        return await _buildRepository.GetByIdAsync(buildId,_userContext.UserId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found");
    }
    public async Task<List<BuildEntity>> GetUserBuildsAsync()
    {
        return await _buildRepository.GetAllAsync(_userContext.UserId);
    }
    public async Task<BuildEntity> AddBuildAsync(BuildRequest dto)
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
            UserId = _userContext.UserId
        };
        await _buildRepository.AddBuildAsync(build);
        await _buildRepository.SaveChangesAsync();
        return build;
    }

    public async Task<BuildEntity> UpdateBuildAsync(int buildId,BuildRequest dto)
    {
        var build = await GetBuildById(buildId);

        if (!string.IsNullOrEmpty(dto.Name)) { build.Name = dto.Name; }
        if (dto.CpuId.HasValue) build.CpuId = dto.CpuId;
        if (dto.CpuCoolerId.HasValue) build.CpuCoolerId = dto.CpuCoolerId;
        if (dto.GpuId.HasValue) build.GpuId = dto.GpuId;
        if (dto.RamId.HasValue) build.RamId = dto.RamId;
        if (dto.HardDriveId.HasValue) build.HardDriveId = dto.HardDriveId;
        if (dto.MotherboardId.HasValue) build.MotherboardId = dto.MotherboardId;
        if (dto.PsuId.HasValue) build.PsuId = dto.PsuId;
        if (dto.CaseId.HasValue) build.CaseId = dto.CaseId;
        if (dto.MonitorId.HasValue) build.MonitorId = dto.MonitorId;

        await _buildRepository.SaveChangesAsync();
        return build;
    }

    public async Task DeleteBuildAsync(int buildId)
    {
        var build = await GetBuildById(buildId);
        await _buildRepository.DeleteBuildAsync(build);
        await _buildRepository.SaveChangesAsync();
    }

    public async Task<BuildEntity> SetComponentAsync(int buildId, BuildComponentRequest dto)
    {
        var build = await GetBuildById(buildId);
        if (!await ComponentExists(dto.ComponentType, dto.ComponentId))
        {
            throw new KeyNotFoundException($"Component with Id {dto.ComponentId} not found for type {dto.ComponentType}");
        }
        ApplyComponent(build, dto.ComponentType, dto.ComponentId);
        await _buildRepository.SaveChangesAsync();
        return build;
    }
    public async Task<BuildEntity> RemoveComponentAsync(int buildId, BuildComponentType componentType)
    {
        var build = await GetBuildById(buildId);
        ApplyComponent(build, componentType, null);
        await _buildRepository.SaveChangesAsync();
        return build;
    }

    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksAsync(BuildRequest dto)
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
    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksForUpdateAsync(int buildId, BuildRequest dto)
    {
        var build = await GetBuildById(buildId);

        var mergedDto = new BuildRequest
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

    public async Task<List<CompatibilityIssue>> RunCompatibilityChecksForComponentUpdateAsync(int buildId, BuildComponentRequest dto)
    {
        var build = await GetBuildById(buildId);
        var mergedDto = new BuildRequest
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
            BuildComponentType.Cpu => await _buildRepository.CpuExistsAsync(componentId ?? 0),
            BuildComponentType.CpuCooler => await _buildRepository.CpuCoolerExistsAsync(componentId ?? 0),
            BuildComponentType.Gpu => await _buildRepository.GpuExistsAsync(componentId ?? 0),
            BuildComponentType.Ram => await _buildRepository.RamExistsAsync(componentId ?? 0),
            BuildComponentType.HardDrive => await _buildRepository.HardDriveExistsAsync(componentId ?? 0),
            BuildComponentType.Motherboard => await _buildRepository.MotherboardExistsAsync(componentId ?? 0),
            BuildComponentType.Psu => await _buildRepository.PsuExistsAsync(componentId ?? 0),
            BuildComponentType.PcCase => await _buildRepository.CaseExistsAsync(componentId ?? 0),
            BuildComponentType.PcMonitor => await _buildRepository.MonitorExistsAsync(componentId ?? 0),
            _ => throw new ArgumentException($"Invalid component type: {componentType}")
        };
    }

    private async Task<BuildEntity> GetBuildById(int buildId)
    {
        return await _buildRepository.GetByIdAsync(buildId, _userContext.UserId) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found for user Id {_userContext.UserId}");
    }
}
