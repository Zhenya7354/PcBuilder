using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class PcCaseRepository(PcDbContext context) : IPcCaseRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcCaseEntity>> GetAllCasesAsync()
    {
        return await _context.PcCase.Include(c => c.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<PcCaseEntity?> GetCaseByIdAsync(int id)
    {
        return await _context.PcCase.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddCaseAsync(PcCaseEntity pcCase)
    {
        await _context.PcCase.AddAsync(pcCase);
    }

    public async Task DeleteCaseAsync(PcCaseEntity pcCase)
    {
        await _context.PcCase.Where(c => c.Id == pcCase.Id).ExecuteDeleteAsync();
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
