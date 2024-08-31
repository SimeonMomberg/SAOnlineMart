namespace SAOnlineMart.Models
{
    public class ShoppingCartDTO
    {
        public int UserId { get; set; }
        public List<ShoppingCartItemDTO> Items { get; set; }
    }

}
