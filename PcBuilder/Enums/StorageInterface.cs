using NpgsqlTypes;

namespace PcBuilder.Enums;

public enum StorageInterface
{
    [PgName("SATA III")]
    Sata_3,
    [PgName("NVMe PCIe 3.0")]
    NvmePcie_Gen3,
    [PgName("NVMe PCIe 4.0")]
    NvmePcie_Gen4,
    [PgName("NVMe PCIe 5.0")]
    NvmePcie_Gen5,
    [PgName("SAS")]
    SAS
}
