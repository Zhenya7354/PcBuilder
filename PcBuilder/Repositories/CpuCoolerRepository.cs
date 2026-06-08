using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class CpuCoolerRepository(PcDbContext context) : ICpuCoolerRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken)
    {
        return await _context.CpuCooler.Include(c => c.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.CpuCooler.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken)
    {
        await _context.CpuCooler.AddAsync(cpuCooler, cancellationToken);
    }

    public async Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken)
    {
        await _context.CpuCooler.Where(c => c.Id == cpuCooler.Id).ExecuteDeleteAsync(cancellationToken);
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
