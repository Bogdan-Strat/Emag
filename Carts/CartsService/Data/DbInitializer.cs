using CartsService.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CartsService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("CartsDb", MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Cart>()
                .Key(x => x.ID, KeyType.Text)
                .CreateAsync();

            var cartsNo = await DB.CountAsync<Cart>();

            if (cartsNo > 0)
                return;

            var carts = new List<Cart>()
            {
                new()
                {
                    ProductId = Guid.Parse("0baeff6f-a36d-4699-913b-e5391bcf54ab"),
                    ProductQuantity = 1,
                }
            };

            await DB.InsertAsync(carts);
        }
    }
}
