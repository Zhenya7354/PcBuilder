using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class PsuRepository(PcDbContext context) : IPsuRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken)
    {
        return await _context.Psu.Include(p => p.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PsuEntity?> GetPsuByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Psu.Include(p => p.Brand).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddPsuAsync(PsuEntity psu, CancellationToken cancellationToken)
    {
        await _context.Psu.AddAsync(psu, cancellationToken);
    }

    public async Task DeletePsuAsync(PsuEntity psu, CancellationToken cancellationToken)
    {
        await _context.Psu.Where(p => p.Id == psu.Id).ExecuteDeleteAsync(cancellationToken);
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
