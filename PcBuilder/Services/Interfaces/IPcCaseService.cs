using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPcCaseService
{
    public Task<List<PcCaseEntity>> GetAllCasesAsync();
    public Task<PcCaseEntity> GetCaseByIdAsync(int id);
    public Task<PcCaseEntity> AddCaseAsync(PcCaseCreate dto);
    public Task<PcCaseEntity> UpdateCaseAsync(int id, PcCaseUpdate dto);
    public Task DeleteCaseAsync(int id);
}
