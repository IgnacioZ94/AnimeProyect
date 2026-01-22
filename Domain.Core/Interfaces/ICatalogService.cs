using Domain.Core.DTOs;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<Catalog>> GetCatalogsAsync();
        Task<Catalog> GetCatalogAsync(string id);
        Task CreateCatalogAsync(CatalogRequest catalogRequest);
        Task UpdateCatalogAsync(string id, CatalogRequest catalogRequest);
        Task DeleteCatalogAsync(string id);
    }
}
