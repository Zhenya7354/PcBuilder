using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IMotherboardService
{
    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync();
    public Task<MotherboardEntity> GetMotherboardByIdAsync(int id);
    public Task<MotherboardEntity> AddMotherboardAsync(MotherboardCreate dto);
    public Task<MotherboardEntity> UpdateMotherboardAsync(int id, MotherboardUpdate dto);
    public Task DeleteMotherboardAsync(int id);
}
