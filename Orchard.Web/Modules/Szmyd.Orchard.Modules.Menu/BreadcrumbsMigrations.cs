using System;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Notify;

namespace Szmyd.Orchard.Modules.Menu
{
    [OrchardFeature("Szmyd.Menu.Breadcrumbs")]
    public class BreadcrumbsMigrations : DataMigrationImpl {
        private readonly Work<IContentManager> _content;
        private readonly Work<INotifier> _notifier;

        public BreadcrumbsMigrations(Work<IContentManager> content, Work<INotifier> notifier) {
            _content = content;
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        public Localizer T;

        public int Create() {
            try {
                SchemaBuilder.CreateTable("BreadcrumbsPartRecord",
                          table => table
                                       .ContentPartRecord()
                                       .Column<bool>("LastAsLink")
                                       .Column<string>("Separator")
                                       .Column<string>("LeadingText"));

            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping BreadcrumbsPartRecord table creation. It already exists."));
            }
                ContentDefinitionManager.AlterTypeDefinition("BreadcrumbsWidget",
                    cfg => cfg
                        .WithPart("BreadcrumbsPart")
                        .WithPart("CommonPart")
                        .WithPart("WidgetPart")
                        .WithSetting("Stereotype", "Widget")
                    );
            

            return 1;
        }

        public int UpdateFrom1()
        {
            /* Adding some new columns */
            try {
                SchemaBuilder.AlterTable("BreadcrumbsPartRecord", t => t.AddColumn<string>("MenuName", c => c.WithDefault("main")));
            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping BreadcrumbsPartRecord table alteration. It already exists."));
            }

            /* Record for storing item counters */
            try
            {
                SchemaBuilder.CreateTable("CounterRecord", table => table
                    .Column<int>("Id", c => c.PrimaryKey().Identity())
                    .Column<int>("ForItemId", c => c.NotNull())
                    .Column<int>("InContextOfItemId", c => c.NotNull())
                    .Column<int>("Count", c => c.NotNull().WithDefault(0))
                    .Column<string>("Type", c => c.WithDefault("Visits"))
                    .Column<DateTime>("LastModified"));
            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping CounterRecord table creation. It already exists."));
            }

            /* Record for holding item counter parts */
            try {
                SchemaBuilder.CreateTable("ItemCounterPartRecord", table => table
                                                                                .ContentPartRecord()
                                                                                .Column<int>("Count", c => c.WithDefault(0))
                                                                                .Column<string>("Type", c => c.WithDefault("PerSite")));
            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping ItemCounterPartRecord table creation. It already exists."));
            }

            /* Allow the counter part to attach to any content item */
            ContentDefinitionManager.AlterPartDefinition("ItemCounterPart", builder => builder.Attachable());
            return 2;
        }
    }
}