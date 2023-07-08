using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.DAL.Models;

namespace Server.DAL.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly ILogger<AirportRepository> _logger;
        private readonly DataContext _context;
        public AirportRepository(DataContext context, ILogger<AirportRepository> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug("NLog injected into AirportRepository");
        }

        public async Task AddFlightAsync(Flight flight)
        {
            try
            {
                _logger.LogInformation($"AirportRepository: Trying to add new flight ({flight.Number}) to database...");
                _context.Add(flight);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"AirportRepository: Flight ({flight.Number}) added!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to add flight ({flight.Number}) to database: {e.Message}");
                throw;
            }
        }

        public async Task AddLogAsync(Log log)
        {
            try
            {
                _logger.LogInformation("AirportRepository: Trying to add new log to database...");
                _context.Add(log);
                await _context.SaveChangesAsync();
                _logger.LogInformation("AirportRepository: Log added!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to add log to database: {e.Message}");
                throw;
            }
        }

        public async Task<ICollection<Flight>> GetFlightsAsync()
        {
            try
            {
                _logger.LogInformation("AirportRepository: Trying to get flights list from database...");
                var flights = await _context.Flights.ToListAsync();
                _logger.LogInformation("AirportRepository: Flights list found and returned!");
                return flights;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to get flights list from database: {e.Message}");
                throw;
            }
        }

        public async Task<ICollection<Leg>> GetLegsAsync()
        {
            try
            {
                _logger.LogInformation("AirportRepository: Trying to get legs list from database...");
                var legs = await _context.Legs.ToListAsync();
                _logger.LogInformation("AirportRepository: Legs list found and returned!");
                return legs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to get legs list from database: {e.Message}");
                throw;
            }
        }

        public async Task<ICollection<Log>> GetLogsAsync()
        {
            try
            {
                _logger.LogInformation("AirportRepository: Trying to get logs list from database...");
                var logs = await _context.Logs.ToListAsync();
                _logger.LogInformation("AirportRepository: Logs list found and returned!");
                return logs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to get logs list from database: {e.Message}");
                throw;
            }
        }

        private async Task UpdateLegAsync(Leg leg)
        {
            try
            {
                _logger.LogInformation($"AirportRepository: Trying to update leg ({leg.Number}) in context...");
                var reqLeg = await _context.Legs.FirstOrDefaultAsync(l => l.Id == leg.Id);
                if (reqLeg != null)
                {
                    reqLeg.Number = leg.Number;
                    reqLeg.CrossingTime = leg.CrossingTime;
                    reqLeg.NextLegs = leg.NextLegs;
                    reqLeg.ChangePlaneStatus = leg.ChangePlaneStatus;
                    reqLeg.IsStartingLeg = leg.IsStartingLeg;
                    reqLeg.IsDepartureLeg = leg.IsDepartureLeg;
                    reqLeg.IsOccupied = leg.IsOccupied;
                }
                _logger.LogInformation($"AirportRepository: Leg ({leg.Number}) updated in context!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to update leg ({leg.Number}) in context: {e.Message}");
                throw;
            }
        }

        public async Task UpdateLegsAsync(params Leg[] legs)
        {
            foreach (var leg in legs)
                await UpdateLegAsync(leg);

            try
            {
                _logger.LogInformation("AirportRepository: Trying to update legs in database...");
                await _context.SaveChangesAsync();
                _logger.LogInformation("AirportRepository: Legs updated in database!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to update legs in database: {e.Message}");
                throw;
            }
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            try
            {
                _logger.LogInformation($"AirportRepository: Trying to update flight ({flight.Id}) in database...");
                var reqFlight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == flight.Id);

                if (reqFlight != null)
                {
                    reqFlight.BrandName = flight.BrandName;
                    reqFlight.PassengersCount = flight.PassengersCount;
                    reqFlight.IsDeparture = flight.IsDeparture;
                    await _context.SaveChangesAsync();
                }
                _logger.LogInformation($"AirportRepository: Flight ({flight.Id}) updated in database!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AirportRepository: Failed to update flight ({flight.Id}) in database: {e.Message}");
                throw;
            }
        }
    }
}
