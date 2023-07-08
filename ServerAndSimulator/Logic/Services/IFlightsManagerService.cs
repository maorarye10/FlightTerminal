using Server.DAL.Models;

namespace Server.Services
{
    public interface IFlightsManagerService
    {
        static int MaxFlights { get; set; }
        Task RegisterFlightAsync(Flight flight);
        Task<List<Log>> GetFlightsLogsAsync();
    }
}
