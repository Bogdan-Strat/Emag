using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities
{
    [Table("Orders")]
    public class Order
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }

        public ICollection<OrderXProduct> OrderXProducts { get; set; } = new List<OrderXProduct>();
    }
}
