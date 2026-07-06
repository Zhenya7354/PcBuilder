namespace PcBuilder.Services.Interfaces;

public interface IGeminiAiProvider
{
    Task<HttpResponseMessage?> GenerateContentAsync(HttpContent? content, CancellationToken cancellationToken);
}
