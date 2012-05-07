using System.Linq;

using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Navigation.Models;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Environment.Extensions.Models;
using Orchard.Logging;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;
using IMenuService = Orchard.Core.Navigation.Services.IMenuService;


namespace Szmyd.Orchard.Modules.Menu
{
    
    public class MenuFeatureEvents : IFeatureEventHandler
    {
        private readonly IOrchardServices _services;
        private readonly IWidgetsService _widgets;
        private readonly IMenuService _oldMenuService;
        private readonly IAdvancedMenuService _menuService;

        public MenuFeatureEvents(IOrchardServices services, IWidgetsService widgets, IMenuService oldMenuService, IAdvancedMenuService menuService)
        {
            _services = services;
            _widgets = widgets;
            _oldMenuService = oldMenuService;
            _menuService = menuService;
        }

        public ILogger Logger { get; set; }

        public void Installing(Feature feature) { }

        public void Installed(Feature feature)
        {

        }

        public void Enabling(Feature feature) { }

        public void Enabled(Feature feature)
        {
            if (feature.Descriptor.Id != "Szmyd.Orchard.Modules.Menu") return;
            var currentMainMenu = _menuService.GetMenu("main");
            if (currentMainMenu == null)
            {
                _services.ContentManager.Create<AdvancedMenuPart>("Menu", p => p.Name = "main");
            }
            var currentItems = _menuService.GetMenuItems("main");
            /* Creating new menu with Home in it */
            if (!currentItems.Any())
            {
                _services.ContentManager.Create<AdvancedMenuItemPart>("SimpleMenuItem",
                    nItem =>
                    {
                        nItem.DisplayHref = true;
                        nItem.DisplayText = true;
                        nItem.MenuName = "main";
                        nItem.Url = "/";
                        nItem.Position = "0";
                        nItem.Text = "Home";
                    });
            }

            // Adding the default menu widget to Navigation zone in Default layer
            var layerPart = _widgets.GetLayers().FirstOrDefault(l => l.Name == "Default");
            if (layerPart == null) {
                return;
            }
            var countDefault = _services.ContentManager
                .Query<WidgetPart, WidgetPartRecord>()
                .Where(p => p.Zone == "Navigation")
                .ForType("MenuWidget").List();
            if (countDefault.Count() == 0)
            {
                _services.ContentManager.Create<WidgetPart>("StyledMenuWidget",
                                                            widget =>
                                                            {
                                                                widget.Record.Title = "Main menu";
                                                                widget.Record.Position = "0";
                                                                widget.Record.Zone = "Navigation";
                                                                widget.LayerPart = layerPart;
                                                                widget.As<MenuWidgetPart>().Levels = 0;
                                                                widget.As<MenuWidgetPart>().CutOrFlattenLower = false;
                                                                widget.As<MenuWidgetPart>().MenuName = "main";
                                                                widget.As<MenuWidgetPart>().WrapChildrenInDiv = false;
                                                                widget.As<MenuWidgetPart>().Mode = MenuWidgetMode.AllItems;
                                                                widget.As<MenuStylingPart>().BackColor = "#FFFFFF";
                                                                widget.As<MenuStylingPart>().ForeColor = "#333333";
                                                                widget.As<MenuStylingPart>().HoverBackColor = "#F7F7F7";
                                                                widget.As<MenuStylingPart>().HoverForeColor = "#444444";
                                                                widget.As<MenuStylingPart>().SelectedForeColor = "#333333";
                                                                widget.As<MenuStylingPart>().SelectedBackColor = "#F1F1F1";
                                                                widget.As<MenuStylingPart>().Style = MenuStyles.SuperfishHorizontal;
                                                            });
            }
        }

        public void Disabling(Feature feature) { }

        public void Disabled(Feature feature) { }

        public void Uninstalling(Feature feature) { }

        public void Uninstalled(Feature feature) { }
    }
}