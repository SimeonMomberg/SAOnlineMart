namespace SAOnlineMartAPI.Models
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
    }
}