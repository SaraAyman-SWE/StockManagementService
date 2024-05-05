using StockExchangeService.Models.Dtos;

namespace StockExchangeService.Services.Interfaces
{
    public interface IStockService
    {
        Task<List<StockDto>?> GetRealTimeStockData();
        Task<List<HistoricalStockDto>?> GetHistoricalStockData(string symbol);
    }
}
