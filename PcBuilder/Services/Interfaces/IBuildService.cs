using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBuildService
{
    public Task<BuildEntity> GetBuildByIdAsync(int buildId, CancellationToken cancellationToken);
    public Task<List<BuildEntity>> GetUserBuildsAsync(CancellationToken cancellationToken);
    public Task<BuildEntity> AddBuildAsync(BuildRequest dto, CancellationToken cancellationToken);
    public Task<BuildEntity> UpdateBuildAsync(int buildId, BuildRequest dto, CancellationToken cancellationToken);
    public Task DeleteBuildAsync(int buildId, CancellationToken cancellationToken);
    public Task<BuildEntity> SetComponentAsync(int buildId, BuildComponentRequest dto, CancellationToken cancellationToken);
    public Task<BuildEntity> RemoveComponentAsync(int buildId,BuildComponentType componentType, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> RunCompatibilityChecksAsync(BuildRequest dto, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> RunCompatibilityChecksForBuildUpdateAsync(int buildId,BuildRequest dto, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> RunCompatibilityChecksForComponentUpdateAsync(int buildId,BuildComponentRequest dto, CancellationToken cancellationToken);
}
