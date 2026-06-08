using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPcMonitorService
{
    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken);
    public Task<PcMonitorEntity> GetMonitorByIdAsync(int id, CancellationToken cancellationToken);
    public Task<PcMonitorEntity> AddMonitorAsync(PcMonitorCreateRequest dto, CancellationToken cancellationToken);
    public Task<PcMonitorEntity> UpdateMonitorAsync(int id, PcMonitorUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteMonitorAsync(int id, CancellationToken cancellationToken);
}
