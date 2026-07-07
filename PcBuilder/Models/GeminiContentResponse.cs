namespace PcBuilder.Models;

public class GeminiContentResponse
{
    public List<GeminiCandidate> Candidates { get; set; } = [];
}

public class GeminiCandidate
{
    public GeminiContent Content { get; set; } = new();
}
public class GeminiContent
{
    public List<GeminiPart> Parts { get; set; } = [];
}
public class GeminiPart
{
    public string Text { get; set; } = string.Empty;
}

