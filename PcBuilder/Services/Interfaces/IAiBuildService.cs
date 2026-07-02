using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IAiBuildService
{
    Task<AiBuildRequirements> AnalyzeAsync(string prompt, CancellationToken cancellation);
}
