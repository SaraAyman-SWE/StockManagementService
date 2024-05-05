using Microsoft.AspNetCore.SignalR;
using StockExchangeService.Hubs;
using StockExchangeService.Services.Interfaces;

namespace StockExchangeService.Services.BackgroundServices
{
    public class StockDataBackgroundService : BackgroundService
    {
        private readonly IHubContext<StockTickerHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public StockDataBackgroundService(IHubContext<StockTickerHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var stockDataService = scope.ServiceProvider.GetRequiredService<IStockService>();
            var stockData = await stockDataService.GetRealTimeStockData();
            await _hubContext.Clients.All.SendAsync("StockDataUpdated", stockData);
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
