using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IRamService
{
    public Task<List<RamEntity>> GetAllRamAsync();
    public Task<RamEntity> GetRamByIdAsync(int id);
    public Task<RamEntity> AddRamAsync(RamCreateRequest dto);
    public Task<RamEntity> UpdateRamAsync(int id, RamUpdateRequest dto);
    public Task DeleteRamAsync(int id);
}
