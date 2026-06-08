using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class PcCaseService(IPcCaseRepository pcCaseRepository) : IPcCaseService
{
    private readonly IPcCaseRepository _pcCaseRepository = pcCaseRepository;

    public async Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken)
    {
        return await _pcCaseRepository.GetAllCasesAsync(cancellationToken);
    }

    public async Task<PcCaseEntity> GetCaseByIdAsync(int id, CancellationToken cancellationToken)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        return pcCase;
    }

    public async Task<PcCaseEntity> AddCaseAsync(PcCaseCreateRequest dto, CancellationToken cancellationToken)
    {
        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

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
            ColorScheme = dto.ColorScheme,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _pcCaseRepository.AddCaseAsync(pcCase, cancellationToken);
        await _pcCaseRepository.SaveChangesAsync(cancellationToken);
        return pcCase;
    }

    public async Task<PcCaseEntity> UpdateCaseAsync(int id, PcCaseUpdateRequest dto, CancellationToken cancellationToken)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? pcCase.BrandId, cancellationToken);

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
        if(dto.ColorScheme.HasValue) pcCase.ColorScheme = dto.ColorScheme.Value;
        if(dto.Currency.HasValue) pcCase.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) pcCase.Price = dto.Price.Value;

        await _pcCaseRepository.SaveChangesAsync(cancellationToken);
        return pcCase;
    }

    public async Task DeleteCaseAsync(int id, CancellationToken cancellationToken)
    {
        var pcCase = await _pcCaseRepository.GetCaseByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Case with ID {id} not found.");

        await _pcCaseRepository.DeleteCaseAsync(pcCase, cancellationToken);
        await _pcCaseRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _pcCaseRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
