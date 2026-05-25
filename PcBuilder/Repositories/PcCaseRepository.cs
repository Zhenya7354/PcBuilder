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
        return await _context.PcCase.Include(c => c.Brand).ToListAsync();
    }

    public async Task<PcCaseEntity?> GetCaseByIdAsync(int id)
    {
        return await _context.PcCase.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task AddCaseAsync(PcCaseEntity pcCase)
    {
        _context.PcCase.Add(pcCase);
        return Task.CompletedTask;
    }

    public Task DeleteCaseAsync(PcCaseEntity pcCase)
    {
        _context.PcCase.Remove(pcCase);
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
