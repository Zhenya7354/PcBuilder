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
        return await _context.Psu.Include(p => p.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<PsuEntity?> GetPsuByIdAsync(int id)
    {
        return await _context.Psu.Include(p => p.Brand).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddPsuAsync(PsuEntity psu)
    {
        await _context.Psu.AddAsync(psu);
    }

    public async Task DeletePsuAsync(PsuEntity psu)
    {
        await _context.Psu.Where(p => p.Id == psu.Id).ExecuteDeleteAsync();
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
