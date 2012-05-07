using System;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Containers.Models;
using Orchard.Core.Contents.Controllers;
using Orchard.Core.Routable.Models;
using Orchard.Localization;
using Orchard.Mvc.AntiForgery;
using Orchard.Mvc.Extensions;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Orchard.Utility;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;
using Szmyd.Orchard.Modules.Menu.ViewModels;
using Szmyd.Orchard.Modules.Menu.Drivers;

namespace Szmyd.Orchard.Modules.Menu.Controllers
{
    [ValidateInput(false), Admin]
    public class MenuAdminController : Controller, IUpdateModel
    {
        private readonly IAdvancedMenuService _menuService;
        private readonly IOrchardServices _services;
        private readonly INavigationManager _nav;

        public MenuAdminController(IAdvancedMenuService menuService, IOrchardServices services, INavigationManager nav)
        {
            _menuService = menuService;
            _services = services;
            _nav = nav;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public ActionResult Index(string menuName)
        {
            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            var model = new MenuItemsViewModel { MenuName = menuName, Items = _menuService.GetMenuItems(menuName) };
            return View(model);
        }

        public ActionResult Create()
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenus, T("Not allowed to create new menus")))
                return new HttpUnauthorizedResult();

            var item = _services.ContentManager.New<AdvancedMenuPart>("Menu");

            dynamic model = _services.ContentManager.BuildEditor(item);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost, ActionName("Create")]
        [FormValueRequired("submit.Save")]
        public ActionResult CreatePOST()
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Not allowed to create new menus")))
                return new HttpUnauthorizedResult();

            var item = _services.ContentManager.New<AdvancedMenuPart>("Menu");

            _services.ContentManager.Create(item);
            var model = _services.ContentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            _services.Notifier.Information(T("Your {0} has been created.", item.TypeDefinition.DisplayName));
            return Redirect(Url.Action("Index", "MenuAdmin", new { menuName = item.Name, area = "Szmyd.Orchard.Modules.Menu" }));
        }

        public ActionResult CreateItem(string menuName, MenuItemType type)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Not allowed to create menu items")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            AdvancedMenuItemPart item = null;
            switch (type)
            {
                case MenuItemType.Simple:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("SimpleMenuItem");
                    item.DisplayHref = true;
                    item.DisplayText = true;
                    break;
                case MenuItemType.Templated:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("TemplatedMenuItem");
                    item.DisplayHref = false;
                    item.DisplayText = false;
                    break;
                default:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("SimpleMenuItem");
                    item.DisplayHref = true;
                    item.DisplayText = true;
                    break;
            }

            if (item == null) return HttpNotFound();

            item.MenuName = menuName;
            dynamic model = _services.ContentManager.BuildEditor(item);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost, ActionName("CreateItem")]
        [FormValueRequired("submit.Save")]
        [ValidateAntiForgeryTokenOrchard]
        public ActionResult CreateItemPOST(string menuName, MenuItemType type)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Couldn't create menu item")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            AdvancedMenuItemPart item = null;
            switch (type)
            {
                case MenuItemType.Simple:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("SimpleMenuItem");
                    break;
                case MenuItemType.Templated:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("TemplatedMenuItem");
                    break;
                default:
                    item = _services.ContentManager.New<AdvancedMenuItemPart>("SimpleMenuItem");
                    break;
            }
            item.MenuName = menuName;

            _services.ContentManager.Create(item);
            var model = _services.ContentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            _services.Notifier.Information(T("Your {0} has been created.", item.TypeDefinition.DisplayName));
            _menuService.TriggerSignal();

            return Redirect(Url.Action("Index", "MenuAdmin", new { menuName, area = "Szmyd.Orchard.Modules.Menu" }));
        }

        public ActionResult EditItem(string menuName, int itemId)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Couldn't edit menu item")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            var item = _menuService.GetMenuItem(itemId);
            if (item == null)
                return HttpNotFound();

            dynamic model = _services.ContentManager.BuildEditor(item);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [ActionName("EditItem"), HttpPost]
        [ValidateAntiForgeryTokenOrchard]
        public ActionResult EditItemPOST(string menuName, int itemId, string returnUrl)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Couldn't edit menu item.")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            var item = _menuService.GetMenuItem(itemId);
            if (item == null)
                return HttpNotFound();

            // Validate form input
            var model = _services.ContentManager.UpdateEditor(item, this);
            if (!ModelState.IsValid)
            {
                _services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }
            _menuService.TriggerSignal();
            _services.Notifier.Information(T("Your {0} has been saved.", item.TypeDefinition.DisplayName));

            return this.RedirectLocal(returnUrl, Url.Action("EditItem", "MenuAdmin", new { menuName, itemId, area = "Szmyd.Orchard.Modules.Menu" }));
        }


        [ValidateAntiForgeryTokenOrchard]
        public ActionResult DeleteItem(string menuName, int itemId)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenuItems, T("Couldn't delete menu item")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            var item = _menuService.GetMenuItem(itemId);
            if (item == null)
                return HttpNotFound();

            _menuService.DeleteMenuItem(itemId);
            _services.Notifier.Information(T("Menu item was successfully deleted"));

            return Redirect(Url.Action("Index", "MenuAdmin", new { menuName, area = "Szmyd.Orchard.Modules.Menu" }));
        }

        [ValidateAntiForgeryTokenOrchard]
        public ActionResult Delete(string menuName)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenus, T("Couldn't delete {0} menu", menuName)))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            _menuService.DeleteMenu(menuName);

            // TODO: Delete corresponding menu items first
            _services.Notifier.Information(T("Menu {0} was successfully deleted", menuName));

            return Redirect(Url.Action("Create", "MenuAdmin", new { area = "Szmyd.Orchard.Modules.Menu" }));
        }

        public ActionResult Fill(string menuName, bool containable)
        {
            if (!_services.Authorizer.Authorize(Permissions.EditMenus, T("Couldn't fill the menu")))
                return new HttpUnauthorizedResult();

            var menu = _menuService.GetMenu(menuName);
            if (menu == null)
                return HttpNotFound();

            var routables = _services
                .ContentManager.Query<RoutePart, RoutePartRecord>().List().Select(r => r);

            int j = int.Parse(PositionUtility.GetNext(_nav.BuildMenu(menuName)));
            var currentItems = _menuService.GetMenuItems(menuName);
            foreach (var r in routables
                .Where(p => !currentItems.Any(i => i.RelatedItemId == p.Id) && (containable || (!containable && !p.Is<ContainablePart>()))))
            {
                
                int j1 = j;
                RoutePart r1 = r;
                _services.ContentManager.Create<AdvancedMenuItemPart>("SimpleMenuItem", i =>
                {
                    i.DisplayHref = true;
                    i.DisplayText = true;
                    i.MenuName = menuName;
                    i.Position = j1.ToString();
                    i.Text = r1.Title;
                    i.Record.RelatedContentId = r1.Id;
                    i.Url = "/"+r1.Path;
                });
                j++;
            }

            if (!ModelState.IsValid)
            {
                _services.TransactionManager.Cancel();
            }

            _services.Notifier.Information(T("Your menu has been filled."));
            _menuService.TriggerSignal();
            return Redirect(Url.Action("Index", "MenuAdmin", new { menuName, area = "Szmyd.Orchard.Modules.Menu" }));
        }
        #region Implementation of IUpdateModel

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        #endregion
    }
}