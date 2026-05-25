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
        return await _context.CpuCooler.Include(c => c.Brand).ToListAsync();
    }

    public async Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id)
    {
        return await _context.CpuCooler.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler)
    {
        _context.CpuCooler.Add(cpuCooler);
        return Task.CompletedTask;
    }

    public Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler)
    {
        _context.CpuCooler.Remove(cpuCooler);
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
