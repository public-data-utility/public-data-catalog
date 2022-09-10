using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Pdu.Catalog.Migrations.EntityBuilders
{
    public class CatalogEntityBuilder : AuditableBaseEntityBuilder<CatalogEntityBuilder>
    {
        private const string _entityTableName = "PduCatalog";
        private readonly PrimaryKey<CatalogEntityBuilder> _primaryKey = new("PK_PduCatalog", x => x.CatalogId);
        private readonly ForeignKey<CatalogEntityBuilder> _moduleForeignKey = new("FK_PduCatalog_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public CatalogEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override CatalogEntityBuilder BuildTable(ColumnsBuilder table)
        {
            CatalogId = AddAutoIncrementColumn(table,"CatalogId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> CatalogId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
