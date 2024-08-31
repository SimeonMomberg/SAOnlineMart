using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SAOnlineMart.Models;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using SAOnlineMart.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CartService> _logger;

    public CartService(HttpClient httpClient, ILogger<CartService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ShoppingCartDTO> GetCartAsync(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetCart");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var cartDto = JObject.Parse(responseData).ToObject<ShoppingCartDTO>();
                return cartDto;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to get cart: {StatusCode}, {ErrorMessage}", response.StatusCode, errorMessage);
                throw new HttpRequestException($"Failed to get cart. Status code: {response.StatusCode}. Error: {errorMessage}");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while getting the cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task AddToCartAsync(int userId, ShoppingCartItemDTO item)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/ShoppingCart/{userId}/add", item);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while adding an item to the cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{userId}/remove/{productId}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while removing an item from the cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task UpdateCartAsync(int userId, ShoppingCartItemDTO item)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ShoppingCart/{userId}/update", item);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while updating the cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task ClearCartAsync(int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{userId}/clear");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while clearing the cart for user {UserId}", userId);
            throw;
        }
    }
}
