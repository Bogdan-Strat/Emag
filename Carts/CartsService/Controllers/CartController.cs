using CartsService.DTOs;
using CartsService.Entities;
using CartsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartsService.Controllers
{
    [ApiController]
    [Route("/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpGet("/all")]
        public async Task<IActionResult> GetAllCarts()
        {
            return Ok(await _service.GetAllCarts());
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddCart(AddCartDTO cart)
        {
            await _service.AddCart(cart);
            return Ok();
        }
    }
}
