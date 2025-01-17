using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.DTOs;
using OrderService.Entities;

namespace OrderService.Services
{
    public class OrderServices
    {
        private readonly OrderDbContext _db;
        private readonly CurrentUserDTO _currentUser;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderServices(OrderDbContext db, CurrentUserDTO currentUser, IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _currentUser = currentUser;
            _publishEndpoint = publishEndpoint;
        }

        public async Task AddOrder(List<AddOrderDTO> dtos)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = _currentUser.Id,
                Date = DateTime.UtcNow.AddHours(2),
                Price = dtos.Sum(dto => dto.Price * dto.Quantity),
            };

            _db.Orders.Add(order);

            foreach (var dto in dtos)
            {
                var orderXProduct = new OrderXProduct
                {
                    OrderId = order.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                order.OrderXProducts.Add(orderXProduct);
            }

            await _db.SaveChangesAsync();

            var orderCreated = new OrderCreated()
            {
                UserId = _currentUser.Id,
            };

            await _publishEndpoint.Publish(orderCreated);
        }

        public async Task<List<HistoryDTO>> GetHistory()
        {
            var orders =  await _db.Orders
                .Include(o => o.OrderXProducts)
                .Where(o => o.UserId == _currentUser.Id)
                .Select(o => new HistoryDTO
                {
                    OrderId = o.Id,
                    Price = (double)o.Price,
                    Date = o.Date,
                    Products = o.OrderXProducts.Select(oxp => new Product
                    {
                        Id = oxp.ProductId,
                        Price = oxp.Product.Price,
                        Name = oxp.Product.Name,
                        Quantity = oxp.Quantity
                    }).ToList()
                })
                .OrderByDescending(o => o.Date)
                .ToListAsync();

            return orders;
        }
    }
}
