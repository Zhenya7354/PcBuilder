using PcBuilder.Models;
using PcBuilder.Services.Interfaces;
using System.Text.Json;

namespace PcBuilder.Services;

public class GeminiAiProvider(IConfiguration configuration, HttpClient httpClient) : IGeminiAiProvider
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/";
    const string GeminiApiEndpoint = ":generateContent";
    public async Task<GeminiContentResponse> GenerateContentAsync(HttpContent? content, CancellationToken cancellationToken)
    {
        string model = _configuration["Gemini:Model"] ?? "gemini-3.5-flash";
        using var response = await _httpClient.PostAsync(GeminiApiUrl + model + GeminiApiEndpoint, content, cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Api request failed with {(int)response.StatusCode} {response.ReasonPhrase}: {responseJson}");
        }

        return JsonSerializer.Deserialize<GeminiContentResponse>(
            responseJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidOperationException("Gemini API returned an empty response.");
    }
}
