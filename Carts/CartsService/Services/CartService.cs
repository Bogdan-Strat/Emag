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

            return carts ;
        }

        public async Task AddCart(AddCartDTO cart)
        {
            await DB.InsertAsync(AddCartDTO.FromDTO(cart));
        }
    }
}
