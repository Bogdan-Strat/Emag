using Contracts;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _service;

        public OrderController(OrderServices service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] List<AddOrderDTO> dtos)
        {
            await _service.AddOrder(dtos);

            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            return Ok(await _service.GetHistory());
        }

    }
}
