using PcBuilder.Enums;

namespace PcBuilder.Models;

public class CompatibilityCheckResponse
{
    public bool IsSuccess { get; set; }
    public List<CompatibilityIssue> Issues { get; set; } = [];

    public static CompatibilityCheckResponse Success() => new () { IsSuccess = true,};
    public static CompatibilityCheckResponse Failure(List<CompatibilityIssue> issues) => new() { IsSuccess = false, Issues = issues };
}



