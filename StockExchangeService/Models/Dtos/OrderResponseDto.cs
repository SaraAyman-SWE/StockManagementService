namespace StockExchangeService.Models.Dtos
{
    public class OrderResponseDto : BaseEntityDto
    {
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string StockSymbol { get; set; }
    }
}
