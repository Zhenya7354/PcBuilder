using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPsuService
{
    public Task<List<PsuEntity>> GetAllPsusAsync();
    public Task<PsuEntity> GetPsuByIdAsync(int id);
    public Task<PsuEntity> AddPsuAsync(PsuCreateRequest dto);
    public Task<PsuEntity> UpdatePsuAsync(int id, PsuUpdateRequest dto);
    public Task DeletePsuAsync(int id);
}
