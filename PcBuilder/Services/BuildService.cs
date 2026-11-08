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

    public BuildService(IBuildRepository buildRepository, ICompatibilityCheckService compatibilityCheckService, IUserContextAccessor userContextAccessor)
    {
        _buildRepository = buildRepository;
        _compatibilityCheckService = compatibilityCheckService;
        _userContextAccessor = userContextAccessor;
    }

    public async Task<BuildEntity> GetBuildByIdAsync(int buildId, CancellationToken cancellationToken)
    {
        return await _buildRepository.GetByIdAsync(buildId, _userContextAccessor.GetUserContext().UserId, cancellationToken) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found");
    }
    public async Task<List<BuildEntity>> GetUserBuildsAsync(CancellationToken cancellationToken)
    {
        return await _buildRepository.GetAllAsync(_userContextAccessor.GetUserContext().UserId, cancellationToken);
    }
    public async Task<BuildEntity> AddBuildAsync(BuildRequest dto, CancellationToken cancellationToken)
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
            UserId = _userContextAccessor.GetUserContext().UserId
        };
        await _buildRepository.AddBuildAsync(build, cancellationToken);
        await _buildRepository.SaveChangesAsync(cancellationToken);
        return build;
    }

    public async Task<BuildEntity> UpdateBuildAsync(int buildId, BuildRequest dto, CancellationToken cancellationToken)
    {
        var build = await GetBuildForUpdateAsync(buildId, cancellationToken);

        if (!string.IsNullOrEmpty(dto.Name)) { build.Name = dto.Name; }

        // Full replacement: null in the request clears that slot.
        build.CpuId = dto.CpuId;
        build.CpuCoolerId = dto.CpuCoolerId;
        build.GpuId = dto.GpuId;
        build.RamId = dto.RamId;
        build.HardDriveId = dto.HardDriveId;
        build.MotherboardId = dto.MotherboardId;
        build.PsuId = dto.PsuId;
        build.CaseId = dto.CaseId;
        build.MonitorId = dto.MonitorId;

        await _buildRepository.SaveChangesAsync(cancellationToken);
        return await GetBuildByIdAsync(buildId, cancellationToken);
    }

    public async Task DeleteBuildAsync(int buildId, CancellationToken cancellationToken)
    {
        var build = await GetBuildByIdAsync(buildId, cancellationToken);
        await _buildRepository.DeleteBuildAsync(build, cancellationToken);
        await _buildRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<BuildEntity> SetComponentAsync(int buildId, BuildComponentRequest dto, CancellationToken cancellationToken)
    {
        var build = await GetBuildForUpdateAsync(buildId, cancellationToken);
        if (!await ComponentExists(dto.ComponentType, dto.ComponentId, cancellationToken))
        {
            throw new KeyNotFoundException($"Component with Id {dto.ComponentId} not found for type {dto.ComponentType}");
        }
        ApplyComponent(build, dto.ComponentType, dto.ComponentId);
        await _buildRepository.SaveChangesAsync(cancellationToken);
        return await GetBuildByIdAsync(buildId, cancellationToken);
    }
    public async Task<BuildEntity> RemoveComponentAsync(int buildId, BuildComponentType componentType, CancellationToken cancellationToken)
    {
        var build = await GetBuildForUpdateAsync(buildId, cancellationToken);
        ApplyComponent(build, componentType, null);
        await _buildRepository.SaveChangesAsync(cancellationToken);
        return await GetBuildByIdAsync(buildId, cancellationToken);
    }

    private async Task<BuildEntity> GetBuildForUpdateAsync(int buildId, CancellationToken cancellationToken)
    {
        var userId = _userContextAccessor.GetUserContext().UserId;
        return await _buildRepository.GetByIdForUpdateAsync(buildId, userId, cancellationToken) ??
            throw new KeyNotFoundException($"Build with Id {buildId} not found");
    }

    public async Task<CompatibilityCheckResponse> RunCompatibilityChecksAsync(BuildRequest dto, CancellationToken cancellationToken)
    {
        var checks = new List<(int? IdA, int? IdB, Func<int, int, CancellationToken, Task<CompatibilityCheckResponse>> Check)> {
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

        var compatibilityCheckResults = new List<CompatibilityCheckResponse>();
        foreach(var check in checks.Where(c => c.IdA.HasValue && c.IdB.HasValue))
        {
            compatibilityCheckResults.Add(
                await check.Check(check.IdA!.Value, check.IdB!.Value, cancellationToken));
        }

        if (compatibilityCheckResults.Any(c => c.IsSuccess == false))
        {
            return CompatibilityCheckResponse.Failure(compatibilityCheckResults.SelectMany(c => c.Issues).ToList());
        }
        return CompatibilityCheckResponse.Success();
    }
    public async Task<CompatibilityCheckResponse> RunCompatibilityChecksForBuildUpdateAsync(int buildId, BuildRequest dto, CancellationToken cancellationToken)
    {
        // Ensure the build exists and belongs to the current user; slots use full-replacement from dto.
        await GetBuildByIdAsync(buildId, cancellationToken);
        return await RunCompatibilityChecksAsync(dto, cancellationToken);
    }

    public async Task<CompatibilityCheckResponse> RunCompatibilityChecksForComponentUpdateAsync(int buildId, BuildComponentRequest dto, CancellationToken cancellationToken)
    {
        var build = await GetBuildByIdAsync(buildId, cancellationToken);
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
        }
        ;
        return await RunCompatibilityChecksAsync(mergedDto, cancellationToken);
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
    private async Task<bool> ComponentExists(BuildComponentType componentType, int? componentId, CancellationToken cancellationToken)
    {
        return componentType switch
        {
            BuildComponentType.Cpu => await _buildRepository.CpuExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.CpuCooler => await _buildRepository.CpuCoolerExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.Gpu => await _buildRepository.GpuExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.Ram => await _buildRepository.RamExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.HardDrive => await _buildRepository.HardDriveExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.Motherboard => await _buildRepository.MotherboardExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.Psu => await _buildRepository.PsuExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.PcCase => await _buildRepository.CaseExistsAsync(componentId ?? 0, cancellationToken),
            BuildComponentType.PcMonitor => await _buildRepository.MonitorExistsAsync(componentId ?? 0, cancellationToken),
            _ => throw new ArgumentException($"Invalid component type: {componentType}")
        };
    }


}
