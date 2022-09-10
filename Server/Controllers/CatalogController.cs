using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Pdu.Catalog.Repository;
using Oqtane.Controllers;
using System.Net;

namespace Pdu.Catalog.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class CatalogController : ModuleControllerBase
    {
        private readonly ICatalogRepository _CatalogRepository;

        public CatalogController(ICatalogRepository CatalogRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _CatalogRepository = CatalogRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Catalog> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && ModuleId == AuthEntityId(EntityNames.Module))
            {
                return _CatalogRepository.GetCatalogs(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Catalog Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Catalog Get(int id)
        {
            Models.Catalog Catalog = _CatalogRepository.GetCatalog(id);
            if (Catalog != null && Catalog.ModuleId == AuthEntityId(EntityNames.Module))
            {
                return Catalog;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Catalog Get Attempt {CatalogId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Catalog Post([FromBody] Models.Catalog Catalog)
        {
            if (ModelState.IsValid && Catalog.ModuleId == AuthEntityId(EntityNames.Module))
            {
                Catalog = _CatalogRepository.AddCatalog(Catalog);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Catalog Added {Catalog}", Catalog);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Catalog Post Attempt {Catalog}", Catalog);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Catalog = null;
            }
            return Catalog;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Catalog Put(int id, [FromBody] Models.Catalog Catalog)
        {
            if (ModelState.IsValid && Catalog.ModuleId == AuthEntityId(EntityNames.Module) && _CatalogRepository.GetCatalog(Catalog.CatalogId, false) != null)
            {
                Catalog = _CatalogRepository.UpdateCatalog(Catalog);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Catalog Updated {Catalog}", Catalog);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Catalog Put Attempt {Catalog}", Catalog);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Catalog = null;
            }
            return Catalog;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Catalog Catalog = _CatalogRepository.GetCatalog(id);
            if (Catalog != null && Catalog.ModuleId == AuthEntityId(EntityNames.Module))
            {
                _CatalogRepository.DeleteCatalog(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Catalog Deleted {CatalogId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Catalog Delete Attempt {CatalogId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
