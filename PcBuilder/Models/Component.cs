namespace PcBuilder.Models
{
    public abstract class Component
    {
        public int id { get; set; }
        public int brandId { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
