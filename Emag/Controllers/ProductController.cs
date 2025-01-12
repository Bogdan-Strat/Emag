using Emag.DTOs;
using Emag.Entities;
using Emag.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emag.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("/all")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO productDTO)
        {
            await _service.AddProduct(productDTO);

            return Ok();
        }
    }
}
