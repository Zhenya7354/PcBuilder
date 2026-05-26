using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPsuService
{
    public Task<List<PsuEntity>> GetAllPsusAsync();
    public Task<PsuEntity> GetPsuByIdAsync(int id);
    public Task<PsuEntity> AddPsuAsync(PsuCreate dto);
    public Task<PsuEntity> UpdatePsuAsync(int id, PsuUpdate dto);
    public Task DeletePsuAsync(int id);
}
