
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

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();
            var id5 = Guid.NewGuid();
            var id6 = Guid.NewGuid();
            var id7 = Guid.NewGuid();

            var products = new List<Product>()
            {
                new()
                {
                    Id = id1,
                    Name = "Iphone 16",
                    Description = "Latest iphone model. 256 GB memory and 32 GB RAM", 
                    Price = 1500,
                    Category = Category.Phone,
                    Quantity = 1054
                },
                new()
                {
                    Id = id2,
                    Name = "Dell Laptop",
                    Description = "15 inches, 2GB dedicated memory, 16GB of RAM, 1 TB memory, I7 processor",
                    Price = 1000,
                    Category = Category.Laptop,
                    Quantity = 657
                },
                new()
                {
                    Id = id3,
                    Name = "Polo T-Shirt",
                    Description = "All the sizes, original not fake",
                    Price = 100,
                    Category = Category.Fashion,
                    Quantity = 11
                },
                new()
                {
                    Id = id4,
                    Name = "Kinder bueno",
                    Description = "The classic kinder bueno, but with white chocolate",
                    Price = 1.02,
                    Category = Category.Food,
                    Quantity = 196
                },
                new()
                {
                    Id = id5,
                    Name = "Whirpool washing machine",
                    Description = "2022 model, 5 years warranty",
                    Price = 524,
                    Category = Category.Appliances,
                    Quantity = 10597
                },
                new()
                {
                    Id = id6,
                    Name = "Samsung Galaxy Z-Flip",
                    Description = "The only smart phone which can fold. More resistant, better camera",
                    Price = 1481.35,
                    Category = Category.Phone,
                    Quantity = 509
                },
                new()
                {
                    Id = id7,
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
