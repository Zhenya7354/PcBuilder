using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class MotherboardRepository(PcDbContext context) : IMotherboardRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken)
    {
        return await _context.Motherboard.Include(m => m.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<MotherboardEntity?> GetMotherboardByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Motherboard.Include(m => m.Brand).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken)
    {
        await _context.Motherboard.AddAsync(motherboard, cancellationToken);
    }

    public async Task DeleteMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken)
    {
        await _context.Motherboard.Where(m => m.Id == motherboard.Id).ExecuteDeleteAsync(cancellationToken);
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
