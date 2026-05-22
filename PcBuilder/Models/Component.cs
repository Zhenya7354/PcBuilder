using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PcBuilder.Models;

public abstract class Component
{
    [JsonPropertyOrder(-3)]
    [Column("id")]
    public int Id { get; set; }
    [JsonPropertyOrder(-2)]
    [Column("brand_id")]
    public int BrandId { get; set; }
    [JsonPropertyOrder(-1)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    //Navigation property for the brand relationship
    public Brand? Brand { get; set; }
}
