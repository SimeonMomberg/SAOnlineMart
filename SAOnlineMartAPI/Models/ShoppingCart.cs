using System.ComponentModel.DataAnnotations.Schema;

namespace SAOnlineMartAPI.Models
{
    [Table("ShoppingCart")]  // This ensures EF maps to the correct table
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}