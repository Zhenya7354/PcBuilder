using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class HardDriveService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<HardDrive>> GetAllHardDrivesAsync()
        {
            return await _context.HardDrive.Include(h => h.brand).ToListAsync();
        }

        public async Task<HardDrive> GetHardDriveByIdAsync(int id)
        {
            var hardDrive = await _context.HardDrive.Include(h => h.brand).FirstOrDefaultAsync(h => h.id == id);
            if (hardDrive is null)
                throw new ArgumentException($"Hard drive with ID {id} not found.");

            return hardDrive;
        }

        public async Task<HardDrive> AddHardDriveAsync(HardDriveCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var hardDrive = new HardDrive
            {
                name = dto.Name,
                brandId = dto.BrandId,
                capacityGb = dto.CapacityGb,
                driveInterface = dto.DriveInterface,
                formFactor = dto.FormFactor,
                isSsd = dto.IsSsd,
                readSpeedMbS = dto.ReadSpeedMbS,
                writeSpeedMbs = dto.WriteSpeedMbs,
                rpm = dto.Rpm,
                cacheMb = dto.CacheMb,
                tbw = dto.Tbw,
                powerWatts = dto.PowerWatts,
                priceUsd = dto.PriceUsd
            };

            _context.HardDrive.Add(hardDrive);
            await _context.SaveChangesAsync();
            return hardDrive;
        }

        public async Task<HardDrive> UpdateHardDriveAsync(int id, HardDriveUpdateDto dto)
        {
            var hardDrive = await _context.HardDrive.FindAsync(id);
            if (hardDrive is null)
                throw new ArgumentException($"Hard drive with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                hardDrive.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) hardDrive.name = dto.Name;
            if (dto.CapacityGb.HasValue) hardDrive.capacityGb = dto.CapacityGb.Value;
            if (dto.DriveInterface.HasValue) hardDrive.driveInterface = dto.DriveInterface.Value;
            if (dto.FormFactor.HasValue) hardDrive.formFactor = dto.FormFactor.Value;
            if (dto.IsSsd.HasValue) hardDrive.isSsd = dto.IsSsd.Value;
            if (dto.ReadSpeedMbS.HasValue) hardDrive.readSpeedMbS = dto.ReadSpeedMbS.Value;
            if (dto.WriteSpeedMbs.HasValue) hardDrive.writeSpeedMbs = dto.WriteSpeedMbs.Value;
            if (dto.Rpm.HasValue) hardDrive.rpm = dto.Rpm.Value;
            if (dto.CacheMb.HasValue) hardDrive.cacheMb = dto.CacheMb.Value;
            if (dto.Tbw.HasValue) hardDrive.tbw = dto.Tbw.Value;
            if (dto.PowerWatts.HasValue) hardDrive.powerWatts = dto.PowerWatts.Value;
            if (dto.PriceUsd.HasValue) hardDrive.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return hardDrive;
        }

        public async Task DeleteHardDriveAsync(int id)
        {
            var hardDrive = await _context.HardDrive.FindAsync(id);
            if (hardDrive is null)
                throw new ArgumentException($"Hard drive with ID {id} not found.");

            _context.HardDrive.Remove(hardDrive);
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
