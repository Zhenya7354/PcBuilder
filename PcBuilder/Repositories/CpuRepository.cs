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
        return await _context.Cpu.Include(c => c.Brand).ToListAsync();
    }

    public async Task<CpuEntity?> GetCpuByIdAsync(int id)
    {
        return await _context.Cpu.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task AddCpu(CpuEntity cpu)
    {
         _context.Cpu.Add(cpu);
        return Task.CompletedTask;
    }

    public Task DeleteCpu(CpuEntity cpu)
    {
        _context.Cpu.Remove(cpu);
        return Task.CompletedTask;
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
