using System.Linq;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard.Utility.Extensions;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu
{
    [OrchardSuppressDependency("Orchard.Core.Navigation.AdminMenu")]
    public class AdminMenu : INavigationProvider
    {
        private readonly IAdvancedMenuService _menuServices;

        public AdminMenu(IAdvancedMenuService menuServices) {
            _menuServices = menuServices;
        }

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            var menuNames = _menuServices.GetMenus().Select(m => m.Name.CamelFriendly());
            builder.AddImageSet("navigation")
                .Add(T("Navigation"), "7",
                    menu => {
                            menu.Add(T("Create new menu"), "0", item => item.Action("Create", "MenuAdmin", new { area = "Szmyd.Orchard.Modules.Menu" })
                                                                          .Permission(Permissions.EditMenus));
                            foreach(var name in menuNames) {
                                string name1 = name;
                                menu.Add(T("Manage menu '{0}'", name), "1", item => item.Action("Index", "MenuAdmin", new {menuName = name1, area = "Szmyd.Orchard.Modules.Menu"})
                                                                          .Permission(Permissions.EditMenuItems));
                            }
                    });
        }
    }
}
