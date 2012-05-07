using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.ViewModels
{
    public class MenuItemsViewModel
    {
        public string MenuName { get; set; }
        public IEnumerable<AdvancedMenuItemPart> Items { get; set; }
    }
}