using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.Models;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.UI;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;
using Szmyd.Orchard.Modules.Menu.Settings;
using Szmyd.Orchard.Modules.Menu.Utilities;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    public class MenuWidgetPartDriver : ContentPartDriver<MenuWidgetPart>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INavigationManager _nav;
        private readonly IAdvancedMenuService _menuService;
        private readonly INotifier _notifier;
        private const string TemplateName = "Parts/Menu.Widget";

        public Localizer T { get; set; }


        public MenuWidgetPartDriver(IAdvancedMenuService menuService, INotifier notifier, IHttpContextAccessor httpContextAccessor, INavigationManager nav)
        {
            _menuService = menuService;
            _notifier = notifier;
            _httpContextAccessor = httpContextAccessor;
            _nav = nav;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(MenuWidgetPart part, string displayType, dynamic shapeHelper)
        {
            var menu = _nav.BuildMenu(part.MenuName);

            HttpRequestBase request = _httpContextAccessor.Current().Request;
            IEnumerable<MenuItem> items = menu;

            // Set the currently selected path
            MenuItemsUtility.SetSelectedPath(items,
                _httpContextAccessor.Current().Request.RequestContext.RouteData,
                _httpContextAccessor.Current().Request.Path,
                _httpContextAccessor.Current());

            if (!string.IsNullOrWhiteSpace(part.RootNode)) {
                var item = FindItemByPosition(menu, part.RootNode);
                if (item != null)
                    items = item.Items.OrderBy(m => m.Position, new FlatPositionComparer()).ToList();
            }
            else {
                switch (part.Mode)
                {
                    case MenuWidgetMode.AllItems:
                        break;
                    case MenuWidgetMode.ChildrenOnly:
                        var item = MenuItemsUtility.GetItemByUrl(menu, request.RequestContext.RouteData, request.Path, _httpContextAccessor.Current());
                        items = item != null ? item.Items.OrderBy(m => m.Position, new FlatPositionComparer()).ToList() : Enumerable.Empty<MenuItem>();
                        break;
                    case MenuWidgetMode.SiblingsOnly:
                        var childItem = MenuItemsUtility.GetItemByUrl(menu, request.RequestContext.RouteData, request.Path, _httpContextAccessor.Current());
                        var parent = MenuItemsUtility.GetParent(menu, childItem);
                        if (parent != null)
                            items = parent.Items.OrderBy(m => m.Position, new FlatPositionComparer()).ToList();
                        break;
                    case MenuWidgetMode.SiblingsExpanded:
                        CollapseNonSiblings(items, MenuItemsUtility.GetItemByUrl(menu, request.RequestContext.RouteData, request.Path, _httpContextAccessor.Current()));
                        break;
                }
            }

            // Id passed as string for future changes (eg. to guid/sth different from raw item id)
            dynamic menuShape = shapeHelper.Menu().MenuName(part.MenuName).ItemId(part.ContentItem.Id.ToString());
            PopulateMenu(shapeHelper, menuShape, menuShape, items, _menuService.GetMenuItems(part.MenuName).ToDictionary(k => k.Id), part, 1);

            // Add any know image sets to the main nav
            IEnumerable<string> menuImageSets = _nav.BuildImageSets(part.MenuName);
            if (menuImageSets != null && menuImageSets.Count() > 0)
                menuShape.ImageSets(menuImageSets);

            dynamic partShape = shapeHelper.Parts_Menu_Widget().Add(menuShape);

            return ContentShape("Parts_Menu_Widget", () => partShape);

        }

        protected override DriverResult Editor(MenuWidgetPart part, dynamic shapeHelper)
        {
            // Setting the global default menu name
            var settings = part.Settings.GetModel<MenuWidgetPartTypePartSettings>();
            if (string.IsNullOrWhiteSpace(part.MenuName))
                part.MenuName = settings.MenuName;

            part.AvailableMenus = _menuService.GetMenus().Select(m => new {Text = String.Format("Menu '{0}'", m.Name), Value = m.Name});
            part.AvailableModes = Enum.GetValues(typeof(MenuWidgetMode))
                .Cast<int>()
                .Select(i =>
                    new
                    {
                        Text = Enum.GetName(typeof(MenuWidgetMode), i),
                        Value = i
                    });

            return ContentShape("Parts_Menu_Widget",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(MenuWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Menu widget edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during menu widget update!"));
            }
            return Editor(part, shapeHelper);
        }

        internal static MenuItem FindItem(IEnumerable<MenuItem> items, string url)
        {

            if (items != null && items.Any())
            {
                var found = items.Where(i => i.Href.TrimEnd('/').ToUpperInvariant() == url.ToUpperInvariant()).FirstOrDefault();
                return found ?? items.Select(item => FindItem(item.Items, url)).FirstOrDefault(iFound => iFound != null);
            }

            return null;
        }

        internal static MenuItem FindItemByPosition(IEnumerable<MenuItem> items, string position)
        {
            if (items != null && items.Any())
            {
                var found = items.Where(i => i.Position == position).FirstOrDefault();
                return found ?? items.Select(item => FindItemByPosition(item.Items, position)).FirstOrDefault(iFound => iFound != null);
            }

            return null;
        }

        /// <summary>
        /// Populates the menu shapes.
        /// </summary>
        /// <param name="shapeFactory">The shape factory.</param>
        /// <param name="parentShape">The menu parent shape.</param>
        /// <param name="menu">The menu shape.</param>
        /// <param name="menuItems">The current level to populate.</param>
        /// <param name="customItemData">Custom menu item</param>
        /// <param name="settings">Menu widget part containing settings</param>
        /// <param name="level">Current menu level</param>
        protected void PopulateMenu(
            dynamic shapeFactory, dynamic parentShape, dynamic menu, 
            IEnumerable<MenuItem> menuItems, IDictionary<int, AdvancedMenuItemPart> customItemData, 
            MenuWidgetPart settings, int level)
        {
            /* If should cut then return and stop further processing */
            if (settings.Levels > 0 && level > settings.Levels && settings.CutOrFlattenLower) return;

            foreach (var menuItem in menuItems) {
                AdvancedMenuItemPart correspondingPart = null;
                int hintId;
                if (!string.IsNullOrWhiteSpace(menuItem.IdHint) && int.TryParse(menuItem.IdHint, out hintId))
                    customItemData.TryGetValue(hintId, out correspondingPart);

                dynamic menuItemShape = BuildMenuItemShape(shapeFactory, parentShape, menu, menuItem, correspondingPart, settings);

                if (menuItem.Items != null && menuItem.Items.Any())
                {
                    if(settings.Levels > 0 && level > settings.Levels)
                        PopulateMenu(shapeFactory, parentShape, menu, menuItem.Items, customItemData, settings, level + 1);
                    else
                        PopulateMenu(shapeFactory, menuItemShape, menu, menuItem.Items, customItemData, settings, level + 1);
                }

                parentShape.Add(menuItemShape, menuItem.Position);
            }
        }

        /// <summary>
        /// Find the first level in the selection path, starting from the bottom, that is not a local task.
        /// </summary>
        /// <param name="selectedPath">The selection path stack. The bottom node is the currently selected one.</param>
        /// <returns>The first node, starting from the bottom, that is not a local task. Otherwise, null.</returns>
        protected static MenuItem FindParentLocalTask(Stack<MenuItem> selectedPath)
        {
            if (selectedPath != null)
            {
                var parentMenuItem = selectedPath.Pop();
                if (parentMenuItem != null)
                {
                    while (selectedPath.Count > 0)
                    {
                        var currentMenuItem = selectedPath.Pop();
                        if (currentMenuItem.LocalNav)
                        {
                            return parentMenuItem;
                        }

                        parentMenuItem = currentMenuItem;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Builds a menu item shape.
        /// </summary>
        /// <param name="shapeFactory">The shape factory.</param>
        /// <param name="parentShape">The parent shape.</param>
        /// <param name="menu">The menu shape.</param>
        /// <param name="menuItem">The menu item to build the shape for.</param>
        /// <returns>The menu item shape.</returns>
        protected dynamic BuildMenuItemShape(dynamic shapeFactory, dynamic parentShape, dynamic menu, MenuItem menuItem, AdvancedMenuItemPart part, MenuWidgetPart settings) {
            var item = shapeFactory.MenuItem()
                .Text(menuItem.Text)
                .IdHint(menuItem.IdHint)
                .Href(menuItem.Href)
                .LinkToFirstChild(menuItem.LinkToFirstChild)
                .LocalNav(menuItem.LocalNav)
                .Selected(menuItem.Selected)
                .RouteValues(menuItem.RouteValues)
                .Item(menuItem)
                .Menu(menu)
                .Parent(parentShape)
                .WrapChildrenInDiv(settings.WrapChildrenInDiv);

            if(part != null) {
                item
                    .DisplayHref(part.DisplayHref)
                    .DisplayText(part.DisplayText)
                    .SubTitle(part.SubTitle);
                if (part.Is<BodyPart>())
                    item.HtmlTemplate(part.As<BodyPart>().Text);
                
                if(!string.IsNullOrWhiteSpace(part.Classes))
                    foreach(var c in part
                        .Classes
                        .Split(new[]{' ', ',', ';'}, StringSplitOptions.RemoveEmptyEntries)) {
                        item.Classes.Add(c.Trim());
                    }
            }

            /* Setting currently selected item */
            item.Current(
                UrlUtility.RouteMatches(menuItem.RouteValues, _httpContextAccessor.Current().Request.RequestContext.RouteData.Values)
                || UrlUtility.UrlMatches(menuItem.Href, _httpContextAccessor.Current().Request.Path, _httpContextAccessor.Current())
            );

            return item;
        }
    
        protected static bool CollapseNonSiblings(IEnumerable<MenuItem> menuItems, MenuItem item) {

            var retVal = false;
            foreach (var menuItem in menuItems)
            {
                if (menuItem == item) {
                    retVal = true;
                }
                else {
                    var isSelected = CollapseNonSiblings(menuItem.Items, item);
                    if (!isSelected)
                        menuItem.Items = null;
                    else
                        retVal = true;
                }

            }

            return retVal;
        }
    }
}