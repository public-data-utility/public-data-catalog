using Oqtane.Models;
using Oqtane.Modules;

namespace Pdu.Catalog
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Catalog",
            Description = "Catalog",
            Version = "1.0.0",
            ServerManagerType = "Pdu.Catalog.Manager.CatalogManager, Pdu.Catalog.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Pdu.Catalog.Shared.Oqtane",
            PackageName = "Pdu.Catalog" 
        };
    }
}
