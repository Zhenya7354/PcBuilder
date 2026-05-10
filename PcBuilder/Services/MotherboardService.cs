using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class MotherboardService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<Motherboard>> GetAllMotherboardsAsync()
        {
            return await _context.Motherboard.Include(m => m.brand).ToListAsync();
        }

        public async Task<Motherboard> GetMotherboardByIdAsync(int id)
        {
            var motherboard = await _context.Motherboard.Include(m => m.brand).FirstOrDefaultAsync(m => m.id == id);
            if (motherboard is null)
                throw new ArgumentException($"Motherboard with ID {id} not found.");

            return motherboard;
        }

        public async Task<Motherboard> AddMotherboardAsync(MotherboardCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var motherboard = new Motherboard
            {
                name = dto.Name,
                brandId = dto.BrandId,
                socket = dto.Socket,
                chipset = dto.Chipset,
                formFactor = dto.FormFactor,
                memoryType = dto.MemoryType,
                memorySlots = dto.MemorySlots,
                maxMemoryGb = dto.MaxMemoryGb,
                maxMemorySpeedMhz = dto.MaxMemorySpeedMhz,
                pcieX16Slots = dto.PcieX16Slots,
                pcieX1Slots = dto.PcieX1Slots,
                m2Slots = dto.M2Slots,
                sataPorts = dto.SataPorts,
                usbHeaders3Gen2 = dto.UsbHeaders3Gen2,
                usbHeaders2Gen0 = dto.UsbHeaders2Gen0,
                hasWifi = dto.HasWifi,
                hasBluetooth = dto.HasBluetooth,
                lanSpeedGbps = dto.LanSpeedGbps,
                fanHeaders = dto.FanHeaders,
                argbHeaders = dto.ArgbHeaders,
                vrmPhases = dto.VrmPhases,
                rearUsbA = dto.RearUsbA,
                rearUsbC = dto.RearUsbC,
                rearHdmi = dto.RearHdmi,
                rearDisplayPort = dto.RearDisplayPort,
                priceUsd = dto.PriceUsd
            };

            _context.Motherboard.Add(motherboard);
            await _context.SaveChangesAsync();
            return motherboard;
        }

        public async Task<Motherboard> UpdateMotherboardAsync(int id, MotherboardUpdateDto dto)
        {
            var motherboard = await _context.Motherboard.FindAsync(id);
            if (motherboard is null)
                throw new ArgumentException($"Motherboard with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                motherboard.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) motherboard.name = dto.Name;
            if (dto.Socket.HasValue) motherboard.socket = dto.Socket.Value;
            if (!string.IsNullOrWhiteSpace(dto.Chipset)) motherboard.chipset = dto.Chipset;
            if (dto.FormFactor.HasValue) motherboard.formFactor = dto.FormFactor.Value;
            if (dto.MemoryType.HasValue) motherboard.memoryType = dto.MemoryType.Value;
            if (dto.MemorySlots.HasValue) motherboard.memorySlots = dto.MemorySlots.Value;
            if (dto.MaxMemoryGb.HasValue) motherboard.maxMemoryGb = dto.MaxMemoryGb.Value;
            if (dto.MaxMemorySpeedMhz.HasValue) motherboard.maxMemorySpeedMhz = dto.MaxMemorySpeedMhz.Value;
            if (dto.PcieX16Slots.HasValue) motherboard.pcieX16Slots = dto.PcieX16Slots.Value;
            if (dto.PcieX1Slots.HasValue) motherboard.pcieX1Slots = dto.PcieX1Slots.Value;
            if (dto.M2Slots.HasValue) motherboard.m2Slots = dto.M2Slots.Value;
            if (dto.SataPorts.HasValue) motherboard.sataPorts = dto.SataPorts.Value;
            if (dto.UsbHeaders3Gen2.HasValue) motherboard.usbHeaders3Gen2 = dto.UsbHeaders3Gen2.Value;
            if (dto.UsbHeaders2Gen0.HasValue) motherboard.usbHeaders2Gen0 = dto.UsbHeaders2Gen0.Value;
            if (dto.HasWifi.HasValue) motherboard.hasWifi = dto.HasWifi.Value;
            if (dto.HasBluetooth.HasValue) motherboard.hasBluetooth = dto.HasBluetooth.Value;
            if (dto.LanSpeedGbps.HasValue) motherboard.lanSpeedGbps = dto.LanSpeedGbps.Value;
            if (dto.FanHeaders.HasValue) motherboard.fanHeaders = dto.FanHeaders.Value;
            if (dto.ArgbHeaders.HasValue) motherboard.argbHeaders = dto.ArgbHeaders.Value;
            if (dto.VrmPhases.HasValue) motherboard.vrmPhases = dto.VrmPhases.Value;
            if (dto.RearUsbA.HasValue) motherboard.rearUsbA = dto.RearUsbA.Value;
            if (dto.RearUsbC.HasValue) motherboard.rearUsbC = dto.RearUsbC.Value;
            if (dto.RearHdmi.HasValue) motherboard.rearHdmi = dto.RearHdmi.Value;
            if (dto.RearDisplayPort.HasValue) motherboard.rearDisplayPort = dto.RearDisplayPort.Value;
            if (dto.PriceUsd.HasValue) motherboard.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return motherboard;
        }

        public async Task DeleteMotherboardAsync(int id)
        {
            var motherboard = await _context.Motherboard.FindAsync(id);
            if (motherboard is null)
                throw new ArgumentException($"Motherboard with ID {id} not found.");

            _context.Motherboard.Remove(motherboard);
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
