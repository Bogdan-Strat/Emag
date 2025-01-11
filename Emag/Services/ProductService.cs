using Emag.Data;
using Emag.DTOs;
using Emag.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emag.Services
{
    public class ProductService
    {
        private readonly ProductDbContext _context;
        public ProductService(ProductDbContext context)
        {
            _context = context;
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
        }
    }
}
