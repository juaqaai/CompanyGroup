using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Routable.Models;
using Orchard.Core.Routable.Services;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.UI;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;
using Szmyd.Orchard.Modules.Menu.Utilities;
using MenuItemComparer = Szmyd.Orchard.Modules.Menu.Services.MenuItemComparer;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    [OrchardFeature("Szmyd.Menu.Breadcrumbs")]
    public class BreadcrumbsPartDriver : ContentPartDriver<BreadcrumbsPart>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INavigationManager _nav;
        private readonly IAdvancedMenuService _menuService;
        private readonly INotifier _notifier;
        private readonly IPartWatcher _routable;
        private const string TemplateName = "Parts/Menu.Breadcrumbs";

        public Localizer T { get; set; }


        public BreadcrumbsPartDriver(IAdvancedMenuService menuService, INotifier notifier, IPartWatcher routable, IHttpContextAccessor httpContextAccessor, INavigationManager nav)
        {
            _menuService = menuService;
            _notifier = notifier;
            _routable = routable;
            _httpContextAccessor = httpContextAccessor;
            _nav = nav;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(BreadcrumbsPart part, string displayType, dynamic shapeHelper) {

            var menu = _nav.BuildMenu(part.MenuName);
  
            HttpRequestBase request = _httpContextAccessor.Current().Request;
            var current = _routable.Get<RoutePart>().FirstOrDefault();
            var currentContainer = current.GetContainerPath();
            IList<MenuItem> path;

            // We have to differentiate directly available items and containable items (like blog posts)
            if (currentContainer == null)
            {
                // Searching directly for an item
                path = MenuItemsUtility.SetSelectedPath(menu, request.RequestContext.RouteData, request.Path, _httpContextAccessor.Current()).ToList();
            }
            else
            {
                // Searching for the container menu item
                path = MenuItemsUtility.SetSelectedPath(menu, request.RequestContext.RouteData, "/" + currentContainer, _httpContextAccessor.Current()).ToList();
                path.Add(new MenuItem { Url = "/" + current.Path, Href = "/" + current.Path, Text = new LocalizedString(current.Title) });
            }

            if(path.FirstOrDefault() != null && path.FirstOrDefault().Href != "/") {
                path.Insert(0, new MenuItem { Url = "/", Href = "/", Text = new LocalizedString("Home") });
            }

            return ContentShape("Parts_Menu_Breadcrumbs",
                () => shapeHelper.Parts_Menu_Breadcrumbs(
                    ContentItem: part.ContentItem,
                    Path: path,
                    LastAsLink: part.LastAsLink, 
                    Separator: part.Separator, 
                    LeadingText: part.LeadingText));
        }

        protected override DriverResult Editor(BreadcrumbsPart part, dynamic shapeHelper)
        {
            part.AvailableMenus = _menuService.GetMenus().Select(m => new { Text = String.Format("Menu '{0}'", m.Name), Value = m.Name });
            return ContentShape("Parts_Menu_Breadcrumbs",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(BreadcrumbsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Breadcrumbs edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during breadcrumbs update!"));
            }
            return Editor(part, shapeHelper);
        }

        

    }
}