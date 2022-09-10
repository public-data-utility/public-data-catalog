using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using Pdu.Catalog.Repository;

namespace Pdu.Catalog.Manager
{
    public class CatalogManager : MigratableModuleBase, IInstallable, IPortable
    {
        private ICatalogRepository _CatalogRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IHttpContextAccessor _accessor;

        public CatalogManager(ICatalogRepository CatalogRepository, ITenantManager tenantManager, IHttpContextAccessor accessor)
        {
            _CatalogRepository = CatalogRepository;
            _tenantManager = tenantManager;
            _accessor = accessor;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new CatalogContext(_tenantManager, _accessor), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new CatalogContext(_tenantManager, _accessor), tenant, MigrationType.Down);
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.Catalog> Catalogs = _CatalogRepository.GetCatalogs(module.ModuleId).ToList();
            if (Catalogs != null)
            {
                content = JsonSerializer.Serialize(Catalogs);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.Catalog> Catalogs = null;
            if (!string.IsNullOrEmpty(content))
            {
                Catalogs = JsonSerializer.Deserialize<List<Models.Catalog>>(content);
            }
            if (Catalogs != null)
            {
                foreach(var Catalog in Catalogs)
                {
                    _CatalogRepository.AddCatalog(new Models.Catalog { ModuleId = module.ModuleId, Name = Catalog.Name });
                }
            }
        }
    }
}