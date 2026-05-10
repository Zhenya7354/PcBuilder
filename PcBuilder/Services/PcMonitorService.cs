using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class PcMonitorService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<PcMonitor>> GetAllMonitorsAsync()
        {
            return await _context.PcMonitor.Include(m => m.brand).ToListAsync();
        }

        public async Task<PcMonitor> GetMonitorByIdAsync(int id)
        {
            var monitor = await _context.PcMonitor.Include(m => m.brand).FirstOrDefaultAsync(m => m.id == id);
            if (monitor is null)
                throw new ArgumentException($"Monitor with ID {id} not found.");

            return monitor;
        }

        public async Task<PcMonitor> AddMonitorAsync(PcMonitorCreateDto dto)
        {
            await EnsureBrandExistsAsync(dto.BrandId);

            var monitor = new PcMonitor
            {
                name = dto.Name,
                brandId = dto.BrandId,
                screenSizeInch = dto.ScreenSizeInch,
                resolutionWidth = dto.ResolutionWidth,
                resolutionHeight = dto.ResolutionHeight,
                panelType = dto.PanelType,
                refreshRateHz = dto.RefreshRateHz,
                responseTimeMs = dto.ResponseTimeMs,
                hdrSupport = dto.HdrSupport,
                brightnessNits = dto.BrightnessNits,
                contrastRatio = dto.ContrastRatio,
                colorGamutP3 = dto.ColorGamutP3,
                hasGSync = dto.HasGSync,
                hasFreeSync = dto.HasFreeSync,
                hasFreeSyncPremium = dto.HasFreeSyncPremium,
                hdmiPorts = dto.HdmiPorts,
                hdmiVersion = dto.HdmiVersion,
                dpPorts = dto.DpPorts,
                dpVersion = dto.DpVersion,
                usbCPorts = dto.UsbCPorts,
                hasUsbHub = dto.HasUsbHub,
                hasSpeakers = dto.HasSpeakers,
                heightAdjustable = dto.HeightAdjustable,
                vesaMount = dto.VesaMount,
                priceUsd = dto.PriceUsd
            };

            _context.PcMonitor.Add(monitor);
            await _context.SaveChangesAsync();
            return monitor;
        }

        public async Task<PcMonitor> UpdateMonitorAsync(int id, PcMonitorUpdateDto dto)
        {
            var monitor = await _context.PcMonitor.FindAsync(id);
            if (monitor is null)
                throw new ArgumentException($"Monitor with ID {id} not found.");

            if (dto.BrandId.HasValue)
            {
                await EnsureBrandExistsAsync(dto.BrandId.Value);
                monitor.brandId = dto.BrandId.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name)) monitor.name = dto.Name;
            if (dto.ScreenSizeInch.HasValue) monitor.screenSizeInch = dto.ScreenSizeInch.Value;
            if (dto.ResolutionWidth.HasValue) monitor.resolutionWidth = dto.ResolutionWidth.Value;
            if (dto.ResolutionHeight.HasValue) monitor.resolutionHeight = dto.ResolutionHeight.Value;
            if (dto.PanelType.HasValue) monitor.panelType = dto.PanelType.Value;
            if (dto.RefreshRateHz.HasValue) monitor.refreshRateHz = dto.RefreshRateHz.Value;
            if (dto.ResponseTimeMs.HasValue) monitor.responseTimeMs = dto.ResponseTimeMs.Value;
            if (!string.IsNullOrWhiteSpace(dto.HdrSupport)) monitor.hdrSupport = dto.HdrSupport;
            if (dto.BrightnessNits.HasValue) monitor.brightnessNits = dto.BrightnessNits.Value;
            if (!string.IsNullOrWhiteSpace(dto.ContrastRatio)) monitor.contrastRatio = dto.ContrastRatio;
            if (dto.ColorGamutP3.HasValue) monitor.colorGamutP3 = dto.ColorGamutP3.Value;
            if (dto.HasGSync.HasValue) monitor.hasGSync = dto.HasGSync.Value;
            if (dto.HasFreeSync.HasValue) monitor.hasFreeSync = dto.HasFreeSync.Value;
            if (dto.HasFreeSyncPremium.HasValue) monitor.hasFreeSyncPremium = dto.HasFreeSyncPremium.Value;
            if (dto.HdmiPorts.HasValue) monitor.hdmiPorts = dto.HdmiPorts.Value;
            if (!string.IsNullOrWhiteSpace(dto.HdmiVersion)) monitor.hdmiVersion = dto.HdmiVersion;
            if (dto.DpPorts.HasValue) monitor.dpPorts = dto.DpPorts.Value;
            if (!string.IsNullOrWhiteSpace(dto.DpVersion)) monitor.dpVersion = dto.DpVersion;
            if (dto.UsbCPorts.HasValue) monitor.usbCPorts = dto.UsbCPorts.Value;
            if (dto.HasUsbHub.HasValue) monitor.hasUsbHub = dto.HasUsbHub.Value;
            if (dto.HasSpeakers.HasValue) monitor.hasSpeakers = dto.HasSpeakers.Value;
            if (dto.HeightAdjustable.HasValue) monitor.heightAdjustable = dto.HeightAdjustable.Value;
            if (!string.IsNullOrWhiteSpace(dto.VesaMount)) monitor.vesaMount = dto.VesaMount;
            if (dto.PriceUsd.HasValue) monitor.priceUsd = dto.PriceUsd.Value;

            await _context.SaveChangesAsync();
            return monitor;
        }

        public async Task DeleteMonitorAsync(int id)
        {
            var monitor = await _context.PcMonitor.FindAsync(id);
            if (monitor is null)
                throw new ArgumentException($"Monitor with ID {id} not found.");

            _context.PcMonitor.Remove(monitor);
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
