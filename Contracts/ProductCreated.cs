namespace Contracts
{
    public class ProductCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
