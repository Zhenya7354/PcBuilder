using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum CoolerType
    {
        [PgName("Air")]
        Air,
        [PgName("AIO Liquid")]
        Liquid,
        [PgName("Custom Liquid")]
        CustomLiquid
    }
}
