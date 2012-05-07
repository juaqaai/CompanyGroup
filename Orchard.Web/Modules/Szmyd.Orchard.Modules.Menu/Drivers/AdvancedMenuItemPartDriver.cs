using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    public class AdvancedMenuItemPartDriver : ContentPartDriver<AdvancedMenuItemPart>
    {
        private readonly INotifier _notifier;
        private readonly IAdvancedMenuService _service;
        private readonly INavigationManager _nav;
        private const string TemplateName = "Parts/Menu.AdvancedItem";

        public Localizer T { get; set; }

        public AdvancedMenuItemPartDriver(INotifier notifier, IAdvancedMenuService service, INavigationManager nav)
        {
            _notifier = notifier;
            _service = service;
            _nav = nav;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Editor(AdvancedMenuItemPart part, dynamic shapeHelper)
        {
            if(string.IsNullOrWhiteSpace(part.Position))
                part.Position = PositionUtility.GetNext(_nav.BuildMenu(part.MenuName ?? "main"));
            return ContentShape("Parts_Menu_AdvancedItem",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(AdvancedMenuItemPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Menu edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during menu update!"));
            }
            _service.TriggerSignal();
            return Editor(part, shapeHelper);
        }
    }
}