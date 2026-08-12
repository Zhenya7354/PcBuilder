namespace PcBuilder.Models;

public class AiBuildRequest
{
    public required string Prompt { get; set; } = string.Empty;
    public string? Name { get; set; }
    public bool AcceptAiSuggestedName { get; set; } = false;
}

