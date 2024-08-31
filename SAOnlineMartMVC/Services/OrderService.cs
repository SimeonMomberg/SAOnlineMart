using SAOnlineMart.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderAsync(int userId, IEnumerable<ShoppingCartItemDTO> cartItems, string shippingAddress, string paymentMethod)
        {
            foreach (var item in cartItems)
            {
                // Create an order for each item
                var orderDTO = new OrderCreateDto
                {
                    UserId = userId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ShippingAddress = shippingAddress,
                    PaymentMethod = paymentMethod,
                    OrderDate = DateTime.Now
                };

                // Send the order to the API
                var response = await _httpClient.PostAsJsonAsync("api/orders", orderDTO);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
