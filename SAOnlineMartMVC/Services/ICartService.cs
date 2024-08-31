using SAOnlineMart.Models;
using System.Threading.Tasks;

namespace SAOnlineMart.Services
{
    public interface ICartService
    {
        Task<ShoppingCartDTO> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, ShoppingCartItemDTO item);
        Task RemoveFromCartAsync(int userId, int productId);
        Task UpdateCartAsync(int userId, ShoppingCartItemDTO item);
        Task ClearCartAsync(int userId);
    }
}
