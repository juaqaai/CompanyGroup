using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    public class AdvancedMenuPartDriver : ContentPartDriver<AdvancedMenuPart>
    {
        private readonly INotifier _notifier;
        private readonly IAdvancedMenuService _service;
        private const string TemplateName = "Parts/Menu.AdvancedMenu";

        public Localizer T { get; set; }

        public AdvancedMenuPartDriver(INotifier notifier, IAdvancedMenuService service)
        {
            _notifier = notifier;
            _service = service;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Editor(AdvancedMenuPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Menu_AdvancedMenu",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(AdvancedMenuPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            part.Items = _service.GetMenuItems(part.Name);
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