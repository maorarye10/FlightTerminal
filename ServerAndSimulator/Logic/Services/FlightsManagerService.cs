using Server.DAL.Models.Enums;
using Server.DAL.Models;
using Server.DAL.Repositories;
using Logic.Services;
using Microsoft.Extensions.Logging;

namespace Server.Services
{
    public class FlightsManagerService : IFlightsManagerService
    {
        private readonly ILogger<FlightsManagerService> _logger;
        private readonly IFlightsLogsHubContext _flightsLogsHub;
        private readonly IAirportRepository _repo;
        private static List<Leg> s_legs = new();
        private static int s_currFlights;

        public static int MaxFlights { get; set; }

        public FlightsManagerService(IAirportRepository repo, IFlightsLogsHubContext flightsLogsHub, ILogger<FlightsManagerService> logger)
        {
            lock (s_legs)
            {
                MaxFlights = 4;
                _repo = repo;
                _flightsLogsHub = flightsLogsHub;
                _logger = logger;
                _logger.LogDebug("NLog injected into FlightsManagerService");
                if (s_legs.Count == 0)
                {
                    _logger.LogInformation("FlightsManagerService: Loading legs...");
                    s_legs = (_repo.GetLegsAsync().Result as List<Leg>)!;
                    _logger.LogInformation("FlightsManagerService: Legs Loaded!");
                    ClearLegs();
                }
            }
        }

        public async Task RegisterFlightAsync(Flight newFlight)
        {
            var leg = s_legs.FirstOrDefault(l => l.IsStartingLeg);
            var currEnumLeg = leg!.Number;
            await _repo.AddFlightAsync(newFlight);
            await MoveToNextLeg(newFlight, currEnumLeg, leg.CrossingTime);
        }

        private async Task MoveToNextLeg(Flight flight, Legs currEnumLeg, int secToWait)
        {
            // Verifying leg existence
            var currLeg = s_legs.FirstOrDefault(l => l.Number.HasFlag(currEnumLeg));
            if (currLeg == null)
            {
                _logger.LogError($"FlightsManagerService: Current-Leg was not found ({currEnumLeg})");
                return;
            }

            //await Console.Out.WriteLineAsync($"Flight - {flight.Number!.Substring(0, 8)} - Id: {flight.Id}");
            var log = new Log
            {
                FlightNum = flight.Number,
                BrandName = flight.BrandName,
                PassengersCount = flight.PassengersCount,
                LegNum = currLeg.RepresentationalNumber,
                In = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };

            _logger.LogInformation($"FlightsManagerService: Flight number: {flight.Number!.Substring(0, 8)} (id: {flight.Id}) - Flight brand: {flight.BrandName} - Passengers count: {flight.PassengersCount} - Is-Departing: {flight.IsDeparture} - Is now in leg: {currEnumLeg} - Waiting: {currLeg.CrossingTime} seconds to cross");
            Console.WriteLine($"\n~ Flight number: {flight.Number!.Substring(0, 8)} - Passengers count: {flight.PassengersCount} - Is-Departing: {flight.IsDeparture} - is now in leg: {currEnumLeg} - Waiting: {currLeg.CrossingTime} seconds to cross");

            // Waiting the leg crossing time
            Thread.Sleep(currLeg.CrossingTime * 1000);

            // Check if flight suppose to depart
            if (currLeg.IsDepartureLeg && flight.IsDeparture)
            {
                currLeg.IsOccupied = false;
                await _repo.UpdateLegsAsync(currLeg);

                log.Out = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                await _repo.AddLogAsync(log);
                await _flightsLogsHub.SendFlightLogAsync(log);

                _logger.LogInformation($"FlightsManagerService: Flight number: {flight.Number!.Substring(0, 8)} (id: {flight.Id}) is now departing!");
                Console.WriteLine($"\n~ Flight number: {flight.Number!.Substring(0, 8)} is now departing!");

                s_currFlights--;
                return;
            }

            if (currLeg.ChangePlaneStatus)
            {
                var rnd = new Random();
                flight.PassengersCount = rnd.Next(100, 200);
                flight.IsDeparture = currLeg.ChangePlaneStatus;
                await _repo.UpdateFlightAsync(flight);
            }

            // flight needs to continue to the next leg
            // Finding all possible next legs
            var nextLegs = s_legs.FindAll(l => currLeg.NextLegs.HasFlag(l.Number));
            if (nextLegs == null)
            {
                _logger.LogError($"FlightsManagerService: Next-Legs were not found ({currLeg.NextLegs})");
                return;
            }

            // Move the flight to the next leg
            while (true)
            {
                if (currLeg.IsStartingLeg && s_currFlights == MaxFlights)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                // Finding empty next leg 
                var nextLeg = nextLegs!.FirstOrDefault(l => l.IsOccupied == false);
                
                if (nextLeg == null)
                {
                    _logger.LogInformation($"FlightsManagerService: Flight number: {flight.Number!.Substring(0, 8)} (id: {flight.Id}) is waiting for the next leg to free up");
                    Console.WriteLine("Waiting for the next leg to free up");

                    Thread.Sleep(1000);
                    continue;
                }

                lock (nextLeg)
                {
                    // In case two threads lock on the same leg at the same time and one of them waits for the lock to free up
                    if (nextLeg.IsOccupied)
                    {
                        _logger.LogInformation($"FlightsManagerService: Flight number: {flight.Number!.Substring(0, 8)} (id: {flight.Id}) is waiting for the next leg to free up");
                        Console.WriteLine("Waiting for the next leg to free up");

                        Thread.Sleep(1000);
                        continue;
                    }
                    nextLeg.IsOccupied = true;
                    currLeg.IsOccupied = false;
                    s_currFlights = currLeg.IsStartingLeg ? ++s_currFlights : s_currFlights;
                }

                await _repo.UpdateLegsAsync(nextLeg, currLeg);
                log.Out = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                log.NextLegNum = nextLeg.RepresentationalNumber;
                await _repo.AddLogAsync(log);
                await _flightsLogsHub.SendFlightLogAsync(log);
                await MoveToNextLeg(flight, nextLeg.Number, nextLeg.CrossingTime);
                break;
            }

        }

        public async Task<List<Log>> GetFlightsLogsAsync()
        {
            var logs = (await _repo.GetLogsAsync()) as List<Log>;
            return logs == null ? new List<Log>() : logs;
        }

        private void ClearLegs()
        {
            _logger.LogInformation($"FlightsManagerService: Clearing legs...");
            Console.WriteLine("~ Clearing legs...");
            foreach (var leg in s_legs)
                leg.IsOccupied = false;
            _logger.LogInformation($"FlightsManagerService: Legs cleared!");
        }
    }
}
