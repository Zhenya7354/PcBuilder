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
        return await _context.Gpu.Include(g => g.Brand).ToListAsync();
    }
    public async Task<GpuEntity?> GetGpuByIdAsync(int id)
    {
        return await _context.Gpu.Include(g => g.Brand).FirstOrDefaultAsync(g => g.Id == id);
    }

    public  Task AddGpuAsync(GpuEntity gpu)
    {
        _context.Gpu.Add(gpu);
        return Task.CompletedTask;
    }
    public  Task DeleteGpuAsync(GpuEntity gpu)
    {
        _context.Gpu.Remove(gpu);
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
