using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace PcBuilder.Models
{
    public class Motherboard : Component
    {
        [Column("socket")]
        public PcSocketType Socket { get; set; }
        [Column("chipset")]
        public string Chipset { get; set; } = string.Empty;
        [Column("form_factor")]
        public FormFactor FormFactor { get; set; }
        [Column("memory_type")]
        public MemoryType MemoryType { get; set; }
        [Column("memory_slots")]
        public int MemorySlots { get; set; }
        [Column("max_memory_gb")]
        public int MaxMemoryGb { get; set; }
        [Column("max_memory_speed_mhz")]
        public int MaxMemorySpeedMhz { get; set; }
        [Column("pcie_x16_slots")]
        public int PcieX16Slots { get; set; }
        [Column("pcie_x1_slots")]
        public int PcieX1Slots { get; set; }
        [Column("m2_slots")]
        public int M2Slots { get; set; }
        [Column("sata_ports")]
        public int SataPorts { get; set; }
        [Column("usb_headers_3_2")]
        public int UsbHeaders3Gen2 { get; set; }
        [Column("usb_headers_2_0")]
        public int UsbHeaders2Gen0 { get; set; }
        [Column("has_wifi")]
        public bool HasWifi { get; set; }
        [Column("has_bluetooth")]
        public bool HasBluetooth { get; set; }
        [Column("lan_speed_gbps")]
        public int LanSpeedGbps { get; set; }
        [Column("fan_headers")]
        public int FanHeaders { get; set; }
        [Column("argb_headers")]
        public int ArgbHeaders { get; set; }
        [Column("vrm_phases")]
        public int? VrmPhases { get; set; }
        [Column("rear_usb_a")]
        public int RearUsbA { get; set; }
        [Column("rear_usb_c")]
        public int RearUsbC { get; set; }
        [Column("rear_hdmi")]
        public bool RearHdmi { get; set; }
        [Column("rear_displayport")]
        public bool RearDisplayPort { get; set; }
        [Column("price_usd")]
        public decimal? PriceUsd { get; set; }


    }
}
