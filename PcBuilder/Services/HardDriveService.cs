using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class HardDriveService(IHardDriveRepository hardDriveRepository) : IHardDriveService
{
    private readonly IHardDriveRepository _hardDriveRepository = hardDriveRepository;

    public async Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken)
    {
        return await _hardDriveRepository.GetAllHardDrivesAsync(cancellationToken);
    }

    public async Task<HardDriveEntity> GetHardDriveByIdAsync(int id, CancellationToken cancellationToken)
    {
        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");

        return hardDrive;
    }

    public async Task<HardDriveEntity> AddHardDriveAsync(HardDriveCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Hard drive data is required.");

        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

        var hardDrive = new HardDriveEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            CapacityGb = dto.CapacityGb,
            DriveInterface = dto.DriveInterface,
            FormFactor = dto.FormFactor,
            PcDriveType = dto.PcDriveType,
            ReadSpeedMbS = dto.ReadSpeedMbS,
            WriteSpeedMbs = dto.WriteSpeedMbs,
            Rpm = dto.Rpm,
            CacheMb = dto.CacheMb,
            Tbw = dto.Tbw,
            PowerWatts = dto.PowerWatts,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _hardDriveRepository.AddHardDriveAsync(hardDrive, cancellationToken);
        await _hardDriveRepository.SaveChangesAsync(cancellationToken);
        return hardDrive;
    }

    public async Task<HardDriveEntity> UpdateHardDriveAsync(int id, HardDriveUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Hard drive data is required.");

        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");

       await EnsureBrandExistsAsync(dto.BrandId ?? hardDrive.BrandId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dto.Name)) hardDrive.Name = dto.Name;
        if (dto.CapacityGb.HasValue) hardDrive.CapacityGb = dto.CapacityGb.Value;
        if (dto.DriveInterface.HasValue) hardDrive.DriveInterface = dto.DriveInterface.Value;
        if (dto.FormFactor.HasValue) hardDrive.FormFactor = dto.FormFactor.Value;
        if (dto.PcDriveType.HasValue) hardDrive.PcDriveType = dto.PcDriveType.Value;
        if (dto.ReadSpeedMbS.HasValue) hardDrive.ReadSpeedMbS = dto.ReadSpeedMbS.Value;
        if (dto.WriteSpeedMbs.HasValue) hardDrive.WriteSpeedMbs = dto.WriteSpeedMbs.Value;
        if (dto.Rpm.HasValue) hardDrive.Rpm = dto.Rpm.Value;
        if (dto.CacheMb.HasValue) hardDrive.CacheMb = dto.CacheMb.Value;
        if (dto.Tbw.HasValue) hardDrive.Tbw = dto.Tbw.Value;
        if (dto.PowerWatts.HasValue) hardDrive.PowerWatts = dto.PowerWatts.Value;
        if(dto.Currency.HasValue) hardDrive.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) hardDrive.Price = dto.Price.Value;

        await _hardDriveRepository.SaveChangesAsync(cancellationToken);
        return hardDrive;
    }

    public async Task DeleteHardDriveAsync(int id, CancellationToken cancellationToken)
    {
        var hardDrive = await _hardDriveRepository.GetHardDriveByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Hard drive with ID {id} not found.");
        await _hardDriveRepository.DeleteHardDriveAsync(hardDrive, cancellationToken);
        await _hardDriveRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _hardDriveRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
