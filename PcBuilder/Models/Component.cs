using System.Text.Json.Serialization;

namespace PcBuilder.Models;

public abstract class Component
{
    [JsonPropertyOrder(-3)]
    public int Id { get; set; }
    [JsonPropertyOrder(-2)]
    public int BrandId { get; set; }
    [JsonPropertyOrder(-1)]
    public string Name { get; set; } = string.Empty;
    //Navigation property for the brand relationship
    public Brand? Brand { get; set; }
}