using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBuildService
{
    public Task<BuildEntity> GetBuildByIdAsync(string userId, int buildId);
    public Task<List<BuildEntity>> GetUserBuildsAsync(string userId);
    public Task<BuildEntity> AddBuildAsync(string userId, Build dto);
    public Task<BuildEntity> UpdateBuildAsync(int buildId, string userId, Build dto);
    public Task DeleteBuildAsync(int buildId, string userId);
    public Task<BuildEntity> SetComponentAsync(int buildId, string userId, BuildComponent dto);
    public Task<BuildEntity> RemoveComponentAsync(int buildId, string userId, BuildComponentType componentType);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksAsync(Build dto);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksForUpdateAsync(int buildId, string userId, Build dto);
    public Task<List<CompatibilityIssue>> RunCompatibilityChecksForComponentUpdateAsync(int buildId, string userId, BuildComponent dto);
}
