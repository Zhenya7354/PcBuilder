using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class GeminiAiProvider(IConfiguration configuration, HttpClient httpClient) : IGeminiAiProvider
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/";
    const string GeminiApiEndpoint = ":generateContent";
    public async Task<HttpResponseMessage?> GenerateContentAsync(HttpContent? content, CancellationToken cancellationToken)
    {
        string model = _configuration["Gemini:Model"] ?? "gemini-3.5-flash";
        return await _httpClient.PostAsync(GeminiApiUrl + model + GeminiApiEndpoint, content, cancellationToken);
    }
}
