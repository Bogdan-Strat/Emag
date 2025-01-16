using Emag.Entities;

namespace Emag.DTOs
{
    public class GetProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public static GetProductDTO FromEntity(Product entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Category = entity.Category.ToString(),
            };

        }
    }
}
