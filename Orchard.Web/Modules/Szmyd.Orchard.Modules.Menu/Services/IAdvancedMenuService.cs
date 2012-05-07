using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.ContentManagement;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Services
{
    /// <summary>
    /// Service for dealing with custom menus.
    /// </summary>
    public interface IAdvancedMenuService : IDependency {
        IEnumerable<AdvancedMenuPart> GetMenus();
        AdvancedMenuPart GetMenu(string menuName);
        IEnumerable<AdvancedMenuItemPart> GetMenuItems(string menuName);
        IEnumerable<AdvancedMenuItemPart> GetMenuItemsForContent(IContent contentItem);
        AdvancedMenuItemPart GetMenuItem(int itemId);
        void DeleteMenu(string menuName);
        void DeleteMenuItem(int itemId);
        void TriggerSignal();
    }
}