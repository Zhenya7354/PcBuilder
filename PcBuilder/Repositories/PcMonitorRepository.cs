using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using System.Xml.Schema;

namespace PcBuilder.Repositories;

public class PcMonitorRepository(PcDbContext context) : IPcMonitorRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken)
    {
        return await _context.PcMonitor.Include(m => m.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PcMonitorEntity?> GetMonitorByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.PcMonitor.Include(m => m.Brand).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken)
    {
        await _context.PcMonitor.AddAsync(monitor, cancellationToken);
    }

    public async Task DeleteMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken)
    {
        await _context.PcMonitor.Where(m => m.Id == monitor.Id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
