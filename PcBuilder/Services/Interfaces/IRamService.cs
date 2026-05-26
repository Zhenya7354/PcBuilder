using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IRamService
{
    public Task<List<RamEntity>> GetAllRamAsync();
    public Task<RamEntity> GetRamByIdAsync(int id);
    public Task<RamEntity> AddRamAsync(RamCreate dto);
    public Task<RamEntity> UpdateRamAsync(int id, RamUpdate dto);
    public Task DeleteRamAsync(int id);
}
