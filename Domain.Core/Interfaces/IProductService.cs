using Domain.Core.DTOs;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(string id);
        Task CreateProductAsync(ProductRequest productRequest);
        Task UpdateProductAsync(string id, ProductRequest productRequest);
        Task DeleteProductAsync(string id);
    }
}
