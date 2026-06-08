using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IGpuService
{
    public Task<List<GpuEntity>> GetGpusAsync(CancellationToken cancellationToken);
    public Task<GpuEntity> GetGpuById(int id, CancellationToken cancellationToken);
    public Task<GpuEntity> AddGpuAsync(GpuCreateRequest gpuDto, CancellationToken cancellationToken);
    public Task<GpuEntity> UpdateGpuAsync(int id, GpuUpdateRequest gpuDto, CancellationToken cancellationToken);
    public Task DeleteGpuAsync(int id, CancellationToken cancellationToken);
}
