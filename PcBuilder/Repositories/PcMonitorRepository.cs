using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using System.Xml.Schema;

namespace PcBuilder.Repositories;

public class PcMonitorRepository(PcDbContext context) : IPcMonitorRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcMonitorEntity>> GetAllMonitorsAsync()
    {
        return await _context.PcMonitor.Include(m => m.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<PcMonitorEntity?> GetMonitorByIdAsync(int id)
    {
        return await _context.PcMonitor.Include(m => m.Brand).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddMonitorAsync(PcMonitorEntity monitor)
    {
        await _context.PcMonitor.AddAsync(monitor);
    }

    public async Task DeleteMonitorAsync(PcMonitorEntity monitor)
    {
        await _context.PcMonitor.Where(m => m.Id == monitor.Id).ExecuteDeleteAsync();
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
