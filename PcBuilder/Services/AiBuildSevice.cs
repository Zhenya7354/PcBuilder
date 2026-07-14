using PcBuilder.Enums;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PcBuilder.Services;

public class AiBuildSevice(
    IGeminiAiProvider geminiAiProvider,
    IConfiguration configuration,
    ICpuRepository cpuRepository,
    IGpuRepository gpuRepository,
    IMotherboardRepository motherboardRepository,
    IRamRepository ramRepository,
    IHardDriveRepository hardDriveRepository,
    IPsuRepository psuRepository,
    ICpuCoolerRepository cpuCoolerRepository,
    IPcCaseRepository pcCaseRepository,
    IPcMonitorRepository pcMonitorRepository,
    ICompatibilityCheckService compatibilityCheckService) : IAiBuildService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IGeminiAiProvider _geminiAiProvider = geminiAiProvider;
    private const decimal DefaultBudget = 1500m;

    public async Task<AiBuildRequirements> AnalyzeAsync(string prompt, CancellationToken cancellationToken)
    {

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = ApiRequestBodyVariables.ContentsRole,
                    parts = new[]
                    {
                        new
                        {
                            text = $$"""
                                {{ApiRequestBodyVariables.StructuredOutputInstructions}}

                                User prompt:
                                {{prompt}}
                                """
                        }
                    }
                }
            },
            generationConfig = new
            {
                temperature = ApiRequestBodyVariables.GenerationConfigTemperature,
                maxOutputTokens = ApiRequestBodyVariables.GenerationConfigMaxOutputTokens,
                responseMimeType = ApiRequestBodyVariables.GenerationConfigResponseMimeType,
                responseSchema = new
                {
                    type = ApiRequestBodyVariables.Object,
                    properties = new
                    {
                        purpose = new
                        {
                            type = ApiRequestBodyVariables.String,
                            description = ApiRequestBodyVariables.PurposeDescription
                        },
                        budget = new
                        {
                            type = ApiRequestBodyVariables.Decimal,
                            nullable = true,
                            description = ApiRequestBodyVariables.BudgetDescription
                        },
                        currency = new
                        {
                            type = ApiRequestBodyVariables.String,
                            nullable = true,
                            description = ApiRequestBodyVariables.CurrencyDescription
                        },
                        targetResolution = new
                        {
                            type = ApiRequestBodyVariables.String,
                            nullable = true,
                            description = ApiRequestBodyVariables.TargetResolutionDescription
                        },
                        priorities = new
                        {
                            type = ApiRequestBodyVariables.Array,
                            items = new
                            {
                                type = ApiRequestBodyVariables.String
                            }
                        },
                        needsMonitor = new
                        {
                            type = ApiRequestBodyVariables.Bool
                        },
                        preferredBrands = new
                        {
                            type = ApiRequestBodyVariables.Array,
                            items = new
                            {
                                type = ApiRequestBodyVariables.String
                            }
                        },
                        avoidBrands = new
                        {
                            type = ApiRequestBodyVariables.Array,
                            items = new
                            {
                                type = ApiRequestBodyVariables.String
                            }
                        }
                    },
                    required = ApiRequestBodyVariables.RequiredFields
                }
            }
        };
        var json = JsonSerializer.Serialize(requestBody);
        using var content = new StringContent(json, Encoding.UTF8, ApiRequestBodyVariables.GenerationConfigResponseMimeType);

        var response = await _geminiAiProvider.GenerateContentAsync(content, cancellationToken);

        var outputText = response.Candidates
            .FirstOrDefault()?
            .Content
            .Parts
            .FirstOrDefault()?
            .Text;

        if (string.IsNullOrWhiteSpace(outputText))
        {
            throw new InvalidOperationException("Gemini API returned no output text.");
        }

        return JsonSerializer.Deserialize<AiBuildRequirements>(
                outputText!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

    }

    public async Task<BuildRecommendationResult> RecommendBuildAsync(AiBuildRequirements requirements, CancellationToken cancellationToken)
    {
        var result = new BuildRecommendationResult();
        var build = result.Build;
        var totalBudget = requirements.Budget ?? DefaultBudget;

        var cpus = await cpuRepository.GetAllCpusAsync(cancellationToken);
        var cpu = PickBest(cpus, c => c.Price, requirements, BuildComponentType.Cpu, totalBudget);
        if (cpu is null)
        {
            result.Notes.Add("No Cpu found matching budget/brand constraints.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.CpuId = cpu.Id;

        var motherboards = await motherboardRepository.GetAllMotherboardsAsync(cancellationToken);
        var compatibleMotherboards = await FilterCompatibleAsync(motherboards,
            m => compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(cpu.Id, m.Id, cancellationToken));
        var motherboard = PickBest(compatibleMotherboards, m => m.Price, requirements, BuildComponentType.Motherboard, totalBudget);
        if (motherboard is null)
        {
            result.Notes.Add($"No motherboard compatible with {cpu.Name} within budget.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.MotherboardId = motherboard.Id;

        var rams = await ramRepository.GetAllRamAsync(cancellationToken);
        var compatibleRam = await FilterCompatibleAsync(rams,
            r => compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(r.Id, motherboard.Id, cancellationToken));
        var ram = PickBest(compatibleRam, r => r.Price, requirements, BuildComponentType.Ram, totalBudget);
        if (ram is null)
        {
            result.Notes.Add($"No RAM compatible with {motherboard.Name} within budget.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.RamId = ram.Id;

        var gpus = await gpuRepository.GetAllGpusAsync(cancellationToken);
        var gpuCandidates = FilterByTarget(gpus, requirements.TargetResolution);
        var gpu = PickBest(gpuCandidates, g => g.Price, requirements, BuildComponentType.Gpu, totalBudget);
        if (gpu is null)
        {
            result.Notes.Add("No GPU found matching budget/resolution target.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.GpuId = gpu.Id;

        var psus = await psuRepository.GetAllPsusAsync(cancellationToken);
        var viablePsus = psus.Where(p => p.Wattage >= gpu.RecommendedPsuWattage).ToList();
        var psu = PickBest(viablePsus, p => p.Price, requirements, BuildComponentType.Psu, totalBudget);
        if (psu is null)
        {
            result.Notes.Add($"No PSU rated for {gpu.Name}'s {gpu.RecommendedPsuWattage}W requirement within budget.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.PsuId = psu.Id;

        var cases = await pcCaseRepository.GetAllCasesAsync(cancellationToken);
        var compatibleCases = await FilterCompatibleAsync(cases,
            c => compatibilityCheckService.CheckCaseToMotherboardCompatibilityAsync(c.Id, motherboard.Id, cancellationToken));
        compatibleCases = await FilterCompatibleAsync(compatibleCases,
            c => compatibilityCheckService.CheckCaseToGpuCompatibilityAsync(c.Id, gpu.Id, cancellationToken));
        compatibleCases = await FilterCompatibleAsync(compatibleCases,
            c => compatibilityCheckService.CheckCaseToPsuCompatibilityAsync(c.Id, psu.Id, cancellationToken));
        var pcCase = PickBest(compatibleCases, c => c.Price, requirements, BuildComponentType.PcCase, totalBudget);
        if (pcCase is null)
        {
            result.Notes.Add("No case fits the selected motherboard/GPU/PSU combination within budget.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.CaseId = pcCase.Id;

        var coolers = await cpuCoolerRepository.GetAllCpuCoolersAsync(cancellationToken);
        var compatibleCoolers = await FilterCompatibleAsync(coolers,
            cc => compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync(cpu.Id, cc.Id, cancellationToken));
        compatibleCoolers = await FilterCompatibleAsync(compatibleCoolers,
            cc => compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync(pcCase.Id, cc.Id, cancellationToken));
        var cooler = PickBest(compatibleCoolers, c => c.Price, requirements, BuildComponentType.CpuCooler, totalBudget);
        if (cooler is null)
        {
            result.Notes.Add("No CPU cooler fits the selected CPU/case combination within budget.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.CpuCoolerId = cooler.Id;

        var hardDrives = await hardDriveRepository.GetAllHardDrivesAsync(cancellationToken);
        var hardDrive = PickBest(hardDrives, h => h.Price, requirements, BuildComponentType.HardDrive, totalBudget);
        if (hardDrive is null)
        {
            result.Notes.Add("No suitable hard drive found within budget; build is missing storage.");
            result.Status = BuildRecommendationStatus.Failed;
            return result;
        }
        build.HardDriveId = hardDrive.Id;

        if (requirements.NeedsMonitor)
        {
            var monitors = await pcMonitorRepository.GetAllMonitorsAsync(cancellationToken);
            var monitorCandidates = FilterByTarget(monitors, requirements.TargetResolution);
            var monitor = PickBest(monitorCandidates, m => m.Price, requirements, BuildComponentType.PcMonitor, totalBudget);
            if (monitor is null)
            {
                result.Notes.Add("No suitable monitor found within budget.");
                result.Status = BuildRecommendationStatus.Failed;
                return result;
            }
            build.MonitorId = monitor.Id;
        }

        if (result.Notes.Count == 0)
        {
            result.Status = BuildRecommendationStatus.Completed;
        }

        return result;
    }

    private static async Task<List<T>> FilterCompatibleAsync<T>(
        IEnumerable<T> candidates,
        Func<T, Task<CompatibilityCheckResponse>> CheckAsync)
    {
        var passed = new List<T>();
        foreach (var candidate in candidates)
        {
            var check = await CheckAsync(candidate);
            if (check.IsSuccess)
            {
                passed.Add(candidate);
            }
        }
        return passed;
    }
    private static IEnumerable<T> FilterByTarget<T>(IEnumerable<T> candidates, string? target)
      //refine in future(GpuEntity has no resolution so it should be filtered by VramGb);
      => candidates;

    private static T? PickBest<T>(
        IEnumerable<T> candidates,
        Func<T, decimal?> PriceSelector,
        AiBuildRequirements requirements,
        BuildComponentType componentType,
        decimal totalBudget) where T : Component
    {
        var targetSpend = totalBudget * BudgetAllocationProfiles.GetShare(requirements.Purpose, componentType);

        var filtered = candidates
            .Where(c => requirements.AvoidBrands.Count == 0 ||
            !requirements.AvoidBrands.Contains(c.Brand?.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();
        if (filtered.Count == 0)
            return null;

        var withinBudget = filtered
            .Where(c => PriceSelector(c) is { } price && price <= targetSpend * 1.2m)
            .OrderByDescending(PriceSelector)
            .ToList();
        var pool = withinBudget.Count > 0
            ? withinBudget
            : filtered.OrderBy(c => PriceSelector(c) ?? decimal.MaxValue).Take(1).ToList();

        if (requirements.PreferredBrands.Count > 0)
        {
            var preffered = pool.FirstOrDefault(c =>
            requirements.PreferredBrands.Contains(c.Brand?.Name, StringComparer.OrdinalIgnoreCase));
            if (preffered is not null)
                return preffered;
        }
        return pool.FirstOrDefault();
    }
}
