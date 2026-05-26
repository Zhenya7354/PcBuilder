using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Services;

public class MotherboardService(IMotherboardRepository motherboardRepository)
{
    private readonly IMotherboardRepository _motherboardRepository = motherboardRepository;

    public async Task<List<MotherboardEntity>> GetAllMotherboardsAsync()
    {
        return await _motherboardRepository.GetAllMotherboardsAsync();
    }

    public async Task<MotherboardEntity> GetMotherboardByIdAsync(int id)
    {
        var motherboard = await _motherboardRepository.GetMotherboardByIdAsync(id) ??
            throw new KeyNotFoundException($"Motherboard with ID {id} not found.");

        return motherboard;
    }

    public async Task<MotherboardEntity> AddMotherboardAsync(MotherboardCreate dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var motherboard = new MotherboardEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            Socket = dto.Socket,
            Chipset = dto.Chipset,
            FormFactor = dto.FormFactor,
            MemoryType = dto.MemoryType,
            MemorySlots = dto.MemorySlots,
            MaxMemoryGb = dto.MaxMemoryGb,
            MaxMemorySpeedMhz = dto.MaxMemorySpeedMhz,
            PcieX16Slots = dto.PcieX16Slots,
            PcieX1Slots = dto.PcieX1Slots,
            M2Slots = dto.M2Slots,
            SataPorts = dto.SataPorts,
            UsbHeaders3Gen2 = dto.UsbHeaders3Gen2,
            UsbHeaders2Gen0 = dto.UsbHeaders2Gen0,
            HasWifi = dto.HasWifi,
            HasBluetooth = dto.HasBluetooth,
            LanSpeedGbps = dto.LanSpeedGbps,
            FanHeaders = dto.FanHeaders,
            ArgbHeaders = dto.ArgbHeaders,
            VrmPhases = dto.VrmPhases,
            RearUsbA = dto.RearUsbA,
            RearUsbC = dto.RearUsbC,
            RearHdmi = dto.RearHdmi,
            RearDisplayPort = dto.RearDisplayPort,
            PriceUsd = dto.PriceUsd
        };

        await _motherboardRepository.AddMotherboardAsync(motherboard);
        await _motherboardRepository.SaveChangesAsync();
        return motherboard;
    }

    public async Task<MotherboardEntity> UpdateMotherboardAsync(int id, MotherboardUpdate dto)
    {
        var motherboard = await _motherboardRepository.GetMotherboardByIdAsync(id) ??
            throw new KeyNotFoundException($"Motherboard with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? motherboard.BrandId);

        if (!string.IsNullOrWhiteSpace(dto.Name)) motherboard.Name = dto.Name;
        if (dto.Socket.HasValue) motherboard.Socket = dto.Socket.Value;
        if (!string.IsNullOrWhiteSpace(dto.Chipset)) motherboard.Chipset = dto.Chipset;
        if (dto.FormFactor.HasValue) motherboard.FormFactor = dto.FormFactor.Value;
        if (dto.MemoryType.HasValue) motherboard.MemoryType = dto.MemoryType.Value;
        if (dto.MemorySlots.HasValue) motherboard.MemorySlots = dto.MemorySlots.Value;
        if (dto.MaxMemoryGb.HasValue) motherboard.MaxMemoryGb = dto.MaxMemoryGb.Value;
        if (dto.MaxMemorySpeedMhz.HasValue) motherboard.MaxMemorySpeedMhz = dto.MaxMemorySpeedMhz.Value;
        if (dto.PcieX16Slots.HasValue) motherboard.PcieX16Slots = dto.PcieX16Slots.Value;
        if (dto.PcieX1Slots.HasValue) motherboard.PcieX1Slots = dto.PcieX1Slots.Value;
        if (dto.M2Slots.HasValue) motherboard.M2Slots = dto.M2Slots.Value;
        if (dto.SataPorts.HasValue) motherboard.SataPorts = dto.SataPorts.Value;
        if (dto.UsbHeaders3Gen2.HasValue) motherboard.UsbHeaders3Gen2 = dto.UsbHeaders3Gen2.Value;
        if (dto.UsbHeaders2Gen0.HasValue) motherboard.UsbHeaders2Gen0 = dto.UsbHeaders2Gen0.Value;
        if (dto.HasWifi.HasValue) motherboard.HasWifi = dto.HasWifi.Value;
        if (dto.HasBluetooth.HasValue) motherboard.HasBluetooth = dto.HasBluetooth.Value;
        if (dto.LanSpeedGbps.HasValue) motherboard.LanSpeedGbps = dto.LanSpeedGbps.Value;
        if (dto.FanHeaders.HasValue) motherboard.FanHeaders = dto.FanHeaders.Value;
        if (dto.ArgbHeaders.HasValue) motherboard.ArgbHeaders = dto.ArgbHeaders.Value;
        if (dto.VrmPhases.HasValue) motherboard.VrmPhases = dto.VrmPhases.Value;
        if (dto.RearUsbA.HasValue) motherboard.RearUsbA = dto.RearUsbA.Value;
        if (dto.RearUsbC.HasValue) motherboard.RearUsbC = dto.RearUsbC.Value;
        if (dto.RearHdmi.HasValue) motherboard.RearHdmi = dto.RearHdmi.Value;
        if (dto.RearDisplayPort.HasValue) motherboard.RearDisplayPort = dto.RearDisplayPort.Value;
        if (dto.PriceUsd.HasValue) motherboard.PriceUsd = dto.PriceUsd.Value;

        await _motherboardRepository.SaveChangesAsync();
        return motherboard;
    }

    public async Task DeleteMotherboardAsync(int id)
    {
        var motherboard = await _motherboardRepository.GetMotherboardByIdAsync(id) ??
            throw new KeyNotFoundException($"Motherboard with ID {id} not found.");

        await _motherboardRepository.DeleteMotherboardAsync(motherboard);
        await _motherboardRepository.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        if (!await _motherboardRepository.BrandExistsAsync(brandId))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
