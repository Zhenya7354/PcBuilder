using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class BuildRepository(PcDbContext context) : IBuildRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<BuildEntity>> GetAllAsync(string userId, CancellationToken cancellationToken)
    {
        return await BuildWithAllComponents().AsNoTracking().Where(b => b.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<BuildEntity?> GetByIdAsync(int buildId, string userId, CancellationToken cancellationToken)
    {
        return await BuildWithAllComponents().AsNoTracking().FirstOrDefaultAsync(b => b.Id == buildId && b.UserId == userId, cancellationToken);
    }
    public async Task AddBuildAsync(BuildEntity build, CancellationToken cancellationToken)
    {
        await _context.AddAsync(build, cancellationToken);
    }


    public async Task DeleteBuildAsync(BuildEntity build, CancellationToken cancellationToken)
    {
        await _context.Build.Where(b => b.Id == build.Id).ExecuteDeleteAsync(cancellationToken);
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
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

    public async Task<bool> CpuExistsAsync(int cpuId, CancellationToken cancellationToken)
    {
        return await _context.Cpu.AnyAsync(c => c.Id == cpuId, cancellationToken);
    }

    public async Task<bool> CpuCoolerExistsAsync(int cpuCoolerId, CancellationToken cancellationToken)
    {
        return await _context.CpuCooler.AnyAsync(cc => cc.Id == cpuCoolerId, cancellationToken);
    }

    public async Task<bool> GpuExistsAsync(int gpuId, CancellationToken cancellationToken)
    {
        return await _context.Gpu.AnyAsync(g => g.Id == gpuId, cancellationToken);
    }

    public async Task<bool> RamExistsAsync(int ramId, CancellationToken cancellationToken)
    {
        return await _context.Ram.AnyAsync(r => r.Id == ramId, cancellationToken);
    }
    

    public async Task<bool> HardDriveExistsAsync(int hardDriveId, CancellationToken cancellationToken)
    {
       return await _context.HardDrive.AnyAsync(hd => hd.Id == hardDriveId, cancellationToken);
    }

    public async Task<bool> MotherboardExistsAsync(int motherboardId, CancellationToken cancellationToken)
    {
        return await _context.Motherboard.AnyAsync(m => m.Id == motherboardId, cancellationToken);
    }

    public async Task<bool> PsuExistsAsync(int psuId, CancellationToken cancellationToken)
    {
        return await _context.Psu.AnyAsync(p => p.Id == psuId, cancellationToken);
    }

    public async Task<bool> CaseExistsAsync(int caseId, CancellationToken cancellationToken)
    {
        return await _context.PcCase.AnyAsync(c => c.Id == caseId, cancellationToken);
    }

    public async Task<bool> MonitorExistsAsync(int monitorId, CancellationToken cancellationToken)
    {
        return await _context.PcMonitor.AnyAsync(m => m.Id == monitorId, cancellationToken);
    }
}
