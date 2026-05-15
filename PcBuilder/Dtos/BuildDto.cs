using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record BuildDto(
    string? Name,
    int? CpuId,
    int? CpuCoolerId,
    int? GpuId,
    int? RamId,
    int? HardDriveId,
    int? MotherboardId,
    int? PsuId,
    int? CaseId,
    int? MonitorId
        );

}
