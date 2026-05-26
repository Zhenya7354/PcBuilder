using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class CompatibilityCheckRepository(PcDbContext context) : ICompatibilityCheckRepository
{
    private readonly PcDbContext _context = context;
    

    async Task<PcCaseEntity?> ICompatibilityCheckRepository.GetCaseByIdAsync(int caseId)
    {
        return await _context.PcCase.AsNoTracking().FirstOrDefaultAsync(c => c.Id == caseId);
    }

    async Task<CpuEntity?> ICompatibilityCheckRepository.GetCpuByIdAsync(int cpuId)
    {
        return await _context.Cpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuId);
    }

    async Task<CpuCoolerEntity?> ICompatibilityCheckRepository.GetCpuCoolerByIdAsync(int cpuCoolerId)
    {
        return await _context.CpuCooler.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cpuCoolerId);
    }

    async Task<GpuEntity?> ICompatibilityCheckRepository.GetGpuByIdAsync(int gpuId)
    {
        return await _context.Gpu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == gpuId);
    }

    async Task<HardDriveEntity?> ICompatibilityCheckRepository.GetHardDriveByIdAsync(int hardDriveId)
    {
        return await _context.HardDrive.AsNoTracking().FirstOrDefaultAsync(c => c.Id == hardDriveId);
    }

    async Task<PcMonitorEntity?> ICompatibilityCheckRepository.GetMonitorByIdAsync(int monitorId)
    {
        return await _context.PcMonitor.AsNoTracking().FirstOrDefaultAsync(c => c.Id == monitorId);
    }

    async Task<MotherboardEntity?> ICompatibilityCheckRepository.GetMotherboardByIdAsync(int motherboardId)
    {
        return await _context.Motherboard.AsNoTracking().FirstOrDefaultAsync(c => c.Id == motherboardId);
    }

    async Task<PsuEntity?> ICompatibilityCheckRepository.GetPsuByIdAsync(int psuId)
    {
        return await _context.Psu.AsNoTracking().FirstOrDefaultAsync(c => c.Id == psuId);
    }

    async Task<RamEntity?> ICompatibilityCheckRepository.GetRamByIdAsync(int ramId)
    {
        return await _context.Ram.AsNoTracking().FirstOrDefaultAsync(c => c.Id == ramId);
    }
}
