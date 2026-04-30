using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum StorageFormFactor
    {
        [PgName("2.5")]
        Sata_2_5,
        [PgName("3.5")]
        Sata_3_5,
        [PgName("M.2 2242")]
        M2_2242,
        [PgName("M.2 2260")]
        M2_2260,
        [PgName("M.2 2280")]
        M2_2280,
        [PgName("M.2 22110")]
        M2_22110,
        [PgName("U.2")]
        U2,
        [PgName("Add-In Card")]
        AddInCard
    }
}
