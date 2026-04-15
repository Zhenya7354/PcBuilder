namespace PcBuilder.Models
{
    public class Build
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CpuId { get; set; }
        public int? Cpu_CoolerId { get; set; }
        public int? GpuId { get; set; }
        public int? RamId { get; set; }
        public int? Hard_driveId { get; set; }
        public int? MotherboardId { get; set; }
        public int? PsuId { get; set; }
        public int? CaseId { get; set; }
        public int? MonitorId { get; set; }

    }
}
