using PcBuilder.Enums;
using PcBuilder.Models;
using System.Net.Sockets;

namespace PcBuilder.Entities;

public class MotherboardEntity : Component
{
    public PcSocketType Socket { get; set; }
    public string Chipset { get; set; } = string.Empty;
    public FormFactor FormFactor { get; set; }
    public MemoryType MemoryType { get; set; }
    public int MemorySlots { get; set; }
    public int MaxMemoryGb { get; set; }
    public int MaxMemorySpeedMhz { get; set; }
    public int PcieX16Slots { get; set; }
    public int PcieX1Slots { get; set; }
    public int M2Slots { get; set; }
    public int SataPorts { get; set; }
    public int UsbHeaders3Gen2 { get; set; }
    public int UsbHeaders2Gen0 { get; set; }
    public bool HasWifi { get; set; }
    public bool HasBluetooth { get; set; }
    public int LanSpeedGbps { get; set; }
    public int FanHeaders { get; set; }
    public int ArgbHeaders { get; set; }
    public int? VrmPhases { get; set; }
    public int RearUsbA { get; set; }
    public int RearUsbC { get; set; }
    public bool RearHdmi { get; set; }
    public bool RearDisplayPort { get; set; }
    public decimal? PriceUsd { get; set; }


}