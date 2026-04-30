using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum MemoryType
    {
        [PgName("DDR4")]
        DDR4,
        [PgName("DDR5")]
        DDR5,
        [PgName("DDR4/DDR5")]
        Mixed
    }
}
