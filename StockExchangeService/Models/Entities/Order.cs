using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchangeService.Models.Entities
{
    [Table(name: "Orders")]
    public class Order : BaseEntity
    {
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string StockSymbol { get; set; }
    }
}
