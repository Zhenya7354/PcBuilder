using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPsuService
{
    public Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken);
    public Task<PsuEntity> GetPsuByIdAsync(int id, CancellationToken cancellationToken);
    public Task<PsuEntity> AddPsuAsync(PsuCreateRequest dto, CancellationToken cancellationToken);
    public Task<PsuEntity> UpdatePsuAsync(int id, PsuUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeletePsuAsync(int id, CancellationToken cancellationToken);
}
