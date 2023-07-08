using Logic.Services;
using Microsoft.AspNetCore.SignalR;
using Server.DAL.Models;
using WebAPI.Hubs;
using WebAPI.Hubs.Clients;

namespace WebAPI.Services
{
    public class FlightsLogsHubContext : IFlightsLogsHubContext
    {
        private readonly IHubContext<FlightsLogsHub, IFlightsClient> _flightsLogsHub;
        private readonly ILogger<FlightsLogsHubContext> _logger;

        public FlightsLogsHubContext(IHubContext<FlightsLogsHub, IFlightsClient> flightsLogsHub, ILogger<FlightsLogsHubContext> logger)
        {
            _flightsLogsHub = flightsLogsHub;
            _logger = logger;
            _logger.LogDebug("NLog injected into FlightsLogsHubContext");
        }
        public async Task SendFlightLogAsync(Log log)
        {
            _logger.LogInformation("FlightsLogsHubContext: Sending new flight log to clients...");
            try
            {
                await _flightsLogsHub.Clients.All.ReciveFlightLog(log);
                _logger.LogInformation("FlightsLogsHubContext: Flight log sent!");
            }
            catch (Exception e)
            {
                _logger.LogError($"FlightsLogsHubContext: failed to send log to clients! - Error: {e.Message}");
            }
        }
    }
}
