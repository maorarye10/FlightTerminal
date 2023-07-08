using Logic.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Server.DAL;
using Server.DAL.Repositories;
using Server.Services;
using WebAPI.Hubs;
using WebAPI.Services;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("[NLogger]: init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("db"), b => b.MigrationsAssembly("WebAPI"));
    });
    builder.Services.AddSingleton<IFlightsLogsHubContext, FlightsLogsHubContext>();
    builder.Services.AddScoped<IAirportRepository, AirportRepository>();
    builder.Services.AddScoped<IFlightsManagerService, FlightsManagerService>();
    builder.Services.AddControllers();
    builder.Services.AddSignalR();
    builder.Services.AddCors(options =>
    {
        //options.AddPolicy(
        //    name: "ReactUI",
        //    builder =>
        //    {
        //        builder
        //        .AllowAnyOrigin()
        //        .AllowAnyHeader()
        //        .AllowAnyMethod();
        //    });

        options.AddDefaultPolicy(
                builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
                }
            );
    });


    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();



    var app = builder.Build();

    // Configure the HTTP request pipeline.

    //app.UseCors("ReactUI");
    app.UseCors();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<FlightsLogsHub>("/hubs/flights-logs-hub");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
