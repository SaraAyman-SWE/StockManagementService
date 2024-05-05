using AutoMapper;
using StockExchangeService.Models.Dtos;
using StockExchangeService.Models.Entities;

namespace StockExchangeService.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<OrderCreateDto, Order>().ReverseMap();
            CreateMap<Order, OrderResponseDto>().ReverseMap();
        }
    }
}
