using CartsService.Entities;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace CartsService.Consumers
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            Console.WriteLine("--> Consuming product created: " + context.Message.Id);

            var product = new Product() 
            {
                ProductId = context.Message.Id 
            };

            await product.SaveAsync();
        }
    }
}
