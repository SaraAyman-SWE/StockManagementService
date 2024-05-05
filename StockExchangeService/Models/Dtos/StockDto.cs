using Newtonsoft.Json;

namespace StockExchangeService.Models.Dtos
{
    public class StockDto
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("askPrice")]
        public double AskPrice { get; set; }
        [JsonProperty("bidPrice")]
        public double BidPrice { get; set; }
        [JsonProperty("lastUpdated")]
        public long LastUpdatedValue { set { LastUpdated = DateTimeOffset.FromUnixTimeSeconds(value / 1000).UtcDateTime; } }
        public DateTime LastUpdated { get; set; }
    }
}
