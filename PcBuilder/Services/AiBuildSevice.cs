using PcBuilder.Models;
using PcBuilder.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PcBuilder.Services;

public class AiBuildSevice(HttpClient httpClient, IConfiguration configuration) : IAiBuildService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    public async Task<AiBuildRequirements> AnalyzeAsync(string prompt, CancellationToken cancellationToken)
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        var model = _configuration["Gemini:Model"] ?? "gemini-3.5-flash";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("Gemini API key is missing. Set Gemini:ApiKey in user secrets.");
        }

        _httpClient.DefaultRequestHeaders.Remove("x-goog-api-key");
        _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new
                        {
                            text = $$"""
                                Extract PC build requirements from the user prompt.
                                Return only JSON with these exact fields:
                                {
                                  "purpose": "gaming | office | streaming | programming | editing | general use | unknown",
                                  "budget": number or null,
                                  "currency": "currency code or null",
                                  "targetResolution": "1080p | 1440p | 4K | null",
                                  "priorities": ["important preferences like quiet, wifi, rgb, low price"],
                                  "needsMonitor": true or false,
                                  "preferredBrands": ["brand names"],
                                  "avoidBrands": ["brand names"]
                                }

                                User prompt:
                                {{prompt}}
                                """
                        }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0,
                maxOutputTokens = 1024,
                responseMimeType = "application/json",
                responseSchema = new
                {
                    type = "OBJECT",
                    properties = new
                    {
                        purpose = new
                        {
                            type = "STRING",
                            description = "Main use case, for example gaming, office, streaming, programming, editing, or general use."
                        },
                        budget = new
                        {
                            type = "NUMBER",
                            nullable = true,
                            description = "User budget as a number only, or null if no budget is provided."
                        },
                        currency = new
                        {
                            type = "STRING",
                            nullable = true,
                            description = "Currency code or null if no currency is provided."
                        },
                        targetResolution = new
                        {
                            type = "STRING",
                            nullable = true,
                            description = "Gaming or monitor resolution, or null if not provided."
                        },
                        priorities = new
                        {
                            type = "ARRAY",
                            items = new
                            {
                                type = "STRING"
                            }
                        },
                        needsMonitor = new
                        {
                            type = "BOOLEAN"
                        },
                        preferredBrands = new
                        {
                            type = "ARRAY",
                            items = new
                            {
                                type = "STRING"
                            }
                        },
                        avoidBrands = new
                        {
                            type = "ARRAY",
                            items = new
                            {
                                type = "STRING"
                            }
                        }
                    },
                    required = new[]
                    {
                        "purpose",
                        "budget",
                        "currency",
                        "targetResolution",
                        "priorities",
                        "needsMonitor",
                        "preferredBrands",
                        "avoidBrands"
                    }
                }
            }
        };
        var json = JsonSerializer.Serialize(requestBody);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent",
            content,
            cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Gemini request failed with {(int)response.StatusCode} {response.ReasonPhrase}: {responseJson}");
        }

        using var document = JsonDocument.Parse(responseJson);

        var outputText = document.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

            return JsonSerializer.Deserialize<AiBuildRequirements>(
                outputText!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
    }
}
