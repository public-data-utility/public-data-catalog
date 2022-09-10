using System.Collections.Generic;
using Pdu.Catalog.Models;

namespace Pdu.Catalog.Repository
{
    public interface ICatalogRepository
    {
        IEnumerable<Models.Catalog> GetCatalogs(int ModuleId);
        Models.Catalog GetCatalog(int CatalogId);
        Models.Catalog GetCatalog(int CatalogId, bool tracking);
        Models.Catalog AddCatalog(Models.Catalog Catalog);
        Models.Catalog UpdateCatalog(Models.Catalog Catalog);
        void DeleteCatalog(int CatalogId);
    }
}
