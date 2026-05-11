namespace PcBuilder.Models
{
    public class CompatibilityCheckResponse
    {
        public bool IsCompatible { get; set; }
        public List<CompatibilityIssue> Issues { get; set; } = [];

        public static CompatibilityCheckResponse Success() => new () { IsCompatible = true,};
        public static CompatibilityCheckResponse Failure(List<CompatibilityIssue> issues) => new() { IsCompatible = false, Issues = issues };

    }

    public class CompatibilityIssue
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public CompatibilityServerity Severity { get; set; }
    }

    public enum  CompatibilityServerity
    {
        Error,
        Warning,
    }
}
