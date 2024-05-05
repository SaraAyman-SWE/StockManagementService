using AutoMapper;
using StockExchangeService.Models.Dtos;
using StockExchangeService.Models.Entities;
using StockExchangeService.Repositories;
using StockExchangeService.Services.Interfaces;

namespace StockExchangeService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Create(OrderCreateDto dto)
        {
            await _repository.InsertAsync(_mapper.Map<Order>(dto));
            await _repository.SaveAsync();
        }
        public async Task<List<OrderResponseDto>> GetListAsync()
        {
            var orders = await _repository.GetListAsync();
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }
    }
}
