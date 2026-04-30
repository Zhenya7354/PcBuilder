using NpgsqlTypes;

namespace PcBuilder.Enums
{
    public enum PsuRating
    {
        [PgName("80+ White")]
        White,
        [PgName("80+ Bronze")]
        Bronze,
        [PgName("80+ Silver")]
        Silver,
        [PgName("80+ Gold")]
        Gold,
        [PgName("80+ Platinum")]
        Platinum,
        [PgName("80+ Titanium")]
        Titanium
    }
}
