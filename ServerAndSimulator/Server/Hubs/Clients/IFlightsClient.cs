using Server.DAL.Models;

namespace WebAPI.Hubs.Clients
{
    public interface IFlightsClient
    {
        Task ReciveFlightLog(Log log);
    }
}
