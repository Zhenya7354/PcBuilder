using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IMotherboardRepository
{
    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken);
    public Task<MotherboardEntity?> GetMotherboardByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken);
    public Task DeleteMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
