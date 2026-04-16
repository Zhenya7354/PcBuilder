namespace PcBuilder.Models
{
    public class Build
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? cpuId { get; set; }
        public int? cpuCoolerId { get; set; }
        public int? gpuId { get; set; }
        public int? ramId { get; set; }
        public int? hardDriveId { get; set; }
        public int? motherboardId { get; set; }
        public int? psuId { get; set; }
        public int? caseId { get; set; }
        public int? monitorId { get; set; }
        public string? userId { get; set; } = string.Empty;

        // Navigation properties
        public User? user { get; set; }
        public Cpu? cpu { get; set; }
        public CpuCooler? cpuCooler { get; set; }
        public Gpu? gpu { get; set; }
        public Ram? ram { get; set; }
        public HardDrive? hardDrive { get; set; }
        public Motherboard? motherboard { get; set; }
        public Psu? psu { get; set; }
        public Case? pcCase { get; set; }
        public PcMonitor? monitor { get; set; }
    }
}
