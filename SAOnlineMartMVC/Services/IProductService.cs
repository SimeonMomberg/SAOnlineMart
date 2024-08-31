using SAOnlineMart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAOnlineMart.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDTO>> GetAllProductsAsync();
        Task<ProductsDTO> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductsDTO product);
        Task UpdateProductAsync(ProductsDTO product);
        Task DeleteProductAsync(int id);
    }
}
