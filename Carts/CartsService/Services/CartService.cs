using CartsService.DTOs;
using CartsService.Entities;
using Contracts;
using MongoDB.Entities;

namespace CartsService.Services
{
    public class CartService
    {
        private readonly CurrentUserDTO _currentUser;
        public CartService(CurrentUserDTO currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            var carts= await DB.Find<Cart>()
                .ExecuteAsync();

            return carts;
        }

        public async Task AddProductToCart(AddCartDTO cart)
        {
            var products = await DB.Find<Product>()
                .ExecuteAsync();

            var isProductValid = products
                .Select(p => p.ProductId)
                .Contains(cart.ProductId);

            if (!isProductValid)
                return;

            await DB.InsertAsync(AddCartDTO.FromDTO(cart, _currentUser.Id));
        }

        public async Task RemoveProductFromCart(AddCartDTO dto)
        {
            var cartToDelete = await DB.Find<Cart>()
                .Match(c => c.ProductId == dto.ProductId && c.UserId == _currentUser.Id)
                .ExecuteFirstAsync();


            if (cartToDelete == null) return;

            await DB.DeleteAsync<Cart>(cartToDelete.ID);
        }
    }
}
