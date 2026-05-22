using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class PcCaseService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcCaseEntity>> GetAllCasesAsync()
    {
        return await _context.PcCase.Include(c => c.Brand).ToListAsync();
    }

    public async Task<PcCaseEntity> GetCaseByIdAsync(int id)
    {
        var pcCase = await _context.PcCase.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
        if (pcCase is null)
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        return pcCase;
    }

    public async Task<PcCaseEntity> AddCaseAsync(PcCaseCreate dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var pcCase = new PcCaseEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            SupportedFormFactors = dto.SupportedFormFactors,
            MaxGpuLengthMm = dto.MaxGpuLengthMm,
            MaxCpuCoolerHeightMm = dto.MaxCpuCoolerHeightMm,
            MaxPsuLengthMm = dto.MaxPsuLengthMm,
            DriveBays35Inch = dto.DriveBays35Inch,
            DriveBays25Inch = dto.DriveBays25Inch,
            FrontUsbA = dto.FrontUsbA,
            FrontUsbC = dto.FrontUsbC,
            RadiatorSupportMm = dto.RadiatorSupportMm,
            CaseWidthMm = dto.CaseWidthMm,
            CaseHeightMm = dto.CaseHeightMm,
            CaseDepthMm = dto.CaseDepthMm,
            HasGlassPanel = dto.HasGlassPanel,
            IncludedFans = dto.IncludedFans,
            PriceUsd = dto.PriceUsd
        };

        _context.PcCase.Add(pcCase);
        await _context.SaveChangesAsync();
        return pcCase;
    }

    public async Task<PcCaseEntity> UpdateCaseAsync(int id, PcCaseUpdate dto)
    {
        var pcCase = await _context.PcCase.FindAsync(id);
        if (pcCase is null)
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        if (dto.BrandId.HasValue)
        {
            await EnsureBrandExistsAsync(dto.BrandId.Value);
            pcCase.BrandId = dto.BrandId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Name)) pcCase.Name = dto.Name;
        if (dto.SupportedFormFactors is { Count: > 0 }) pcCase.SupportedFormFactors = dto.SupportedFormFactors;
        if (dto.MaxGpuLengthMm.HasValue) pcCase.MaxGpuLengthMm = dto.MaxGpuLengthMm.Value;
        if (dto.MaxCpuCoolerHeightMm.HasValue) pcCase.MaxCpuCoolerHeightMm = dto.MaxCpuCoolerHeightMm.Value;
        if (dto.MaxPsuLengthMm.HasValue) pcCase.MaxPsuLengthMm = dto.MaxPsuLengthMm.Value;
        if (dto.DriveBays35Inch.HasValue) pcCase.DriveBays35Inch = dto.DriveBays35Inch.Value;
        if (dto.DriveBays25Inch.HasValue) pcCase.DriveBays25Inch = dto.DriveBays25Inch.Value;
        if (dto.FrontUsbA.HasValue) pcCase.FrontUsbA = dto.FrontUsbA.Value;
        if (dto.FrontUsbC.HasValue) pcCase.FrontUsbC = dto.FrontUsbC.Value;
        if (dto.RadiatorSupportMm is { Count: > 0 }) pcCase.RadiatorSupportMm = dto.RadiatorSupportMm;
        if (dto.CaseWidthMm.HasValue) pcCase.CaseWidthMm = dto.CaseWidthMm.Value;
        if (dto.CaseHeightMm.HasValue) pcCase.CaseHeightMm = dto.CaseHeightMm.Value;
        if (dto.CaseDepthMm.HasValue) pcCase.CaseDepthMm = dto.CaseDepthMm.Value;
        if (dto.HasGlassPanel.HasValue) pcCase.HasGlassPanel = dto.HasGlassPanel.Value;
        if (dto.IncludedFans.HasValue) pcCase.IncludedFans = dto.IncludedFans.Value;
        if (dto.PriceUsd.HasValue) pcCase.PriceUsd = dto.PriceUsd.Value;

        await _context.SaveChangesAsync();
        return pcCase;
    }

    public async Task DeleteCaseAsync(int id)
    {
        var pcCase = await _context.PcCase.FindAsync(id);
        if (pcCase is null)
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        _context.PcCase.Remove(pcCase);
        await _context.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        var brandExists = await _context.Brand.AnyAsync(b => b.Id == brandId);
        if (!brandExists)
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
