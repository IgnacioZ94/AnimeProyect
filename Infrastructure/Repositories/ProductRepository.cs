using Domain.Core.Interfaces;
using Entities;
using Infrastructure.Data;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MongoDbContext context) : base(context, "Products") // Collection name "Products"
        {
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _collection.Find(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}
