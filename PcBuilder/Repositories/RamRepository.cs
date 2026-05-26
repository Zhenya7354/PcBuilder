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
        return await _context.Ram.Include(r => r.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<RamEntity?> GetRamByIdAsync(int id)
    {
        return await _context.Ram.Include(r => r.Brand).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddRamAsync(RamEntity ram)
    {
        await _context.Ram.AddAsync(ram);
    }

    public async Task DeleteRamAsync(RamEntity ram)
    {
        await _context.Ram.Where(r => r.Id == ram.Id).ExecuteDeleteAsync();
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
