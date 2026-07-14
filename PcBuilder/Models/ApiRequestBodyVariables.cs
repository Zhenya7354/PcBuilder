namespace PcBuilder.Models;

public static class ApiRequestBodyVariables
{
    public const string Object = "OBJECT";
    public const string String = "STRING";
    public const string Bool = "BOOLEAN";
    public const string Array = "ARRAY";
    public const string Decimal = "NUMBER";
    public const string ContentsRole = "user";

    public const string StructuredOutputInstructions = "Extract PC build requirements from the user prompt.\r\n" +
        "Return only JSON with these exact fields:\r\n{\r\n  \"purpose\": \"gaming | office | streaming | programming | editing | general use | unknown\",\r\n" +
        "  \"budget\": number or null,\r\n  \"currency\": \"currency code or null\",\r\n  \"targetResolution\": \"1080p | 1440p | 4K | null\",\r\n" +
        "  \"priorities\": [\"important preferences like quiet, wifi, rgb, low price\"],\r\n  \"needsMonitor\": true or false,\r\n" +
        "  \"preferredBrands\": [\"brand names\"],\r\n  \"avoidBrands\": [\"brand names\"]\r\n}";

    public const string GenerationConfigResponseMimeType = "application/json";
    public const int GenerationConfigMaxOutputTokens = 1024;
    public const int GenerationConfigTemperature = 0;
    public static readonly string[] RequiredFields = new string[] { "purpose", "budget", "currency", "targetResolution", "priorities", "needsMonitor", "preferredBrands", "avoidBrands" };

    public const string PurposeDescription = "Main use case, for example gaming, office, streaming, programming, editing, or general use.";
    public const string BudgetDescription = "User budget as a number only, or null if no budget is provided.";
    public const string CurrencyDescription = "Currency code or null if no currency is provided.";
    public const string TargetResolutionDescription = "Gaming or monitor resolution, or null if not provided.";
}
