using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IGeminiAiProvider
{
    Task<GeminiContentResponse> GenerateContentAsync(HttpContent? content, CancellationToken cancellationToken);
}
