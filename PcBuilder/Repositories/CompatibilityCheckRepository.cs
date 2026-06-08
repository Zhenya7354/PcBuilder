using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class CompatibilityCheckRepository(PcDbContext context) : ICompatibilityCheckRepository
{
    private readonly PcDbContext _context = context;
    

    public async Task<PcCaseEntity?> GetCaseByIdAsync(int caseId, CancellationToken cancellationToken)
    {
        return await _context.PcCase.AsNoTracking().FirstOrDefaultAsync(c => c.Id == caseId, cancellationToken);
    }

    public async Task<CpuEntity?> GetCpuByIdAsync(int cpuId, CancellationToken cancellationToken)
    {
        return await _context.Cpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuId, cancellationToken);
    }

    public async Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int cpuCoolerId, CancellationToken cancellationToken)
    {
        return await _context.CpuCooler.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuCoolerId, cancellationToken);
    }

    public async Task<GpuEntity?> GetGpuByIdAsync(int gpuId, CancellationToken cancellationToken)
    {
        return await _context.Gpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == gpuId, cancellationToken);
    }

    public async Task<HardDriveEntity?> GetHardDriveByIdAsync(int hardDriveId, CancellationToken cancellationToken)
    {
        return await _context.HardDrive.AsNoTracking().FirstOrDefaultAsync(c => c.Id == hardDriveId, cancellationToken);
    }

    public async Task<PcMonitorEntity?> GetMonitorByIdAsync(int monitorId, CancellationToken cancellationToken)
    {
        return await _context.PcMonitor.AsNoTracking().FirstOrDefaultAsync(c => c.Id == monitorId, cancellationToken);
    }

    public async Task<MotherboardEntity?> GetMotherboardByIdAsync(int motherboardId, CancellationToken cancellationToken)
    {
        return await _context.Motherboard.AsNoTracking().FirstOrDefaultAsync(c => c.Id == motherboardId, cancellationToken);
    }

    public async Task<PsuEntity?> GetPsuByIdAsync(int psuId, CancellationToken cancellationToken)
    {
        return await _context.Psu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == psuId, cancellationToken);
    }

    public async Task<RamEntity?> GetRamByIdAsync(int ramId, CancellationToken cancellationToken)
    {
        return await _context.Ram.AsNoTracking().FirstOrDefaultAsync(c => c.Id == ramId, cancellationToken);
    }
}
