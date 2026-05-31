using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class CompatibilityCheckRepository(PcDbContext context) : ICompatibilityCheckRepository
{
    private readonly PcDbContext _context = context;
    

    public async Task<PcCaseEntity?> GetCaseByIdAsync(int caseId)
    {
        return await _context.PcCase.AsNoTracking().FirstOrDefaultAsync(c => c.Id == caseId);
    }

    public async Task<CpuEntity?> GetCpuByIdAsync(int cpuId)
    {
        return await _context.Cpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuId);
    }

    public async Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int cpuCoolerId)
    {
        return await _context.CpuCooler.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuCoolerId);
    }

    public async Task<GpuEntity?> GetGpuByIdAsync(int gpuId)
    {
        return await _context.Gpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == gpuId);
    }

    public async Task<HardDriveEntity?> GetHardDriveByIdAsync(int hardDriveId)
    {
        return await _context.HardDrive.AsNoTracking().FirstOrDefaultAsync(c => c.Id == hardDriveId);
    }

    public async Task<PcMonitorEntity?> GetMonitorByIdAsync(int monitorId)
    {
        return await _context.PcMonitor.AsNoTracking().FirstOrDefaultAsync(c => c.Id == monitorId);
    }

    public async Task<MotherboardEntity?> GetMotherboardByIdAsync(int motherboardId)
    {
        return await _context.Motherboard.AsNoTracking().FirstOrDefaultAsync(c => c.Id == motherboardId);
    }

    public async Task<PsuEntity?> GetPsuByIdAsync(int psuId)
    {
        return await _context.Psu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == psuId);
    }

    public async Task<RamEntity?> GetRamByIdAsync(int ramId)
    {
        return await _context.Ram.AsNoTracking().FirstOrDefaultAsync(c => c.Id == ramId);
    }
}
