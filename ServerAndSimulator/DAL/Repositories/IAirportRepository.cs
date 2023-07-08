using Server.DAL.Models;

namespace Server.DAL.Repositories
{
    public interface IAirportRepository
    {
        Task<ICollection<Flight>> GetFlightsAsync();
        Task<ICollection<Leg>> GetLegsAsync();
        Task<ICollection<Log>> GetLogsAsync();

        Task AddFlightAsync(Flight flight);
        Task AddLogAsync(Log log);

        Task UpdateLegsAsync(params Leg[] legs);
        Task UpdateFlightAsync(Flight flight);
    }
}
