using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using System.Data;

namespace PcBuilder.Repositories;

public class CpuRepository(PcDbContext context) : ICpuRepository
{
    private readonly PcDbContext _context = context;

    public async Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId, cancellationToken);
    }


    public async Task<List<CpuEntity>> GetAllCpusAsync(CancellationToken cancellationToken)
    {
        return await _context.Cpu.Include(c => c.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CpuEntity?> GetCpuByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Cpu.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddCpuAsync(CpuEntity cpu, CancellationToken cancellationToken)
    {
        await _context.Cpu.AddAsync(cpu, cancellationToken);
    }

    public async Task DeleteCpuAsync(CpuEntity cpu, CancellationToken cancellationToken)
    {
        await _context.Cpu.Where(c => c.Id == cpu.Id).ExecuteDeleteAsync(cancellationToken);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

}
