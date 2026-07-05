namespace PcBuilder.Models;

public class BuildRecommendationResult
{
    public BuildRequest Build { get; set; } = new();
    public List<string> Notes { get; set; } = [];
    public bool IsCompleted { get; set; }
}
