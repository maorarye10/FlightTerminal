using System.ComponentModel.DataAnnotations;

namespace Server.DAL.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string? Number { get; set; }
        [Required]
        public string? BrandName { get; set; }
        public int PassengersCount { get; set; }
        public bool IsDeparture { get; set; }
    }
}
