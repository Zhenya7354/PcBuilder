using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class HardDriveService(IHardDriveRepository hardDriveRepository) : IHardDriveService
{
    private readonly IHardDriveRepository _hardDriveRepository = hardDriveRepository;

    public async Task<List<HardDriveEntity>> GetAllHardDrivesAsync()
    {
        return await _hardDriveRepository.GetAllHardDrivesAsync();
    }

    public async Task<HardDriveEntity> GetHardDriveByIdAsync(int id)
    {
        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");

        return hardDrive;
    }

    public async Task<HardDriveEntity> AddHardDriveAsync(HardDriveCreateRequest dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var hardDrive = new HardDriveEntity
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

        await _hardDriveRepository.AddHardDriveAsync(hardDrive);
        await _hardDriveRepository.SaveChangesAsync();
        return hardDrive;
    }

    public async Task<HardDriveEntity> UpdateHardDriveAsync(int id, HardDriveUpdateRequest dto)
    {
        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");

       await EnsureBrandExistsAsync(dto.BrandId ?? hardDrive.BrandId);

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

        await _hardDriveRepository.SaveChangesAsync();
        return hardDrive;
    }

    public async Task DeleteHardDriveAsync(int id)
    {
        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");
        await _hardDriveRepository.DeleteHardDriveAsync(hardDrive);
        await _hardDriveRepository.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        if (!await _hardDriveRepository.BrandExistsAsync(brandId))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
