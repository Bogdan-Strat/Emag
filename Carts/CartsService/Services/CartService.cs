using CartsService.DTOs;
using CartsService.Entities;
using MongoDB.Entities;

namespace CartsService.Services
{
    public class CartService
    {
        public async Task<List<Cart>> GetAllCarts()
        {
            var carts= await DB.Find<Cart>()
                .ExecuteAsync();

            return carts;
        }

        public async Task AddCart(AddCartDTO cart)
        {
            var products = await DB.Find<Product>()
                .ExecuteAsync();

            var isProductValid = products
                .Select(p => p.ProductId)
                .Contains(cart.ProductId);

            if (!isProductValid)
                return;

            await DB.InsertAsync(AddCartDTO.FromDTO(cart));
        }
    }
}
