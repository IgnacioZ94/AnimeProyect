using Entities;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        // Add specific methods if needed, e.g.
        // Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<Product> GetByNameAsync(string name);
    }
}
