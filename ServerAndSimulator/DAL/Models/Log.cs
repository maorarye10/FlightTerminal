using System.ComponentModel.DataAnnotations;

namespace Server.DAL.Models
{
    public class Log
    {
        public int Id { get; set; }
        [Required]
        public string? FlightNum { get; set; }
        [Required]
        public string? BrandName { get; set; }
        public int PassengersCount { get; set; }
        public int LegNum { get; set; }
        public int NextLegNum { get; set; }
        [Required]
        public string? In { get; set; }
        [Required]
        public string? Out { get; set; }
    }
}
