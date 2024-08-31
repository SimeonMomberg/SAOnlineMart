namespace SAOnlineMartAPI.Models
{
    public class ShoppingCartItemDto // Ensure this matches the name in ShoppingCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ShoppingCartDto
    {
        public int UserId { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } // Ensure the name matches
    }
}