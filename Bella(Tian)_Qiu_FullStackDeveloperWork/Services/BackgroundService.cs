using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Microsoft.AspNetCore.SignalR;

public class RegistrationCheckService : BackgroundService
{
    private readonly CarsStore _store;
    private readonly IHubContext<CarStatusHub> _hub;

    public RegistrationCheckService(CarsStore store, IHubContext<CarStatusHub> hub)
    {
        _store = store;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //Console.WriteLine("Broadcasting data: " + DateTime.Now);
            var result = _store.Cars.Select(c => new
            {
                c.Id,
                c.Make,
                c.Owner,
                c.LicenseNumber,
                c.RegistrationExpiry,
                IsExpired = c.RegistrationExpiry < DateTime.Now
            }).ToList();

            await _hub.Clients.All.SendAsync("RegistrationStatusUpdated", result);

            await Task.Delay(60000, stoppingToken);
        }
    }
}
