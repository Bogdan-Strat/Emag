using Microsoft.EntityFrameworkCore;
using OrderService.Entities;

namespace OrderService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<OrderDbContext>());
        }

        private static void SeedData(OrderDbContext context)
        {
            context.Database.Migrate();

            if (context.Products.Any())
                return;

            var products = new List<Product>()
            {
                new()
                {
                    Id = Guid.Parse("9d42e31d-7399-494e-9cda-deef0f668498"),
                    Name = "Iphone 16",
                    Price = 1500,
                    Quantity = 1054
                },
                new()
                {
                    Id = Guid.Parse("bae4491d-9c92-40ed-bc29-ea2ada2a9da0"),
                    Name = "Dell Laptop",
                    Price = 1000,
                    Quantity = 657
                },
                new()
                {
                    Id = Guid.Parse("11a683f8-117c-4099-87a4-d1ae1b22ae59"),
                    Name = "Polo T-Shirt",
                    Price = 100,
                    Quantity = 11
                },
                new()
                {
                    Id = Guid.Parse("986a7789-9f2a-4ab4-aafa-30b640593e87"),
                    Name = "Kinder bueno",
                    Price = 1.02,
                    Quantity = 196
                },
                new()
                {
                    Id = Guid.Parse("81d797f9-7808-405d-84ed-eec479ade424"),
                    Name = "Whirpool washing machine",
                    Price = 524,
                    Quantity = 10597
                },
                new()
                {
                    Id = Guid.Parse("d3bb6b3e-1c1f-4bfe-8477-e330eb6bc0c6"),
                    Name = "Samsung Galaxy Z-Flip",
                    Price = 1481.35,
                    Quantity = 509
                },
                new()
                {
                    Id = Guid.Parse("632c463f-650d-4807-ac52-5dd41adbe002"),
                    Name = "Curved monitor OLED 4K",
                    Price = 999.99,
                    Quantity = 32
                },
            };

            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
