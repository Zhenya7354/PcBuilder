using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IHardDriveService
{
    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken);
    public Task<HardDriveEntity> GetHardDriveByIdAsync(int id, CancellationToken cancellationToken);
    public Task<HardDriveEntity> AddHardDriveAsync(HardDriveCreateRequest dto, CancellationToken cancellationToken);
    public Task<HardDriveEntity> UpdateHardDriveAsync(int id, HardDriveUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteHardDriveAsync(int id, CancellationToken cancellationToken);
}
