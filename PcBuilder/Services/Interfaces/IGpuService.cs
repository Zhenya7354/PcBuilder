using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IGpuService
{
    public Task<List<GpuEntity>> GetGpusAsync();
    public Task<GpuEntity> GetGpuById(int id);
    public Task<GpuEntity> AddGpuAsync(GpuCreateRequest gpuDto);
    public Task<GpuEntity> UpdateGpuAsync(int id, GpuUpdateRequest gpuDto);
    public Task DeleteGpuAsync(int id);
}
