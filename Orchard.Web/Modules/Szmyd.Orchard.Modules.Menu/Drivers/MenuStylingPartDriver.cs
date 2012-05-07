
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    
    public class MenuStylingPartDriver : ContentPartDriver<MenuStylingPart>
    {
        private readonly INotifier _notifier;
        private const string TemplateName = "Parts/Menu.Styling";

        public Localizer T { get; set; }

        public MenuStylingPartDriver(INotifier notifier)
        {
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(MenuStylingPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Menu_Styling",
                () => shapeHelper.Parts_Menu_Styling(ContentPart: part));
        }

        protected override DriverResult Editor(MenuStylingPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Menu_Styling",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(MenuStylingPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Menu styling edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during item counter update!"));
            }
            return Editor(part, shapeHelper);
        }

    }
}