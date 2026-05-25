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
        return await _context.Motherboard.Include(m => m.Brand).ToListAsync();
    }

    public async Task<MotherboardEntity?> GetMotherboardByIdAsync(int id)
    {
        return await _context.Motherboard.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
    }

    public Task AddMotherboardAsync(MotherboardEntity motherboard)
    {
        _context.Motherboard.Add(motherboard);
        return Task.CompletedTask;
    }

    public Task DeleteMotherboardAsync(MotherboardEntity motherboard)
    {
        _context.Motherboard.Remove(motherboard);
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
