using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class PcMonitorService(IPcMonitorRepository pcMonitorRepository) : IPcMonitorService
{
    private readonly IPcMonitorRepository _pcMonitorRepository = pcMonitorRepository;

    public async Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken)
    {
        return await _pcMonitorRepository.GetAllMonitorsAsync(cancellationToken);
    }

    public async Task<PcMonitorEntity> GetMonitorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var monitor = await _pcMonitorRepository.GetMonitorByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        return monitor;
    }

    public async Task<PcMonitorEntity> AddMonitorAsync(PcMonitorCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Monitor data is required.");
        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

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
            SyncTechnologies = dto.SyncTechnologies,
            HdmiPorts = dto.HdmiPorts,
            HdmiVersion = dto.HdmiVersion,
            DpPorts = dto.DpPorts,
            DpVersion = dto.DpVersion,
            UsbCPorts = dto.UsbCPorts,
            HasUsbHub = dto.HasUsbHub,
            HasSpeakers = dto.HasSpeakers,
            HeightAdjustable = dto.HeightAdjustable,
            VesaMount = dto.VesaMount,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _pcMonitorRepository.AddMonitorAsync(monitor, cancellationToken);
        await _pcMonitorRepository.SaveChangesAsync(cancellationToken);
        return monitor;
    }

    public async Task<PcMonitorEntity> UpdateMonitorAsync(int id, PcMonitorUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Monitor data is required.");

        var monitor = await _pcMonitorRepository.GetMonitorByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? monitor.BrandId, cancellationToken);

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
        if (dto.SyncTechnologies != null) monitor.SyncTechnologies = dto.SyncTechnologies;
        if (dto.HdmiPorts.HasValue) monitor.HdmiPorts = dto.HdmiPorts.Value;
        if (!string.IsNullOrWhiteSpace(dto.HdmiVersion)) monitor.HdmiVersion = dto.HdmiVersion;
        if (dto.DpPorts.HasValue) monitor.DpPorts = dto.DpPorts.Value;
        if (!string.IsNullOrWhiteSpace(dto.DpVersion)) monitor.DpVersion = dto.DpVersion;
        if (dto.UsbCPorts.HasValue) monitor.UsbCPorts = dto.UsbCPorts.Value;
        if (dto.HasUsbHub.HasValue) monitor.HasUsbHub = dto.HasUsbHub.Value;
        if (dto.HasSpeakers.HasValue) monitor.HasSpeakers = dto.HasSpeakers.Value;
        if (dto.HeightAdjustable.HasValue) monitor.HeightAdjustable = dto.HeightAdjustable.Value;
        if (!string.IsNullOrWhiteSpace(dto.VesaMount)) monitor.VesaMount = dto.VesaMount;
        if(dto.Currency.HasValue) monitor.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) monitor.Price = dto.Price.Value;

        await _pcMonitorRepository.SaveChangesAsync(cancellationToken);
        return monitor;
    }

    public async Task DeleteMonitorAsync(int id, CancellationToken cancellationToken)
    {
        var monitor = await _pcMonitorRepository.GetMonitorByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Monitor with ID {id} not found.");

        await _pcMonitorRepository.DeleteMonitorAsync(monitor, cancellationToken);
        await _pcMonitorRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _pcMonitorRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
