using Microsoft.AspNetCore.SignalR;
using Server.DAL.Models;
using WebAPI.Hubs.Clients;

namespace WebAPI.Hubs
{
    public class FlightsLogsHub : Hub<IFlightsClient>
    {
    }
}
