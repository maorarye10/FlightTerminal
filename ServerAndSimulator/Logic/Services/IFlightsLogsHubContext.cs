using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IFlightsLogsHubContext
    {
        Task SendFlightLogAsync(Log log);
    }
}
