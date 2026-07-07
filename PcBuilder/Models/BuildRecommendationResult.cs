using PcBuilder.Enums;

namespace PcBuilder.Models;

public class BuildRecommendationResult
{
    public BuildRequest Build { get; set; } = new();
    public List<string> Notes { get; set; } = [];
    public BuildRecommendationStatus Status { get; set; }
}
