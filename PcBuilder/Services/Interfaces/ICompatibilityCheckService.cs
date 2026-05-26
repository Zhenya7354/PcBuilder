using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICompatibilityCheckService
{
    public Task<CompatibilityCheckResponse> CheckCpuToMotherboardCompatibilityAsync(int cpuId, int motherboardId);
    public Task<CompatibilityCheckResponse> CheckCpuCoolerToCpuCompatibilityAsync(int cpuId, int cpuCoolerId);
    public Task<CompatibilityCheckResponse> CheckCpuToRamCompatibilityAsync(int cpuId, int ramId);
    public Task<CompatibilityCheckResponse> CheckRamToMotherboardCompatibilityAsync(int ramId, int motherboardId);
    public Task<CompatibilityCheckResponse> CheckCaseToMotherboardCompatibilityAsync(int caseId, int motherboardId);
    public Task<CompatibilityCheckResponse> CheckCaseToCpuCoolerCompatibilityAsync(int caseId, int cpuCoolerId);
    public Task<CompatibilityCheckResponse> CheckCaseToGpuCompatibilityAsync(int caseId, int gpuId);
    public Task<CompatibilityCheckResponse> CheckCaseToPsuCompatibilityAsync(int caseId, int psuId);
    public Task<CompatibilityCheckResponse> CheckPsuToGpuCompatibilityAsync(int psuId, int gpuId);
}
