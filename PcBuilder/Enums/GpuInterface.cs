using NpgsqlTypes;

namespace PcBuilder.Enums;

public enum GpuInterface
{
    [PgName("PCIe 3.0 x16")]
    PCle3x16,
    [PgName("PCIe 4.0 x16")]
    PCle4x16,
    [PgName("PCIe 5.0 x16")]
    PCle5x16,
}
