using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities
{
    [Table("OrderXProducts")]
    public class OrderXProduct
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
