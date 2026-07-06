using PcBuilder.Models;
using PcBuilder.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PcBuilder.Services;

public class AiBuildSevice(IGeminiAiProvider geminiAiProvider, IConfiguration configuration) : IAiBuildService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IGeminiAiProvider _geminiAiProvider = geminiAiProvider;
    private const string ResponseSchemaObjectType = "OBJECT";
    private const string ResponseSchemaStringType = "STRING";
    private const string ResponseSchemaBoolType = "BOOLEAN";
    private const string ResponseSchemaArrayType = "ARRAY";
    private const string ResponseSchemaDecimalType = "NUMBER";
    public async Task<AiBuildRequirements> AnalyzeAsync(string prompt, CancellationToken cancellationToken)
    {

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
                    type = ResponseSchemaObjectType,
                    properties = new
                    {
                        purpose = new
                        {
                            type = ResponseSchemaStringType,
                            description = "Main use case, for example gaming, office, streaming, programming, editing, or general use."
                        },
                        budget = new
                        {
                            type = ResponseSchemaDecimalType,
                            nullable = true,
                            description = "User budget as a number only, or null if no budget is provided."
                        },
                        currency = new
                        {
                            type = ResponseSchemaStringType,
                            nullable = true,
                            description = "Currency code or null if no currency is provided."
                        },
                        targetResolution = new
                        {
                            type = ResponseSchemaStringType,
                            nullable = true,
                            description = "Gaming or monitor resolution, or null if not provided."
                        },
                        priorities = new
                        {
                            type = ResponseSchemaArrayType,
                            items = new
                            {
                                type = ResponseSchemaStringType
                            }
                        },
                        needsMonitor = new
                        {
                            type = ResponseSchemaBoolType
                        },
                        preferredBrands = new
                        {
                            type = ResponseSchemaArrayType,
                            items = new
                            {
                                type = ResponseSchemaStringType
                            }
                        },
                        avoidBrands = new
                        {
                            type = ResponseSchemaArrayType,
                            items = new
                            {
                                type = ResponseSchemaStringType
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

        var response = await _geminiAiProvider.GenerateContentAsync(content, cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Api request failed with {(int)response.StatusCode} {response.ReasonPhrase}: {responseJson}");
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
