using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record MotherboardCreate(
    [Required] string Name,
    [Required] int BrandId,
    [Required] PcSocketType Socket,
    [Required] string Chipset,
    [Required] FormFactor FormFactor,
    [Required] MemoryType MemoryType,
    [Required, Range(1, 16)] int MemorySlots,
    [Required, Range(1, 2048)] int MaxMemoryGb,
    [Required, Range(800, 10000)] int MaxMemorySpeedMhz,
    [Required, Range(0, 10)] int PcieX16Slots,
    [Required, Range(0, 10)] int PcieX1Slots,
    [Required, Range(0, 20)] int M2Slots,
    [Required, Range(0, 20)] int SataPorts,
    [Required, Range(0, 20)] int UsbHeaders3Gen2,
    [Required, Range(0, 20)] int UsbHeaders2Gen0,
    bool HasWifi,
    bool HasBluetooth,
    [Required, Range(0, 100)] int LanSpeedGbps,
    [Required, Range(0, 30)] int FanHeaders,
    [Required, Range(0, 30)] int ArgbHeaders,
    int? VrmPhases,
    [Required, Range(0, 30)] int RearUsbA,
    [Required, Range(0, 30)] int RearUsbC,
    [Required, Range(0, 10)] bool RearHdmi,
    [Required, Range(0, 10)] bool RearDisplayPort,
    [Range(0, 100000)] decimal? PriceUsd
);
