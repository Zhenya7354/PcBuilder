using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

    public class BuildRequest
    {
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
    };

