namespace PcBuilder.Dtos
{
    public record BuildUpdateDto(
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
