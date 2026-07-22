using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IAiBuildService
{
    Task<BuildRecommendationResult> RecommendBuildAsync(string prompt, CancellationToken cancellationToken);
}
