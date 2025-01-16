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
                        ProductId = Guid.Parse("11a683f8-117c-4099-87a4-d1ae1b22ae59")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("632c463f-650d-4807-ac52-5dd41adbe002")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("81d797f9-7808-405d-84ed-eec479ade424")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("986a7789-9f2a-4ab4-aafa-30b640593e87")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("9d42e31d-7399-494e-9cda-deef0f668498")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("bae4491d-9c92-40ed-bc29-ea2ada2a9da0")
                    },
                    new()
                    {
                        ProductId = Guid.Parse("d3bb6b3e-1c1f-4bfe-8477-e330eb6bc0c6")
                    },
                };

                await DB.InsertAsync(products);
            }
        }
    }
}
