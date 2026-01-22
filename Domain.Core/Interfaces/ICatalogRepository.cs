using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        // Base repository methods are inherited: GetAll, GetById, Create, Update, Delete
        // GetByNameAsync removed as per requirements
    }
}
