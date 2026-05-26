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

    public async Task<bool> BrandExistsAsync(int brandId)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId);
    }


    public async Task<List<CpuEntity>> GetAllCpusAsync()
    {
        return await _context.Cpu.Include(c => c.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<CpuEntity?> GetCpuByIdAsync(int id)
    {
        return await _context.Cpu.Include(c => c.Brand).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddCpuAsync(CpuEntity cpu)
    {
        await _context.Cpu.AddAsync(cpu);
    }

    public async Task DeleteCpuAsync(CpuEntity cpu)
    {
        await _context.Cpu.Where(c => c.Id == cpu.Id).ExecuteDeleteAsync(); 
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
