using CartsService.Entities;

namespace CartsService.DTOs
{
    public class AddCartDTO
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public static Cart FromDTO(AddCartDTO dto)
        {
            return new Cart()
            {
                ProductId = dto.ProductId,
                ProductQuantity = dto.ProductQuantity,
            };
        }
    }
}
