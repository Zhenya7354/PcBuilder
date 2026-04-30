using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace PcBuilder.Models
{
    public class Motherboard : Component
    {
        [Column("socket")]
        public PcSocketType socket { get; set; }
        [Column("chipset")]
        public string chipset { get; set; } = string.Empty;
        [Column("form_factor")]
        public FormFactor formFactor { get; set; }
        [Column("memory_type")]
        public MemoryType memoryType { get; set; }
        [Column("memory_slots")]
        public int memorySlots { get; set; }
        [Column("max_memory_gb")]
        public int maxMemoryGb { get; set; }
        [Column("max_memory_speed_mhz")]
        public int maxMemorySpeedMhz { get; set; }
        [Column("pcie_x16_slots")]
        public int pcieX16Slots { get; set; }
        [Column("pcie_x1_slots")]
        public int pcieX1Slots { get; set; }
        [Column("m2_slots")]
        public int m2Slots { get; set; }
        [Column("sata_ports")]
        public int sataPorts { get; set; }
        [Column("usb_headers_3_2")]
        public int usbHeaders3Gen2 { get; set; }
        [Column("usb_headers_2_0")]
        public int usbHeaders2Gen0 { get; set; }
        [Column("has_wifi")]
        public bool hasWifi { get; set; }
        [Column("has_bluetooth")]
        public bool hasBluetooth { get; set; }
        [Column("lan_speed_gbps")]
        public int lanSpeedGbps { get; set; }
        [Column("fan_headers")]
        public int fanHeaders { get; set; }
        [Column("argb_headers")]
        public int argbHeaders { get; set; }
        [Column("vrm_phases")]
        public int? vrmPhases { get; set; }
        [Column("rear_usb_a")]
        public int rearUsbA { get; set; }
        [Column("rear_usb_c")]
        public int rearUsbC { get; set; }
        [Column("rear_hdmi")]
        public int rearHdmi { get; set; }
        [Column("rear_display_port")]
        public int rearDisplayPort { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }


    }
}
