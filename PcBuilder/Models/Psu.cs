using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models;

public class Psu : Component
{
    [Column("wattage")]
    public int Wattage { get; set; }
    [Column("efficiency")]
    public PsuRating Efficiency { get; set; }
    [Column("modular")]
    public PsuModular Modularity { get; set; }
    [Column("atx_version")]
    public string AtxVersion { get; set; } = string.Empty;
    [Column("has_16pin")]
    public bool Has16Pin { get; set; }
    [Column("eps_connectors")]
    public int EpsConnectors { get; set; }
    [Column("sata_connectors")]
    public int SataConnectors { get; set; }
    [Column("pcie_8pin")]
    public int Pcie8PinConnectors { get; set; }
    [Column("fan_size_mm")]
    public int FanSizeMm { get; set; }
    [Column("length_mm")]
    public int? LengthMm { get; set; }
    [Column("price_usd")]
    public decimal? PriceUsd { get; set; }


}
