namespace PcBuilder.Models
{
    public class RegularUser
    {
        public int id { get; set; }
        public string userId { get; set; } = string.Empty;
        public string prefferedCurrency { get; set; } = string.Empty;
        public string postalCode { get; set; } = string.Empty;
        public int buildsCreated { get; set; }
        public User user { get; set; }
    }
}
