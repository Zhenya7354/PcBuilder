using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class GpuRepository(PcDbContext context) : IGpuRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<GpuEntity>> GetAllGpusAsync()
    {
        return await _context.Gpu.Include(g => g.Brand).AsNoTracking().ToListAsync();
    }
    public async Task<GpuEntity?> GetGpuByIdAsync(int id)
    {
        return await _context.Gpu.Include(g => g.Brand).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task AddGpuAsync(GpuEntity gpu)
    {
        await _context.Gpu.AddAsync(gpu);
    }
    public async Task DeleteGpuAsync(GpuEntity gpu)
    {
        await _context.Gpu.Where(g => g.Id == gpu.Id).ExecuteDeleteAsync();
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
