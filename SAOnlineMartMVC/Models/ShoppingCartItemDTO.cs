namespace SAOnlineMart.Models
{
    public class ShoppingCartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string ImageUrl { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}

