namespace PcBuilder.Models;

public class Build
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
    public User? User { get; set; }
    public Cpu? Cpu { get; set; }
    public CpuCooler? CpuCooler { get; set; }
    public Gpu? Gpu { get; set; }
    public Ram? Ram { get; set; }
    public HardDrive? HardDrive { get; set; }
    public Motherboard? Motherboard { get; set; }
    public Psu? Psu { get; set; }
    public PcCase? PcCase { get; set; }
    public PcMonitor? Monitor { get; set; }
}