using SAOnlineMartAPI.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime OrderDate { get; set; }

    // Navigation Properties
    public User User { get; set; }
    public Product Product { get; set; }
}