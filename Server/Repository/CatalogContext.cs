using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Pdu.Catalog.Repository
{
    public class CatalogContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Catalog> Catalog { get; set; }

        public CatalogContext(ITenantManager tenantManager, IHttpContextAccessor accessor) : base(tenantManager, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
