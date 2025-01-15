using CartsService.Entities;

namespace CartsService.DTOs
{
    public class AddCartDTO
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public static Cart FromDTO(AddCartDTO dto, Guid userId)
        {
            return new Cart()
            {
                ProductId = dto.ProductId,
                UserId = userId,
            };
        }
    }
}
