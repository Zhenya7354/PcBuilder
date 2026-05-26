using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class CpuCoolerRepository(PcDbContext context) : ICpuCoolerRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync()
    {
        return await _context.CpuCooler.Include(c => c.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id)
    {
        return await _context.CpuCooler.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler)
    {
        await _context.CpuCooler.AddAsync(cpuCooler);
    }

    public async Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler)
    {
        await _context.CpuCooler.Where(c => c.Id == cpuCooler.Id).ExecuteDeleteAsync();
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
