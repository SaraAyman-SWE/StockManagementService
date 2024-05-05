using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using StockExchangeService.Models.Dtos;
using StockExchangeService.Services.Interfaces;

namespace StockExchangeService.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StockService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<StockDto>?> GetRealTimeStockData()
        {
            var response = await UsingFinancialModelingPrep();
            return !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<List<StockDto>>(response) : new List<StockDto>();
        }

        public async Task<List<HistoricalStockDto>?> GetHistoricalStockData(string symbol)
        {
            var response = await UsingFinancialModelingPrep(symbol, true);
            var deserializedRes = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<JObject>(response) : new JObject();
            return response != null ? (deserializedRes["historical"] as JArray)?.ToObject<List<HistoricalStockDto>>()! : null;
        }
        private async Task<string> UsingAlphaVantage(string symbol)
        {
            var baseUrl = _configuration.GetValue<string>("AlphaVantage:BaseUrl");
            var apiKey = _configuration.GetValue<string>("AlphaVantage:ApiKey") ?? "demo";
            var url = $"{baseUrl}function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=5min&apikey={apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string?> UsingFinancialModelingPrep(string symbol = null, bool isHistoricalData = false)
        {
            try
            {
                var baseUrl = _configuration.GetValue<string>("FinancialModelingPrep:BaseUrl");
                var apiKey = _configuration.GetValue<string>("FinancialModelingPrep:ApiKey");
                var url = isHistoricalData ? $"{baseUrl}/api/v3/historical-price-full/{symbol}?apikey={apiKey}" :
                    $"{baseUrl}/api/v3/stock/full/real-time-price?apikey={apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
