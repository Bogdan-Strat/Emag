using CartsService.Entities;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace CartsService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine("--> Consuming order created: ");

            var carts = await DB.Find<Cart>()
                .Match(c => c.UserId == context.Message.UserId)
                .ExecuteAsync();

            for (var i = 0; i < carts.Count; ++i)
            {
                await DB.DeleteAsync<Cart>(carts[i].ID);
            }

        }
    }
}
