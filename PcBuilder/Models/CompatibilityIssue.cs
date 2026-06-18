using PcBuilder.Enums;

namespace PcBuilder.Models;

public class CompatibilityIssue
{
    public string Field { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public CompatibilitySeverity Severity { get; set; }
}
