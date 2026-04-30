using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum PanelType
    {
        [PgName("IPS")]
        IPS,
        [PgName("TN")]
        TN,
        [PgName("VA")]
        VA,
        [PgName("OLED")]
        OLED,
        [PgName("QD-OLED")]
        QDOLED,
        [PgName("Mini-LED")]
        MiniLED
    }
}
