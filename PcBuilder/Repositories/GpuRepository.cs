using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class GpuRepository(PcDbContext context) : IGpuRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<GpuEntity>> GetAllGpusAsync(CancellationToken cancellationToken)
    {
        return await _context.Gpu.Include(g => g.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<GpuEntity?> GetGpuByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Gpu.Include(g => g.Brand).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task AddGpuAsync(GpuEntity gpu, CancellationToken cancellationToken)
    {
        await _context.Gpu.AddAsync(gpu, cancellationToken);
    }
    public async Task DeleteGpuAsync(GpuEntity gpu, CancellationToken cancellationToken)
    {
        await _context.Gpu.Where(g => g.Id == gpu.Id).ExecuteDeleteAsync(cancellationToken);
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
