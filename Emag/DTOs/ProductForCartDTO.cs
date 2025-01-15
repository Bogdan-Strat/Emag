using Emag.Entities;

namespace Emag.DTOs
{
    public class ProductForCartDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public static ProductForCartDTO FromEntity(Product product)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
        }
    }
}
