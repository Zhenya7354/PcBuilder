using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class PcMonitorRepository(PcDbContext context) : IPcMonitorRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcMonitorEntity>> GetAllMonitorsAsync()
    {
        return await _context.PcMonitor.Include(m => m.Brand).ToListAsync();
    }

    public async Task<PcMonitorEntity?> GetMonitorByIdAsync(int id)
    {
        return await _context.PcMonitor.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
    }

    public Task AddMonitor(PcMonitorEntity monitor)
    {
        _context.PcMonitor.Add(monitor);
        return Task.CompletedTask;
    }

    public Task DeleteMonitor(PcMonitorEntity monitor)
    {
        _context.PcMonitor.Remove(monitor);
        return Task.CompletedTask;
    }

    public async Task<bool> BrandExistsAsync(int brandId)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
