namespace PcBuilder.Models;

public class AiBuildRequirements
{
    public string Purpose { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public string? Currency { get; set; }
    public string? TargetResolution { get; set; }
    public List<string> Priorities { get; set; } = [];
    public bool NeedsMonitor { get; set; }
    public List<string> PreferredBrands { get; set; } = [];
    public List<string> AvoidBrands { get; set; } = [];
}
