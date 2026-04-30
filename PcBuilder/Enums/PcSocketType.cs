using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum PcSocketType
    {
        [PgName("LGA1700")]
        LGA1700,
        [PgName("LGA1851")]
        LGA1851,
        [PgName("LGA1200")]
        LGA1200,
        [PgName("LGA2066")]
        LGA2066,
        [PgName("AM4")]
        AM4,
        [PgName("AM5")]
        AM5,
        [PgName("sTR4")]
        TR4,
        [PgName("sTRX4")]
        STRX4,
        [PgName("SP3")]
        SP3
    }
}
