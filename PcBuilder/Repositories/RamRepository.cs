using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class RamRepository(PcDbContext context) : IRamRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<RamEntity>> GetAllRamAsync()
    {
        return await _context.Ram.Include(r => r.Brand).ToListAsync();
    }

    public async Task<RamEntity?> GetRamByIdAsync(int id)
    {
        return await _context.Ram.Include(r => r.Brand).FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task AddRamAsync(RamEntity ram)
    {
        _context.Ram.Add(ram);
        return Task.CompletedTask;
    }

    public Task DeleteRamAsync(RamEntity ram)
    {
        _context.Ram.Remove(ram);
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
