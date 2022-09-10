using System.Collections.Generic;
using System.Threading.Tasks;
using Pdu.Catalog.Models;

namespace Pdu.Catalog.Services
{
    public interface ICatalogService 
    {
        Task<List<Models.Catalog>> GetCatalogsAsync(int ModuleId);

        Task<Models.Catalog> GetCatalogAsync(int CatalogId, int ModuleId);

        Task<Models.Catalog> AddCatalogAsync(Models.Catalog Catalog);

        Task<Models.Catalog> UpdateCatalogAsync(Models.Catalog Catalog);

        Task DeleteCatalogAsync(int CatalogId, int ModuleId);
    }
}
