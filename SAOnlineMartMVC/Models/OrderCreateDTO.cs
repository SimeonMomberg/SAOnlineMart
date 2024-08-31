using System;

namespace SAOnlineMart.Models
{

    public class OrderCreateDto
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OrderDate { get; set; }
}


public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order Order { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OrderDate { get; set; }
    public Product Product { get; set; }
}
}

