
using Emag.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emag.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<ProductDbContext>());
        }

        private static void SeedData(ProductDbContext context)
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
                    Description = "Latest iphone model. 256 GB memory and 32 GB RAM", 
                    Price = 1500,
                    Category = Category.Phone,
                    Quantity = 1054
                },
                new()
                {
                    Id = Guid.Parse("bae4491d-9c92-40ed-bc29-ea2ada2a9da0"),
                    Name = "Dell Laptop",
                    Description = "15 inches, 2GB dedicated memory, 16GB of RAM, 1 TB memory, I7 processor",
                    Price = 1000,
                    Category = Category.Laptop,
                    Quantity = 657
                },
                new()
                {
                    Id = Guid.Parse("11a683f8-117c-4099-87a4-d1ae1b22ae59"),
                    Name = "Polo T-Shirt",
                    Description = "All the sizes, original not fake",
                    Price = 100,
                    Category = Category.Fashion,
                    Quantity = 11
                },
                new()
                {
                    Id = Guid.Parse("986a7789-9f2a-4ab4-aafa-30b640593e87"),
                    Name = "Kinder bueno",
                    Description = "The classic kinder bueno, but with white chocolate",
                    Price = 1.02,
                    Category = Category.Food,
                    Quantity = 196
                },
                new()
                {
                    Id = Guid.Parse("81d797f9-7808-405d-84ed-eec479ade424"),
                    Name = "Whirpool washing machine",
                    Description = "2022 model, 5 years warranty",
                    Price = 524,
                    Category = Category.Appliances,
                    Quantity = 10597
                },
                new()
                {
                    Id = Guid.Parse("d3bb6b3e-1c1f-4bfe-8477-e330eb6bc0c6"),
                    Name = "Samsung Galaxy Z-Flip",
                    Description = "The only smart phone which can fold. More resistant, better camera",
                    Price = 1481.35,
                    Category = Category.Phone,
                    Quantity = 509
                },
                new()
                {
                    Id = Guid.Parse("632c463f-650d-4807-ac52-5dd41adbe002"),
                    Name = "Curved monitor OLED 4K",
                    Description = "Latest generation 96 inch insane graphics",
                    Price = 999.99,
                    Category = Category.PC,
                    Quantity = 32
                },
            };

            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
