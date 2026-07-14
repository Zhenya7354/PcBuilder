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

        var cpu = await SelectComponentAsync(
            cpuRepository.GetAllCpusAsync,
            c => c.Price,
            [],
            requirements,
            BuildComponentType.Cpu,
            totalBudget,
            cancellationToken);
        if (!TryAssign(cpu, "No Cpu found matching budget/priority target.", id => build.CpuId = id, result))
        {
            return result;
        }

        var motherboard = await SelectComponentAsync(
        motherboardRepository.GetAllMotherboardsAsync,
        m => m.Price,
        [mb => compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(cpu!.Id, mb.Id, cancellationToken)],
        requirements, BuildComponentType.Motherboard, totalBudget, cancellationToken);
        if (!TryAssign(motherboard, $"No motherboard compatible with {cpu?.Name ?? "selected Cpu"} within budget.", id => build.MotherboardId = id, result))
            return result;

        var ram = await SelectComponentAsync(
        ramRepository.GetAllRamAsync,
        r => r.Price,
        [r => compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(r.Id, motherboard!.Id, cancellationToken)],
        requirements, BuildComponentType.Ram, totalBudget, cancellationToken);
        if (!TryAssign(ram, $"No RAM compatible with {motherboard?.Name ?? "selected Motherboard"} within budget.", id => build.RamId = id, result))
            return result;

        var gpu = await SelectComponentAsync(
        async ct => FilterByResolution(await gpuRepository.GetAllGpusAsync(ct), requirements.TargetResolution).ToList(),
        g => g.Price,
        [],
        requirements, BuildComponentType.Gpu, totalBudget, cancellationToken);
        if (!TryAssign(gpu, "No GPU found matching budget/resolution target.", id => build.GpuId = id, result))
            return result;


        var psu = await SelectComponentAsync(
        async ct => (await psuRepository.GetAllPsusAsync(ct)).Where(p => p.Wattage >= gpu!.RecommendedPsuWattage).ToList(),
        p => p.Price,
        [],
        requirements, BuildComponentType.Psu, totalBudget, cancellationToken);
        if (!TryAssign(psu, $"No PSU rated for {gpu?.Name ?? "selected GPU"}'s {gpu?.RecommendedPsuWattage ?? 0}W requirement within budget.", id => build.PsuId = id, result))
            return result;


        var pcCase = await SelectComponentAsync(
        pcCaseRepository.GetAllCasesAsync,
        c => c.Price,
        [
            c => compatibilityCheckService.CheckCaseToMotherboardCompatibilityAsync(c.Id, motherboard!.Id, cancellationToken),
            c => compatibilityCheckService.CheckCaseToGpuCompatibilityAsync(c.Id, gpu!.Id, cancellationToken),
            c => compatibilityCheckService.CheckCaseToPsuCompatibilityAsync(c.Id, psu!.Id, cancellationToken),
        ],
        requirements, BuildComponentType.PcCase, totalBudget, cancellationToken);
        if (!TryAssign(pcCase, "No case fits the selected motherboard/GPU/PSU combination within budget.", id => build.CaseId = id, result))
            return result;


        var cooler = await SelectComponentAsync(
        cpuCoolerRepository.GetAllCpuCoolersAsync,
        c => c.Price,
        [
            cc => compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync(cpu!.Id, cc.Id, cancellationToken),
            cc => compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync(pcCase!.Id, cc.Id, cancellationToken),
        ],
        requirements, BuildComponentType.CpuCooler, totalBudget, cancellationToken);
        if (!TryAssign(cooler, "No CPU cooler fits the selected CPU/case combination within budget.", id => build.CpuCoolerId = id, result))
            return result;


        var hardDrive = await SelectComponentAsync(
        hardDriveRepository.GetAllHardDrivesAsync,
        h => h.Price,
        [],
        requirements, BuildComponentType.HardDrive, totalBudget, cancellationToken);
        AssignOptional(hardDrive, "No suitable hard drive found within budget; build is missing storage.", id => build.HardDriveId = id, result);

        if (requirements.NeedsMonitor)
        {
            var monitor = await SelectComponentAsync(
                async ct => FilterByResolution(await pcMonitorRepository.GetAllMonitorsAsync(ct), requirements.TargetResolution).ToList(),
                m => m.Price,
                [],
                requirements, BuildComponentType.PcMonitor, totalBudget, cancellationToken);
            AssignOptional(monitor, "No suitable monitor found within budget.", id => build.MonitorId = id, result);
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
    private static IEnumerable<T> FilterByResolution<T>(IEnumerable<T> candidates, string? target)
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

    private async Task<T?> SelectComponentAsync<T>(
        Func<CancellationToken, Task<List<T>>> pullCandidates,
        Func<T, decimal?> priceSelector,
        List<Func<T, Task<CompatibilityCheckResponse>>> compatibilityChecks,
        AiBuildRequirements requirements,
        BuildComponentType componentType,
        decimal totalBudget,
        CancellationToken cancellationToken) where T : Component
    {
        var candidates = await pullCandidates(cancellationToken);

        foreach (var check in compatibilityChecks)
        {
            candidates = await FilterCompatibleAsync(candidates, check);
        }
        return PickBest(candidates, priceSelector, requirements, componentType, totalBudget);
    }

    private static bool TryAssign<T>(T? component,
        string failureMessage,
        Action<int> assign,
        BuildRecommendationResult result) where T : Component
    {
        if (component is null)
        {
            result.Notes.Add(failureMessage);
            result.Status = BuildRecommendationStatus.Failed;
            return false;
        }
        assign(component.Id);
        return true;
    }
    private static void AssignOptional<T>(T? component,
        string noteMessage,
        Action<int> assign,
        BuildRecommendationResult result) where T : Component
    {
        if (component is null)
        {
            result.Notes.Add(noteMessage);
        }
        else
        {
            assign(component.Id);
        }
    }
}
