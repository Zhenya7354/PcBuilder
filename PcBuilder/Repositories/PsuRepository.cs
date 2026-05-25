using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class PsuRepository(PcDbContext context) : IPsuRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PsuEntity>> GetAllPsusAsync()
    {
        return await _context.Psu.Include(p => p.Brand).ToListAsync();
    }

    public async Task<PsuEntity?> GetPsuByIdAsync(int id)
    {
        return await _context.Psu.Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task AddPsuAsync(PsuEntity psu)
    {
        _context.Psu.Add(psu);
        return Task.CompletedTask;
    }

    public Task DeletePsuAsync(PsuEntity psu)
    {
        _context.Psu.Remove(psu);
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
