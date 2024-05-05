using Microsoft.AspNetCore.Mvc;
using StockExchangeService.Services.Interfaces;

namespace StockExchangeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _service;
        public StocksController(IStockService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetRealTimeData()
        {
            var result = await _service.GetRealTimeStockData();
            return Ok(result);
        }

        [HttpGet]
        [Route("{symbol}/history")]
        public async Task<ActionResult> GetHistoricalData(string symbol)
        {
            var result = await _service.GetHistoricalStockData(symbol);
            return Ok(result);
        }
    }
}
