using SAOnlineMart.Models;
using SAOnlineMart.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductsDTO>> GetAllProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductsDTO>>("api/products");
    }

    public async Task<ProductsDTO> GetProductByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductsDTO>($"api/products/{id}");
    }

    public async Task CreateProductAsync(ProductsDTO product)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductAsync(ProductsDTO product)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        response.EnsureSuccessStatusCode();
    }
}
