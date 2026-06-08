using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IMotherboardService
{
    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken);
    public Task<MotherboardEntity> GetMotherboardByIdAsync(int id, CancellationToken cancellationToken);
    public Task<MotherboardEntity> AddMotherboardAsync(MotherboardCreateRequest dto, CancellationToken cancellationToken);
    public Task<MotherboardEntity> UpdateMotherboardAsync(int id, MotherboardUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteMotherboardAsync(int id, CancellationToken cancellationToken);
}
