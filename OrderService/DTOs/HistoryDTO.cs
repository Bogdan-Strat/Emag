using OrderService.Entities;

namespace OrderService.DTOs
{
    public class HistoryDTO
    {
        public Guid OrderId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
