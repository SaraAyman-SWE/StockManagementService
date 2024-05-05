using StockExchangeService.Models.Dtos;

namespace StockExchangeService.Services.Interfaces
{
    public interface IOrderService
    {
        Task Create(OrderCreateDto dto);
        Task<List<OrderResponseDto>> GetListAsync();
    }
}
