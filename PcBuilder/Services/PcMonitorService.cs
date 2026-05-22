using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class PcMonitorService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<PcMonitorEntity>> GetAllMonitorsAsync()
    {
        return await _context.PcMonitor.Include(m => m.Brand).ToListAsync();
    }

    public async Task<PcMonitorEntity> GetMonitorByIdAsync(int id)
    {
        var monitor = await _context.PcMonitor.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
        if (monitor is null)
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        return monitor;
    }

    public async Task<PcMonitorEntity> AddMonitorAsync(PcMonitorCreateDto dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var monitor = new PcMonitorEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            ScreenSizeInch = dto.ScreenSizeInch,
            ResolutionWidth = dto.ResolutionWidth,
            ResolutionHeight = dto.ResolutionHeight,
            PanelType = dto.PanelType,
            RefreshRateHz = dto.RefreshRateHz,
            ResponseTimeMs = dto.ResponseTimeMs,
            HdrSupport = dto.HdrSupport,
            BrightnessNits = dto.BrightnessNits,
            ContrastRatio = dto.ContrastRatio,
            ColorGamutP3 = dto.ColorGamutP3,
            HasGSync = dto.HasGSync,
            HasFreeSync = dto.HasFreeSync,
            HasFreeSyncPremium = dto.HasFreeSyncPremium,
            HdmiPorts = dto.HdmiPorts,
            HdmiVersion = dto.HdmiVersion,
            DpPorts = dto.DpPorts,
            DpVersion = dto.DpVersion,
            UsbCPorts = dto.UsbCPorts,
            HasUsbHub = dto.HasUsbHub,
            HasSpeakers = dto.HasSpeakers,
            HeightAdjustable = dto.HeightAdjustable,
            VesaMount = dto.VesaMount,
            PriceUsd = dto.PriceUsd
        };

        _context.PcMonitor.Add(monitor);
        await _context.SaveChangesAsync();
        return monitor;
    }

    public async Task<PcMonitorEntity> UpdateMonitorAsync(int id, PcMonitorUpdateDto dto)
    {
        var monitor = await _context.PcMonitor.FindAsync(id);
        if (monitor is null)
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        if (dto.BrandId.HasValue)
        {
            await EnsureBrandExistsAsync(dto.BrandId.Value);
            monitor.BrandId = dto.BrandId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Name)) monitor.Name = dto.Name;
        if (dto.ScreenSizeInch.HasValue) monitor.ScreenSizeInch = dto.ScreenSizeInch.Value;
        if (dto.ResolutionWidth.HasValue) monitor.ResolutionWidth = dto.ResolutionWidth.Value;
        if (dto.ResolutionHeight.HasValue) monitor.ResolutionHeight = dto.ResolutionHeight.Value;
        if (dto.PanelType.HasValue) monitor.PanelType = dto.PanelType.Value;
        if (dto.RefreshRateHz.HasValue) monitor.RefreshRateHz = dto.RefreshRateHz.Value;
        if (dto.ResponseTimeMs.HasValue) monitor.ResponseTimeMs = dto.ResponseTimeMs.Value;
        if (!string.IsNullOrWhiteSpace(dto.HdrSupport)) monitor.HdrSupport = dto.HdrSupport;
        if (dto.BrightnessNits.HasValue) monitor.BrightnessNits = dto.BrightnessNits.Value;
        if (!string.IsNullOrWhiteSpace(dto.ContrastRatio)) monitor.ContrastRatio = dto.ContrastRatio;
        if (dto.ColorGamutP3.HasValue) monitor.ColorGamutP3 = dto.ColorGamutP3.Value;
        if (dto.HasGSync.HasValue) monitor.HasGSync = dto.HasGSync.Value;
        if (dto.HasFreeSync.HasValue) monitor.HasFreeSync = dto.HasFreeSync.Value;
        if (dto.HasFreeSyncPremium.HasValue) monitor.HasFreeSyncPremium = dto.HasFreeSyncPremium.Value;
        if (dto.HdmiPorts.HasValue) monitor.HdmiPorts = dto.HdmiPorts.Value;
        if (!string.IsNullOrWhiteSpace(dto.HdmiVersion)) monitor.HdmiVersion = dto.HdmiVersion;
        if (dto.DpPorts.HasValue) monitor.DpPorts = dto.DpPorts.Value;
        if (!string.IsNullOrWhiteSpace(dto.DpVersion)) monitor.DpVersion = dto.DpVersion;
        if (dto.UsbCPorts.HasValue) monitor.UsbCPorts = dto.UsbCPorts.Value;
        if (dto.HasUsbHub.HasValue) monitor.HasUsbHub = dto.HasUsbHub.Value;
        if (dto.HasSpeakers.HasValue) monitor.HasSpeakers = dto.HasSpeakers.Value;
        if (dto.HeightAdjustable.HasValue) monitor.HeightAdjustable = dto.HeightAdjustable.Value;
        if (!string.IsNullOrWhiteSpace(dto.VesaMount)) monitor.VesaMount = dto.VesaMount;
        if (dto.PriceUsd.HasValue) monitor.PriceUsd = dto.PriceUsd.Value;

        await _context.SaveChangesAsync();
        return monitor;
    }

    public async Task DeleteMonitorAsync(int id)
    {
        var monitor = await _context.PcMonitor.FindAsync(id);
        if (monitor is null)
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        _context.PcMonitor.Remove(monitor);
        await _context.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        var brandExists = await _context.Brand.AnyAsync(b => b.Id == brandId);
        if (!brandExists)
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
