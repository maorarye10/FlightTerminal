using Logic.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.DAL.Models;
using Server.Services;
using WebAPI.Hubs;
using WebAPI.Hubs.Clients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsManagerService _flightManager;
        private readonly ILogger<FlightsController> _logger;
        public FlightsController(IFlightsManagerService flightManager, ILogger<FlightsController> logger)
        {
            _flightManager = flightManager;
            _logger = logger;
            _logger.LogDebug("NLog injected into FlightsController");
        }

        // GET: api/<FlightsController>
        //[EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<List<Log>>> Get()
        {
            _logger.LogInformation("FlightsController - GET: Got new GET request!");
            await Console.Out.WriteLineAsync("New GET Request!");
            return await _flightManager.GetFlightsLogsAsync();
        }

        // POST api/<FlightsController>
        [HttpPost]
        public async Task Post([FromBody] Flight flight)
        {
            _logger.LogInformation($"FlightsController - POST: Got new flight! - {flight.Number!.Substring(0, 8)} - {flight.BrandName} - {flight.PassengersCount}");
            await Console.Out.WriteLineAsync($"\n* New Flight - {flight.Number!.Substring(0, 8)} - {flight.BrandName} - {flight.PassengersCount} - Landing *");
            await _flightManager.RegisterFlightAsync(flight);
        }









        // GET api/<FlightsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // PUT api/<FlightsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<FlightsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}






    }
}
