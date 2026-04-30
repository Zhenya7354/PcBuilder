using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum FormFactor
    {
        [PgName("ATX")]
        ATX,
        [PgName("Micro-ATX")]
        MicroATX,
        [PgName("Mini-ITX")]
        MiniITX,
        [PgName("E-ATX")]
        EATX,
        [PgName("SSI-CEB")]
        SSICEB,
        [PgName("XL-ATX")]
        XLATX
    }
}
