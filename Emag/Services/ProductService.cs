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
        private readonly CurrentUserDTO _currentUser;
        public ProductService(ProductDbContext context, IPublishEndpoint publishEndpoint, CurrentUserDTO currentUser)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _currentUser = currentUser;
        }

        public async Task<List<GetProductDTO>> GetAllProducts()
        {
            var products = await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();

            return products
                .Select(p => GetProductDTO.FromEntity(p))
                .ToList();
        }

        public async Task AddProduct(AddProductDTO productDTO)
        {
            if (_currentUser.RoleId != 0) return;

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

        public async Task<List<ProductForCartDTO>> GetProductsByIds(List<GetProductsByIdDTO> dto)
        {
            var productsIds = dto
                .Select(d => d.Id)
                .ToList();

            var products = await _context.Products
                .Where(p => productsIds.Contains(p.Id))
                .Select(p => ProductForCartDTO.FromEntity(p))
                .ToListAsync();

            return products;
        }
    }
}
