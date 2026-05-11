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
            return await _context.HardDrive.Include(h => h.Brand).ToListAsync();
        }

        public async Task<HardDrive> GetHardDriveByIdAsync(int id)
        {
            var hardDrive = await _context.HardDrive.Include(h => h.Brand).FirstOrDefaultAsync(h => h.Id == id);
            if (hardDrive is null)
                throw new ArgumentException($"Hard drive with ID {id} not found.");

            return hardDrive;
        }

        public async Task<HardDrive> AddHardDriveAsync(HardDriveCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var hardDrive = new HardDrive
            {
                Name = dto.Name,
                BrandId = dto.BrandId,
                CapacityGb = dto.CapacityGb,
                DriveInterface = dto.DriveInterface,
                FormFactor = dto.FormFactor,
                IsSsd = dto.IsSsd,
                ReadSpeedMbS = dto.ReadSpeedMbS,
                WriteSpeedMbs = dto.WriteSpeedMbs,
                Rpm = dto.Rpm,
                CacheMb = dto.CacheMb,
                Tbw = dto.Tbw,
                PowerWatts = dto.PowerWatts,
                PriceUsd = dto.PriceUsd
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
                hardDrive.BrandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) hardDrive.Name = dto.Name;
            if (dto.CapacityGb.HasValue) hardDrive.CapacityGb = dto.CapacityGb.Value;
            if (dto.DriveInterface.HasValue) hardDrive.DriveInterface = dto.DriveInterface.Value;
            if (dto.FormFactor.HasValue) hardDrive.FormFactor = dto.FormFactor.Value;
            if (dto.IsSsd.HasValue) hardDrive.IsSsd = dto.IsSsd.Value;
            if (dto.ReadSpeedMbS.HasValue) hardDrive.ReadSpeedMbS = dto.ReadSpeedMbS.Value;
            if (dto.WriteSpeedMbs.HasValue) hardDrive.WriteSpeedMbs = dto.WriteSpeedMbs.Value;
            if (dto.Rpm.HasValue) hardDrive.Rpm = dto.Rpm.Value;
            if (dto.CacheMb.HasValue) hardDrive.CacheMb = dto.CacheMb.Value;
            if (dto.Tbw.HasValue) hardDrive.Tbw = dto.Tbw.Value;
            if (dto.PowerWatts.HasValue) hardDrive.PowerWatts = dto.PowerWatts.Value;
            if (dto.PriceUsd.HasValue) hardDrive.PriceUsd = dto.PriceUsd.Value;

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
            var brandExists = await _context.Brand.AnyAsync(b => b.Id == brandId);
            if (!brandExists)
            {
                throw new ArgumentException("Brand with the specified ID does not exist.");
            }
        }
    }
}
