namespace PcBuilder.Entities;

public class BuildEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? CpuId { get; set; }
    public int? CpuCoolerId { get; set; }
    public int? GpuId { get; set; }
    public int? RamId { get; set; }
    public int? HardDriveId { get; set; }
    public int? MotherboardId { get; set; }
    public int? PsuId { get; set; }
    public int? CaseId { get; set; }
    public int? MonitorId { get; set; }
    public string? UserId { get; set; } = string.Empty;

    // Navigation properties
    public UserEntity? User { get; set; }
    public CpuEntity? Cpu { get; set; }
    public CpuCoolerEntity? CpuCooler { get; set; }
    public GpuEntity? Gpu { get; set; }
    public RamEntity? Ram { get; set; }
    public HardDriveEntity? HardDrive { get; set; }
    public MotherboardEntity? Motherboard { get; set; }
    public PsuEntity? Psu { get; set; }
    public PcCaseEntity? PcCase { get; set; }
    public PcMonitorEntity? Monitor { get; set; }
}