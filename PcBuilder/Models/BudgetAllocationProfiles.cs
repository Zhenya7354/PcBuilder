using PcBuilder.Enums;

namespace PcBuilder.Services;

public static class BudgetAllocationProfiles
{
    private static readonly Dictionary<string, Dictionary<BuildComponentType, decimal>> Profiles =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["gaming"] = new()
            {
                [BuildComponentType.Cpu] = 0.20m,
                [BuildComponentType.Gpu] = 0.35m,
                [BuildComponentType.Motherboard] = 0.10m,
                [BuildComponentType.Ram] = 0.08m,
                [BuildComponentType.Psu] = 0.07m,
                [BuildComponentType.PcCase] = 0.07m,
                [BuildComponentType.CpuCooler] = 0.05m,
                [BuildComponentType.HardDrive] = 0.08m,
            },
            ["office"] = new()
            {
                [BuildComponentType.Cpu] = 0.30m,
                [BuildComponentType.Gpu] = 0.05m,
                [BuildComponentType.Motherboard] = 0.15m,
                [BuildComponentType.Ram] = 0.15m,
                [BuildComponentType.Psu] = 0.10m,
                [BuildComponentType.PcCase] = 0.10m,
                [BuildComponentType.CpuCooler] = 0.05m,
                [BuildComponentType.HardDrive] = 0.10m,
            },
            ["default"] = new()
            {
                [BuildComponentType.Cpu] = 0.25m,
                [BuildComponentType.Gpu] = 0.20m,
                [BuildComponentType.Motherboard] = 0.12m,
                [BuildComponentType.Ram] = 0.10m,
                [BuildComponentType.Psu] = 0.08m,
                [BuildComponentType.PcCase] = 0.08m,
                [BuildComponentType.CpuCooler] = 0.07m,
                [BuildComponentType.HardDrive] = 0.10m,
            }
        };
    public static decimal GetShare(string? purpose, BuildComponentType componentType)
    {
        var profile = purpose is not null && Profiles.TryGetValue(purpose, out var p) ? p : Profiles["default"];
        return profile.GetValueOrDefault(componentType);
    }
}
