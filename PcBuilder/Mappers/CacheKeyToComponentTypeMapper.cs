using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Mappers;

public static class CacheKeyToBuildTypeMapper
{
    public static string GetCacheKeyForBuildComponentType(BuildComponentType componentType)
    {
        return componentType switch
        {
            BuildComponentType.Cpu => ComponentCacheKeys.CpusKey,
            BuildComponentType.Gpu => ComponentCacheKeys.GpusKey,
            BuildComponentType.Ram => ComponentCacheKeys.RamsKey,
            BuildComponentType.Motherboard => ComponentCacheKeys.MotherboardsKey,
            BuildComponentType.Psu => ComponentCacheKeys.PsusKeys,
            BuildComponentType.PcCase => ComponentCacheKeys.PcCasesKey,
            BuildComponentType.PcMonitor => ComponentCacheKeys.MonitorsKey,
            BuildComponentType.HardDrive => ComponentCacheKeys.HardDrivesKey,
            BuildComponentType.CpuCooler => ComponentCacheKeys.CpuCoolersKey,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
}
