using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class RamRepository(PcDbContext context) : IRamRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<RamEntity>> GetAllRamAsync(CancellationToken cancellationToken)
    {
        return await _context.Ram.Include(r => r.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<RamEntity?> GetRamByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Ram.Include(r => r.Brand).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task AddRamAsync(RamEntity ram, CancellationToken cancellationToken)
    {
        await _context.Ram.AddAsync(ram, cancellationToken);
    }

    public async Task DeleteRamAsync(RamEntity ram, CancellationToken cancellationToken)
    {
        await _context.Ram.Where(r => r.Id == ram.Id).ExecuteDeleteAsync(cancellationToken);
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
