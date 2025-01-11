using Emag.Entities;

namespace Emag.DTOs
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        public static Product FromDTO(AddProductDTO dto)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                Category = (Category)dto.CategoryId,
            };
        }
    }
}
