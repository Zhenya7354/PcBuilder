using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class PcCaseRepository(PcDbContext context) : IPcCaseRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken)
    {
        return await _context.PcCase.Include(c => c.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PcCaseEntity?> GetCaseByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.PcCase.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken)
    {
        await _context.PcCase.AddAsync(pcCase, cancellationToken);
    }

    public async Task DeleteCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken)
    {
        await _context.PcCase.Where(c => c.Id == pcCase.Id).ExecuteDeleteAsync(cancellationToken);
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
