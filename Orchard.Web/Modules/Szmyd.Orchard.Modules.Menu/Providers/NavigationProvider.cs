using System.Collections.Generic;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Providers
{
    internal class NavigationProvider : AbstractNavigationProvider
    {
        public override string MenuName { get; internal set; }
        public override IEnumerable<AdvancedMenuItemPart> Items {
            get; internal set; 
        }
    }
}