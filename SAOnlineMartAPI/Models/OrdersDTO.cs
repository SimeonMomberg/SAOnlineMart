namespace SAOnlineMartAPI.Models
{
    // DTO for creating an order
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    // DTO for deleting an order (typically only need the OrderId)
    public class OrderDeleteDto
    {
        public int OrderId { get; set; }
    }
}