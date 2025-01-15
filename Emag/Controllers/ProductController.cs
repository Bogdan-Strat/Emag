using Contracts;
using Emag.DTOs;
using Emag.Entities;
using Emag.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emag.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly CurrentUserDTO _currentUserDTO;
        public ProductController(ProductService service, CurrentUserDTO user)
        {
            _service = service;
            _currentUserDTO = user;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO productDTO)
        {
            await _service.AddProduct(productDTO);

            return Ok();
        }

        [HttpPost("getCart")]
        [Authorize]
        public async Task<IActionResult> GetProductsByIds([FromBody] List<Guid> productsIds)
        {
            return Ok(await _service.GetProductsByIds(productsIds));
        }
    }
}
