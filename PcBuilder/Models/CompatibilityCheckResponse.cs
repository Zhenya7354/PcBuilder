using PcBuilder.Enums;

namespace PcBuilder.Models;

public class CompatibilityCheckResponse
{
    public bool IsCompatible { get; set; }
    public List<CompatibilityIssue> Issues { get; set; } = [];

    public static CompatibilityCheckResponse Success() => new () { IsCompatible = true,};
    public static CompatibilityCheckResponse Failure(List<CompatibilityIssue> issues) => new() { IsCompatible = false, Issues = issues };
}



