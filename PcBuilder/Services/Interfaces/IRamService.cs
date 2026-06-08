using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IRamService
{
    public Task<List<RamEntity>> GetAllRamAsync(CancellationToken cancellationToken);
    public Task<RamEntity> GetRamByIdAsync(int id, CancellationToken cancellationToken);
    public Task<RamEntity> AddRamAsync(RamCreateRequest dto, CancellationToken cancellationToken);
    public Task<RamEntity> UpdateRamAsync(int id, RamUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteRamAsync(int id, CancellationToken cancellationToken);
}
