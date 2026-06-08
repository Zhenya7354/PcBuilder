using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IRamRepository
{
    public Task<List<RamEntity>> GetAllRamAsync(CancellationToken cancellationToken);
    public Task<RamEntity?> GetRamByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddRamAsync(RamEntity ram, CancellationToken cancellationToken);
    public Task DeleteRamAsync(RamEntity ram, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
