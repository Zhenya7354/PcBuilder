using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class PcCaseService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<PcCase>> GetAllCasesAsync()
        {
            return await _context.PcCase.Include(c => c.brand).ToListAsync();
        }

        public async Task<PcCase> GetCaseByIdAsync(int id)
        {
            var pcCase = await _context.PcCase.Include(c => c.brand).FirstOrDefaultAsync(c => c.id == id);
            if (pcCase is null)
                throw new ArgumentException($"Case with ID {id} not found.");

            return pcCase;
        }

        public async Task<PcCase> AddCaseAsync(PcCaseCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var pcCase = new PcCase
            {
                name = dto.Name,
                brandId = dto.BrandId,
                supportedFormFactors = dto.SupportedFormFactors,
                maxGpuLengthMm = dto.MaxGpuLengthMm,
                maxCpuCoolerHeightMm = dto.MaxCpuCoolerHeightMm,
                maxPsuLengthMm = dto.MaxPsuLengthMm,
                driveBays35Inch = dto.DriveBays35Inch,
                driveBays25Inch = dto.DriveBays25Inch,
                frontUsbA = dto.FrontUsbA,
                frontUsbC = dto.FrontUsbC,
                radiatorSupportMm = dto.RadiatorSupportMm,
                caseWidthMm = dto.CaseWidthMm,
                caseHeightMm = dto.CaseHeightMm,
                caseDepthMm = dto.CaseDepthMm,
                hasGlassPanel = dto.HasGlassPanel,
                includedFans = dto.IncludedFans,
                priceUsd = dto.PriceUsd
            };

            _context.PcCase.Add(pcCase);
            await _context.SaveChangesAsync();
            return pcCase;
        }

        public async Task<PcCase> UpdateCaseAsync(int id, PcCaseUpdateDto dto)
        {
            var pcCase = await _context.PcCase.FindAsync(id);
            if (pcCase is null)
                throw new ArgumentException($"Case with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                pcCase.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) pcCase.name = dto.Name;
            if (dto.SupportedFormFactors is { Count: > 0 }) pcCase.supportedFormFactors = dto.SupportedFormFactors;
            if (dto.MaxGpuLengthMm.HasValue) pcCase.maxGpuLengthMm = dto.MaxGpuLengthMm.Value;
            if (dto.MaxCpuCoolerHeightMm.HasValue) pcCase.maxCpuCoolerHeightMm = dto.MaxCpuCoolerHeightMm.Value;
            if (dto.MaxPsuLengthMm.HasValue) pcCase.maxPsuLengthMm = dto.MaxPsuLengthMm.Value;
            if (dto.DriveBays35Inch.HasValue) pcCase.driveBays35Inch = dto.DriveBays35Inch.Value;
            if (dto.DriveBays25Inch.HasValue) pcCase.driveBays25Inch = dto.DriveBays25Inch.Value;
            if (dto.FrontUsbA.HasValue) pcCase.frontUsbA = dto.FrontUsbA.Value;
            if (dto.FrontUsbC.HasValue) pcCase.frontUsbC = dto.FrontUsbC.Value;
            if (dto.RadiatorSupportMm is { Count: > 0 }) pcCase.radiatorSupportMm = dto.RadiatorSupportMm;
            if (dto.CaseWidthMm.HasValue) pcCase.caseWidthMm = dto.CaseWidthMm.Value;
            if (dto.CaseHeightMm.HasValue) pcCase.caseHeightMm = dto.CaseHeightMm.Value;
            if (dto.CaseDepthMm.HasValue) pcCase.caseDepthMm = dto.CaseDepthMm.Value;
            if (dto.HasGlassPanel.HasValue) pcCase.hasGlassPanel = dto.HasGlassPanel.Value;
            if (dto.IncludedFans.HasValue) pcCase.includedFans = dto.IncludedFans.Value;
            if (dto.PriceUsd.HasValue) pcCase.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return pcCase;
        }

        public async Task DeleteCaseAsync(int id)
        {
            var pcCase = await _context.PcCase.FindAsync(id);
            if (pcCase is null)
                throw new ArgumentException($"Case with ID {id} not found.");

            _context.PcCase.Remove(pcCase);
            await _context.SaveChangesAsync();
        }

        private async Task EnsureBrandExistsAsync(int brandId)
        {
            var brandExists = await _context.Brand.AnyAsync(b => b.id == brandId);
            if (!brandExists)
            {
                throw new ArgumentException("Brand with the specified ID does not exist.");
            }
        }
    }
}
