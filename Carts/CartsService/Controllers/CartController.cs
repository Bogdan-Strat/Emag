using CartsService.DTOs;
using CartsService.Entities;
using CartsService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartsService.Controllers
{
    [ApiController]
    [Route("/carts")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllCarts()
        {
            return Ok(await _service.GetAllCarts());
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProductToCart(AddCartDTO cart)
        {
            await _service.AddProductToCart(cart);
            return Ok();
        }

        [HttpPost("remove")]
        [Authorize]
        public async Task<IActionResult> RemoveProductFromCart(AddCartDTO cart)
        {
            await _service.RemoveProductFromCart(cart);
            return Ok();
        }
    }
}
