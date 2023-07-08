// See https://aka.ms/new-console-template for more information

using RandomNameGeneratorLibrary;
using Simulator.Models;
using System.Net.Http.Json;


var client = new HttpClient { BaseAddress = new Uri("https://localhost:7014") };
var placeGenerator = new PlaceNameGenerator();

async Task GenerateFlightAsync()
{
    var rnd = new Random();
    var res = rnd.Next(0, 2);
    var flight = new FlightDTO
    {
        Number = Guid.NewGuid().ToString(),
        PassengersCount = rnd.Next(100, 200),
        BrandName = placeGenerator.GenerateRandomPlaceName() + " Airlines"
    };

    Console.WriteLine($"Brand Name: {flight.BrandName}, Passsengers: {flight.PassengersCount}, Departure: FALSE");
    await client.PostAsJsonAsync("api/flights", flight);
}

System.Timers.Timer timer = new System.Timers.Timer(5000);
timer.Elapsed += async (s, e) => await GenerateFlightAsync();
timer.Start();

//Thread.Sleep(1);
//GenerateFlightAsync();
//Thread.Sleep(1);
//GenerateFlightAsync();
//Thread.Sleep(5000);
//GenerateFlightAsync();
//Thread.Sleep(5000);
//GenerateFlightAsync();
//Thread.Sleep(5000);
//GenerateFlightAsync();


Console.ReadLine();