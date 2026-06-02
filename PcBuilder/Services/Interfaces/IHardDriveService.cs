using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IHardDriveService
{
    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync();
    public Task<HardDriveEntity> GetHardDriveByIdAsync(int id);
    public Task<HardDriveEntity> AddHardDriveAsync(HardDriveCreateRequest dto);
    public Task<HardDriveEntity> UpdateHardDriveAsync(int id, HardDriveUpdateRequest dto);
    public Task DeleteHardDriveAsync(int id);
}
