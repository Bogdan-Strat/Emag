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

            await DB.Index<Product>()
                .Key(x => x.ProductId, KeyType.Text)
                .CreateAsync();

            var cartsNo = await DB.CountAsync<Cart>();
            var productsNo = await DB.CountAsync<Product>();

            if (productsNo == 0)
            {
                var products = new List<Product>()
                {
                    new()
                    {
                        ProductId = Guid.Parse("0baeff6f-a36d-4699-913b-e5391bcf54ab")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("842fc045-f1f2-4392-8d1b-d403df1d4de1")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("d16784b9-ee83-4b7f-8008-d8e56ad551fc")
                    },
                };

                await DB.InsertAsync(products);
            }
        }
    }
}
