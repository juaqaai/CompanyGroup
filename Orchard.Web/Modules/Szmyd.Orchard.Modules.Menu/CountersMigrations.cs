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
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class CountersMigrations : DataMigrationImpl
    {
        private readonly Work<IContentManager> _content;
        private readonly Work<INotifier> _notifier;

        public CountersMigrations(Work<IContentManager> content, Work<INotifier> notifier)
        {
            _content = content;
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        public Localizer T;

        public int Create()
        {
            try
            {
                SchemaBuilder.CreateTable("RecentlySeenPartRecord",
                                          table => table
                                                       .ContentPartRecord()
                                                       .Column<string>("PositiveFilterRegex", c => c.Unlimited())
                                                       .Column<string>("NegativeFilterRegex", c => c.Unlimited()));
            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping RecentlySeenPartRecord table creation. It already exists."));
            }

            ContentDefinitionManager.AlterTypeDefinition("RecentlySeenWidget",
                cfg => cfg
                    .WithPart("RecentlySeenPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );
            return 1;
        }

        public int UpdateFrom1()
        {
            /* Adding some new columns */
            try
            {
                SchemaBuilder.AlterTable("RecentlySeenPartRecord", t => t.AddColumn<int>("ItemCount", c => c.NotNull().WithDefault(5)));
                SchemaBuilder.AlterTable("RecentlySeenPartRecord", t => t.AddColumn<bool>("ShowCounts", c => c.WithDefault(false)));
                SchemaBuilder.AlterTable("RecentlySeenPartRecord", t => t.AddColumn<string>("VisitCounter", c => c.NotNull().WithDefault("PerSite")));
            }
            catch (Exception ex)
            {
                _notifier.Value.Warning(T("Skipping RecentlySeenPartRecord table alteration. It already exists."));
            }

            return 2;
        }


    }
}