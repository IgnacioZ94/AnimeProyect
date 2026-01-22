using Domain.Core.Interfaces;
using Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CatalogRepository : BaseRepository<Catalog>, ICatalogRepository
    {
        private readonly ILogger<CatalogRepository> _logger;

        public CatalogRepository(MongoDbContext context, ILogger<CatalogRepository> logger) 
            : base(context, "Catalog")
        {
            _logger = logger;
            _logger.LogInformation("CatalogRepository initialized with collection: Catalog");
        }
    }
}
