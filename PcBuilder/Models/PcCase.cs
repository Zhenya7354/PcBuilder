using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class PcCase : Component
    {
        [Column("form_factors_supported")]
        public List<string> supportedFormFactors { get; set; } = [];
        [Column("max_gpu_length_mm")]
        public int maxGpuLengthMm { get; set; }
        [Column("max_cpu_cooler_height_mm")]
        public int maxCpuCoolerHeightMm { get; set; }
        [Column("max_psu_length_mm")]
        public int maxPsuLengthMm { get; set; }
        [Column("drive_bays_35")]
        public int driveBays35Inch { get; set; }
        [Column("drive_bays_25")]
        public int driveBays25Inch { get; set; }
        [Column("front_usb_a")]
        public int frontUsbA { get; set; }
        [Column("front_usb_c")]
        public int frontUsbC { get; set; }
        [Column("radiator_support_mm")]
        public List<string> radiatorSupportMm { get; set; } = [];
        [Column("case_width_mm")]
        public int? caseWidthMm { get; set; }
        [Column("case_height_mm")]
        public int? caseHeightMm { get; set; }
        [Column("case_depth_mm")]
        public int? caseDepthMm { get; set; }
        [Column("has_glass_panel")]
        public bool hasGlassPanel { get; set; }
        [Column("included_fans")]
        public int includedFans { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }
        
    }
}
