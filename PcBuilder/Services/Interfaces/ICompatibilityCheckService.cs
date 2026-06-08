using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICompatibilityCheckService
{
    public Task<CompatibilityCheckResponse> CheckCpuToMotherboardCompatibilityAsync(int cpuId, int motherboardId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCpuCoolerToCpuCompatibilityAsync(int cpuId, int cpuCoolerId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCpuToRamCompatibilityAsync(int cpuId, int ramId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckRamToMotherboardCompatibilityAsync(int ramId, int motherboardId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCaseToMotherboardCompatibilityAsync(int caseId, int motherboardId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCaseToCpuCoolerCompatibilityAsync(int caseId, int cpuCoolerId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCaseToGpuCompatibilityAsync(int caseId, int gpuId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckCaseToPsuCompatibilityAsync(int caseId, int psuId, CancellationToken cancellationToken);
    public Task<CompatibilityCheckResponse> CheckPsuToGpuCompatibilityAsync(int psuId, int gpuId, CancellationToken cancellationToken);
}
