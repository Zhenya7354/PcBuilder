using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Services;

public class PcCaseService(IPcCaseRepository pcCaseRepository)
{
    private readonly IPcCaseRepository _pcCaseRepository = pcCaseRepository;

    public async Task<List<PcCaseEntity>> GetAllCasesAsync()
    {
        return await _pcCaseRepository.GetAllCasesAsync();
    }

    public async Task<PcCaseEntity> GetCaseByIdAsync(int id)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id) ??
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

        await _pcCaseRepository.AddCase(pcCase);
        await _pcCaseRepository.SaveChangesAsync();
        return pcCase;
    }

    public async Task<PcCaseEntity> UpdateCaseAsync(int id, PcCaseUpdate dto)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id) ??
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? pcCase.BrandId);

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

        await _pcCaseRepository.SaveChangesAsync();
        return pcCase;
    }

    public async Task DeleteCaseAsync(int id)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id) ??
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        await _pcCaseRepository.DeleteCase(pcCase);
        await _pcCaseRepository.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        if (!await _pcCaseRepository.BrandExistsAsync(brandId))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
