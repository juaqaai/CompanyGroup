using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using CKEditor.Models;

namespace CKEditor {
    public class Migrations : DataMigrationImpl {
        public int Create() {
			// Creating table CKEditorSettingsPartRecord
			SchemaBuilder.CreateTable("CKEditorSettingsPartRecord", table => table
				.ContentPartRecord()
                .Column<string>("ContentsCss", c => c.WithDefault(CKEditorSettingsPartRecord.DefaultContentsCss).WithLength(255))

			);

            return 1;
        }

        public int UpdateFrom1()
        {
            // Support "extraplugins" setting
            SchemaBuilder.AlterTable("CKEditorSettingsPartRecord", table => table
                .AddColumn<string>("ExtraPlugins", c => c.WithDefault(CKEditorSettingsPartRecord.DefaultExtraPlugins).WithLength(1024)));
            SchemaBuilder.AlterTable("CKEditorSettingsPartRecord", table => table
                .AddColumn<bool>("ClearDefaultStyleSets", c => c.WithDefault(CKEditorSettingsPartRecord.DefaultClearDefaultStyleSets))
            );

            return 2;
        }
    }
}