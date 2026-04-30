using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Psu : Component
    {
        [Column("wattage")]
        public int wattage { get; set; }
        [Column("efficiency")]
        public PsuRating efficiency { get; set; }
        [Column("modularity")]
        public PsuModular modularity { get; set; }
        [Column("atx_version")]
        public string atxVersion { get; set; } = string.Empty;
        [Column("has_16pin")]
        public bool has16Pin { get; set; }
        [Column("eps_connectors")]
        public int epsConnectors { get; set; }
        [Column("sata_connectors")]
        public int sataConnectors { get; set; }
        [Column("pcie_8pin")]
        public int pcie8PinConnectors { get; set; }
        [Column("fan_size_mm")]
        public int fanSizeMm { get; set; }
        [Column("length_mm")]
        public int? lengthMm { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }


    }
}
