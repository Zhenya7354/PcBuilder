using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum PsuModular
    {
        [PgName("Non-Modular")]
        NonModular,
        [PgName("Semi-Modular")]
        SemiModular,
        [PgName("Fully Modular")]
        FullyModular
    }
}
