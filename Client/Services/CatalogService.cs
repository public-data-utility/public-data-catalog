using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Pdu.Catalog.Models;

namespace Pdu.Catalog.Services
{
    public class CatalogService : ServiceBase, ICatalogService, IService
    {
        public CatalogService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Catalog");

        public async Task<List<Models.Catalog>> GetCatalogsAsync(int ModuleId)
        {
            List<Models.Catalog> Catalogs = await GetJsonAsync<List<Models.Catalog>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId));
            return Catalogs.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Catalog> GetCatalogAsync(int CatalogId, int ModuleId)
        {
            return await GetJsonAsync<Models.Catalog>(CreateAuthorizationPolicyUrl($"{Apiurl}/{CatalogId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Catalog> AddCatalogAsync(Models.Catalog Catalog)
        {
            return await PostJsonAsync<Models.Catalog>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Catalog.ModuleId), Catalog);
        }

        public async Task<Models.Catalog> UpdateCatalogAsync(Models.Catalog Catalog)
        {
            return await PutJsonAsync<Models.Catalog>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Catalog.CatalogId}", EntityNames.Module, Catalog.ModuleId), Catalog);
        }

        public async Task DeleteCatalogAsync(int CatalogId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{CatalogId}", EntityNames.Module, ModuleId));
        }
    }
}
