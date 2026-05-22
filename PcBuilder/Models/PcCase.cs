using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models;

public class PcCase : Component
{
    [Column("supported_form_factors")]
    public List<FormFactor> SupportedFormFactors { get; set; } = [];
    [Column("max_gpu_length_mm")]
    public int MaxGpuLengthMm { get; set; }
    [Column("max_cpu_cooler_height_mm")]
    public int MaxCpuCoolerHeightMm { get; set; }
    [Column("max_psu_length_mm")]
    public int MaxPsuLengthMm { get; set; }
    [Column("drive_bays_35")]
    public int DriveBays35Inch { get; set; }
    [Column("drive_bays_25")]
    public int DriveBays25Inch { get; set; }
    [Column("front_usb_a")]
    public int FrontUsbA { get; set; }
    [Column("front_usb_c")]
    public int FrontUsbC { get; set; }
    [Column("radiator_support_mm")]
    public List<string> RadiatorSupportMm { get; set; } = [];
    [Column("case_width_mm")]
    public int? CaseWidthMm { get; set; }
    [Column("case_height_mm")]
    public int? CaseHeightMm { get; set; }
    [Column("case_depth_mm")]
    public int? CaseDepthMm { get; set; }
    [Column("has_glass_panel")]
    public bool HasGlassPanel { get; set; }
    [Column("includes_fans")]
    public int IncludedFans { get; set; }
    [Column("price_usd")]
    public decimal? PriceUsd { get; set; }
    
}
