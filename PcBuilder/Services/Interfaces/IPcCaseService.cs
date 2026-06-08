using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPcCaseService
{
    public Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken);
    public Task<PcCaseEntity> GetCaseByIdAsync(int id, CancellationToken cancellationToken);
    public Task<PcCaseEntity> AddCaseAsync(PcCaseCreateRequest dto, CancellationToken cancellationToken);
    public Task<PcCaseEntity> UpdateCaseAsync(int id, PcCaseUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteCaseAsync(int id, CancellationToken cancellationToken);
}
