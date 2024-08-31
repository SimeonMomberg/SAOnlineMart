using SAOnlineMart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAOnlineMart.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int userId, IEnumerable<ShoppingCartItemDTO> cartItems, string shippingAddress, string paymentMethod);
    }
}
