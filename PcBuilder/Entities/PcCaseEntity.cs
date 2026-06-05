using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Entities;

public class PcCaseEntity : Component
{
    public List<FormFactor> SupportedFormFactors { get; set; } = [];
    public int MaxGpuLengthMm { get; set; }
    public int MaxCpuCoolerHeightMm { get; set; }
    public int MaxPsuLengthMm { get; set; }
    public int DriveBays35Inch { get; set; }
    public int DriveBays25Inch { get; set; }
    public int FrontUsbA { get; set; }
    public int FrontUsbC { get; set; }
    public List<string> RadiatorSupportMm { get; set; } = [];
    public int? CaseWidthMm { get; set; }
    public int? CaseHeightMm { get; set; }
    public int? CaseDepthMm { get; set; }
    public bool HasGlassPanel { get; set; }
    public int IncludedFans { get; set; }
    public ColorScheme ColorScheme { get; set; }
    public Currency? Currency { get; set; }
    public decimal? Price { get; set; }

}