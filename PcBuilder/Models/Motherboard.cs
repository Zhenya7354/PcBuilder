namespace PcBuilder.Models
{
    public class Motherboard : Component
    {
        public string socket { get; set; } = string.Empty;
        public string chipset { get; set; } = string.Empty;
        public string memoryType { get; set; } = string.Empty;
        public int memorySlots { get; set; }
        public int maxMemoryGb { get; set; }
        public int maxMemorySpeedMhz { get; set; }
        public int pcieX16Slots { get; set; }
        public int pcieX1Slots { get; set; }
        public int m2Slots { get; set; }
        public int sataPorts { get; set; }
        public int usbHeaders3Gen2 { get; set; }
        public int usbHeaders2Gen0 { get; set; }
        public bool hasWifi { get; set; }
        public bool hasBluetooth { get; set; }
        public int lanSpeedGbps { get; set; }
        public int fanHeaders { get; set; }
        public int argbHeaders { get; set; }
        public int? vrmPhases { get; set; }
        public int rearUsbA { get; set; }
        public int rearUsbC { get; set; }
        public int rearHdmi { get; set; }
        public int rearDisplayPort { get; set; }
        public decimal? priceUsd { get; set; }


    }
}
