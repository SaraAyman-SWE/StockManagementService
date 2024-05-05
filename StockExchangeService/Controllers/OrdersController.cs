using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StockExchangeService.Models.Dtos;
using StockExchangeService.Services.Interfaces;

namespace StockExchangeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IValidator<OrderCreateDto> _orderValidator;
        private readonly IOrderService _service;
        public OrdersController(IValidator<OrderCreateDto> orderValidator, IOrderService service)
        {
            _orderValidator = orderValidator;
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateDto dto)
        {
            var validationResult = _orderValidator.Validate(dto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            await _service.Create(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetHistory()
        {
            var result = await _service.GetListAsync();
            return Ok(result);
        }
    }
}
