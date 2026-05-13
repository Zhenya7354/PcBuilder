using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Brand
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
