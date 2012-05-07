﻿using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Orchard.Core.Containers {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable("ContainerPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<bool>("Paginated")
                    .Column<int>("PageSize")
                    .Column<string>("OrderByProperty")
                    .Column<int>("OrderByDirection"));

            SchemaBuilder.CreateTable("ContainerWidgetPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("ContainerId")
                    .Column<int>("PageSize")
                    .Column<string>("OrderByProperty")
                    .Column<int>("OrderByDirection")
                    .Column<bool>("ApplyFilter")
                    .Column<string>("FilterByProperty")
                    .Column<string>("FilterByOperator")
                    .Column<string>("FilterByValue"));

            SchemaBuilder.CreateTable("CustomPropertiesPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("CustomOne")
                    .Column<string>("CustomTwo")
                    .Column<string>("CustomThree"));

            ContentDefinitionManager.AlterTypeDefinition("ContainerWidget",
                cfg => cfg
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithPart("ContainerWidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterPartDefinition("ContainerPart", builder => builder.Attachable());
            ContentDefinitionManager.AlterPartDefinition("ContainablePart", builder => builder.Attachable());
            ContentDefinitionManager.AlterPartDefinition("CustomPropertiesPart", builder => builder.Attachable());
 
            return 1;
        }

        public int UpdateFrom1() {
            SchemaBuilder.AlterTable("ContainerPartRecord", table => table.AddColumn<string>("ItemContentType"));
            return 2;
        }
    }
}