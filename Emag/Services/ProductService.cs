using Contracts;
using Emag.Data;
using Emag.DTOs;
using Emag.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Emag.Services
{
    public class ProductService
    {
        private readonly ProductDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProductService(ProductDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();

            return products;
        }

        public async Task AddProduct(AddProductDTO productDTO)
        {
            var product = AddProductDTO.FromDTO(productDTO);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productCreated = new ProductCreated()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = (int)product.Category,
            };

            await _publishEndpoint.Publish(productCreated);
        }
    }
}
