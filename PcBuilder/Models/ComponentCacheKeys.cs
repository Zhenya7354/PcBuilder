using PcBuilder.Enums;

namespace PcBuilder.Models;

public static class ComponentCacheKeys
{
    public const string CpusKey = "catalog:cpus";
    public const string GpusKey = "catalog:gpus";
    public const string RamsKey = "catalog:ram";
    public const string MotherboardsKey = "catalog:motherboards";
    public const string PsusKeys = "catalog:power_supplies";
    public const string PcCasesKey = "catalog:pc_cases";
    public const string MonitorsKey = "catalog:monitors";
    public const string HardDrivesKey = "catalog:hard_drives";
    public const string CpuCoolersKey = "catalog:cpu_coolers";

    public static readonly IReadOnlyList<string> All =
    [
        CpusKey,GpusKey,RamsKey,MotherboardsKey,PsusKeys,PcCasesKey,MonitorsKey,HardDrivesKey,CpuCoolersKey
    ];
}


