using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IPcMonitorService
{
    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync();
    public Task<PcMonitorEntity> GetMonitorByIdAsync(int id);
    public Task<PcMonitorEntity> AddMonitorAsync(PcMonitorCreate dto);
    public Task<PcMonitorEntity> UpdateMonitorAsync(int id, PcMonitorUpdate dto);
    public Task DeleteMonitorAsync(int id);
}
