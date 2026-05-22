using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos;

public record MotherboardUpdateDto(
    string? Name,
    int? BrandId,
    PcSocketType? Socket,
    string? Chipset,
    FormFactor? FormFactor,
    MemoryType? MemoryType,
    [Range(1, 16)] int? MemorySlots,
    [Range(1, 2048)] int? MaxMemoryGb,
    [Range(800, 10000)] int? MaxMemorySpeedMhz,
    [Range(0, 10)] int? PcieX16Slots,
    [Range(0, 10)] int? PcieX1Slots,
    [Range(0, 20)] int? M2Slots,
    [Range(0, 20)] int? SataPorts,
    [Range(0, 20)] int? UsbHeaders3Gen2,
    [Range(0, 20)] int? UsbHeaders2Gen0,
    bool? HasWifi,
    bool? HasBluetooth,
    [Range(0, 100)] int? LanSpeedGbps,
    [Range(0, 30)] int? FanHeaders,
    [Range(0, 30)] int? ArgbHeaders,
    int? VrmPhases,
    [Range(0, 30)] int? RearUsbA,
    [Range(0, 30)] int? RearUsbC,
    [Range(0, 10)] bool? RearHdmi,
    [Range(0, 10)] bool? RearDisplayPort,
    [Range(0, 100000)] decimal? PriceUsd
);
