using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class MotherboardRepository(PcDbContext context) : IMotherboardRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<MotherboardEntity>> GetAllMotherboardsAsync()
    {
        return await _context.Motherboard.Include(m => m.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<MotherboardEntity?> GetMotherboardByIdAsync(int id)
    {
        return await _context.Motherboard.Include(m => m.Brand).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddMotherboardAsync(MotherboardEntity motherboard)
    {
        await _context.Motherboard.AddAsync(motherboard);
    }

    public async Task DeleteMotherboardAsync(MotherboardEntity motherboard)
    {
        await _context.Motherboard.Where(m => m.Id == motherboard.Id).ExecuteDeleteAsync();
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
