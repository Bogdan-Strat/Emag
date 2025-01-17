using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public ICollection<OrderXProduct> OrderXProducts { get; set; } = new List<OrderXProduct>();
    }
}
