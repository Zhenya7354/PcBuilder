using NpgsqlTypes;

namespace PcBuilder.Enums;

public enum StorageInterface
{
    Sata_3,
    NvmePcie_Gen3,
    NvmePcie_Gen4,
    NvmePcie_Gen5,
    SAS
}
