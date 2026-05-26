using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class BuildRepository(PcDbContext context) : IBuildRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<BuildEntity>> GetAllAsync(string userId)
    {
        return await BuildWithAllComponents().AsNoTracking().Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<BuildEntity?> GetByIdAsync(int buildId, string userId)
    {
        return await BuildWithAllComponents().AsNoTracking().FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId);
    }
    public async Task AddBuildAsync(BuildEntity build)
    {
        await _context.AddAsync(build);
    }


    public async Task DeleteBuildAsync(BuildEntity build)
    {
        await _context.Build.Where(b => b.Id == build.Id).ExecuteDeleteAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    private IQueryable<BuildEntity> BuildWithAllComponents()
    {
        return _context.Build.Include(b => b.Cpu)
                    .Include(b => b.CpuCooler)
                    .Include(b => b.Gpu)
                    .Include(b => b.Ram)
                    .Include(b => b.HardDrive)
                    .Include(b => b.Motherboard)
                    .Include(b => b.Psu)
                    .Include(b => b.PcCase)
                    .Include(b => b.Monitor);
    }

    public async Task<bool> CpuExistsAsync(int cpuId)
    {
        return await _context.Cpu.AnyAsync(c => c.Id == cpuId);
    }

    public async Task<bool> CpuCoolerExistsAsync(int cpuCoolerId)
    {
        return await _context.CpuCooler.AnyAsync(cc => cc.Id == cpuCoolerId);
    }

    public async Task<bool> GpuExistsAsync(int gpuId)
    {
        return await _context.Gpu.AnyAsync(g => g.Id == gpuId);
    }

    public async Task<bool> RamExistsAsync(int ramId)
    {
        return await _context.Ram.AnyAsync(r => r.Id == ramId);
    }
    

    public async Task<bool> HardDriveExistsAsync(int hardDriveId)
    {
       return await _context.HardDrive.AnyAsync(hd => hd.Id == hardDriveId);
    }

    public async Task<bool> MotherboardExistsAsync(int motherboardId)
    {
        return await _context.Motherboard.AnyAsync(m => m.Id == motherboardId);
    }

    public async Task<bool> PsuExistsAsync(int psuId)
    {
        return await _context.Psu.AnyAsync(p => p.Id == psuId);
    }

    public async Task<bool> CaseExistsAsync(int caseId)
    {
        return await _context.PcCase.AnyAsync(c => c.Id == caseId);
    }

    public async Task<bool> MonitorExistsAsync(int monitorId)
    {
        return await _context.PcMonitor.AnyAsync(m => m.Id == monitorId);
    }
}
