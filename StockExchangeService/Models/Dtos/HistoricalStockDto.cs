using Newtonsoft.Json;

namespace StockExchangeService.Models.Dtos
{
    public class HistoricalStockDto
    {
        [JsonProperty("open")]
        public double Open { get; set; }
        [JsonProperty("high")]
        public double High { get; set; }
        [JsonProperty("low")]
        public double Low { get; set; }
        [JsonProperty("close")]
        public double Close { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
