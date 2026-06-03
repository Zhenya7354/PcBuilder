using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBuildService
{
    public Task<BuildEntity> GetBuildByIdAsync(int buildId);
    public Task<List<BuildEntity>> GetUserBuildsAsync();
    public Task<BuildEntity> AddBuildAsync(BuildRequest dto);
    public Task<BuildEntity> UpdateBuildAsync(int buildId,BuildRequest dto);
    public Task DeleteBuildAsync(int buildId);
    public Task<BuildEntity> SetComponentAsync(int buildId,BuildComponentRequest dto);
    public Task<BuildEntity> RemoveComponentAsync(int buildId,BuildComponentType componentType);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksAsync(BuildRequest dto);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksForUpdateAsync(int buildId,BuildRequest dto);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksForComponentUpdateAsync(int buildId,BuildComponentRequest dto);
}
