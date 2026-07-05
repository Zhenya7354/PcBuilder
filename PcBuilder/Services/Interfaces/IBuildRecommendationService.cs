using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBuildRecommendationService
{
    Task<BuildRecommendationResult> RecommendBuildAsync(AiBuildRequirements buildRequirements, CancellationToken cancellationToken);
}
