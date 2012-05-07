using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Settings;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class ItemCounterPartDriver : ContentPartDriver<ItemCounterPart>
    {
        private readonly INotifier _notifier;
        private const string TemplateName = "Parts/Counters.ItemCounter";

        public Localizer T { get; set; }

        public ItemCounterPartDriver(INotifier notifier)
        {
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(ItemCounterPart part, string displayType, dynamic shapeHelper)
        {
            var settings = part.Settings.GetModel<ItemCounterPartTypePartSettings>();
            var type = part.Record == null ? settings.Type : part.Type;
            return ContentShape("Parts_Counters_ItemCounter",
                () => shapeHelper.Parts_Counters_ItemCounter(Type: type, ContentItem: part.ContentItem));
        }

        protected override DriverResult Editor(ItemCounterPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Counter_ItemCounter",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(ItemCounterPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Item counter edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during item counter update!"));
            }
            return Editor(part, shapeHelper);
        }

    }
}