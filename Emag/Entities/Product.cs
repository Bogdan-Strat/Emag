using System.ComponentModel.DataAnnotations.Schema;

namespace Emag.Entities
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }

    }
}
