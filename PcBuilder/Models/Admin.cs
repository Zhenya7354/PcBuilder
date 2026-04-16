namespace PcBuilder.Models
{
    public class Admin
    {
        public int id { get; set; }
        public string? userId { get; set; } = string.Empty;
        public User user { get; set; }
    }
}
