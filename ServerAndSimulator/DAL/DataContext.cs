using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.DAL.Models;

namespace Server.DAL
{
    public class DataContext : DbContext
    {
        private readonly ILogger<DataContext> _logger;
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DataContext(DbContextOptions<DataContext> options, ILogger<DataContext> logger) : base(options)
        {
            _logger = logger;
            _logger.LogDebug("NLog injected into DataContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger.LogInformation("DataContext: Validating and creating data in database...");
            modelBuilder.Entity<Leg>().HasData(
                new Leg
                {
                    Id = 1,
                    RepresentationalNumber = 0,
                    Number = Models.Enums.Legs.Air,
                    IsStartingLeg = true,
                    CrossingTime = 0,
                    NextLegs = Models.Enums.Legs.First
                },
                new Leg
                {
                    Id = 2,
                    RepresentationalNumber = 1,
                    Number = Models.Enums.Legs.First,
                    CrossingTime = 1,
                    NextLegs = Models.Enums.Legs.Second
                },
                new Leg
                {
                    Id = 3,
                    RepresentationalNumber = 2,
                    Number = Models.Enums.Legs.Second,
                    CrossingTime = 2,
                    NextLegs = Models.Enums.Legs.Third
                },
                new Leg
                {
                    Id = 4,
                    RepresentationalNumber = 3,
                    Number = Models.Enums.Legs.Third,
                    CrossingTime = 3,
                    NextLegs = Models.Enums.Legs.Fourth
                },
                new Leg
                {
                    Id = 5,
                    RepresentationalNumber = 4,
                    Number = Models.Enums.Legs.Fourth,
                    IsDepartureLeg = true,
                    CrossingTime = 5,
                    NextLegs = Models.Enums.Legs.Fifth
                },
                new Leg
                {
                    Id = 6,
                    RepresentationalNumber = 5,
                    Number = Models.Enums.Legs.Fifth,
                    CrossingTime = 3,
                    NextLegs = Models.Enums.Legs.Sixth | Models.Enums.Legs.Seventh
                },
                new Leg
                {
                    Id = 7,
                    RepresentationalNumber = 6,
                    Number = Models.Enums.Legs.Sixth,
                    CrossingTime = 10,
                    NextLegs = Models.Enums.Legs.Eighth,
                    ChangePlaneStatus = true
                },
                new Leg
                {
                    Id = 8,
                    RepresentationalNumber = 7,
                    Number = Models.Enums.Legs.Seventh,
                    CrossingTime = 10,
                    NextLegs = Models.Enums.Legs.Eighth,
                    ChangePlaneStatus = true
                },
                new Leg
                {
                    Id = 9,
                    RepresentationalNumber = 8,
                    Number = Models.Enums.Legs.Eighth,
                    CrossingTime = 5,
                    NextLegs = Models.Enums.Legs.Fourth
                }
            );

            _logger.LogInformation("DataContext: validated data!");
        }
    }
}
