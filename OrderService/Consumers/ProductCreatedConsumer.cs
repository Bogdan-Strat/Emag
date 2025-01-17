using Contracts;
using MassTransit;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Consumers
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly OrderDbContext _db;

        public ProductCreatedConsumer(OrderDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            Console.WriteLine("--> Consuming product created: " + context.Message.Id);

            var product = new Product()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Price = context.Message.Price,
                Quantity = context.Message.Quantity,
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }
    }
}
