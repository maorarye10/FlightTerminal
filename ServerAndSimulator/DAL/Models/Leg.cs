using Server.DAL.Models.Enums;

namespace Server.DAL.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public int RepresentationalNumber { get; set; }
        public Legs Number { get; set; }
        public int CrossingTime { get; set; }
        public Legs NextLegs { get; set; }
        public bool ChangePlaneStatus { get; set; }
        public bool IsStartingLeg { get; set; }
        public bool IsDepartureLeg { get; set; }
        public bool IsOccupied { get; set; }
    }
}
