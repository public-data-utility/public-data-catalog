using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Pdu.Catalog.Models;

namespace Pdu.Catalog.Repository
{
    public class CatalogRepository : ICatalogRepository, ITransientService
    {
        private readonly CatalogContext _db;

        public CatalogRepository(CatalogContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.Catalog> GetCatalogs(int ModuleId)
        {
            return _db.Catalog.Where(item => item.ModuleId == ModuleId);
        }

        public Models.Catalog GetCatalog(int CatalogId)
        {
            return GetCatalog(CatalogId, true);
        }

        public Models.Catalog GetCatalog(int CatalogId, bool tracking)
        {
            if (tracking)
            {
                return _db.Catalog.Find(CatalogId);
            }
            else
            {
                return _db.Catalog.AsNoTracking().FirstOrDefault(item => item.CatalogId == CatalogId);
            }
        }

        public Models.Catalog AddCatalog(Models.Catalog Catalog)
        {
            _db.Catalog.Add(Catalog);
            _db.SaveChanges();
            return Catalog;
        }

        public Models.Catalog UpdateCatalog(Models.Catalog Catalog)
        {
            _db.Entry(Catalog).State = EntityState.Modified;
            _db.SaveChanges();
            return Catalog;
        }

        public void DeleteCatalog(int CatalogId)
        {
            Models.Catalog Catalog = _db.Catalog.Find(CatalogId);
            _db.Catalog.Remove(Catalog);
            _db.SaveChanges();
        }
    }
}
