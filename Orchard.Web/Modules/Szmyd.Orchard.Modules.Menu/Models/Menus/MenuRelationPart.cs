using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Szmyd.Orchard.Modules.Menu.ViewModels;

namespace Szmyd.Orchard.Modules.Menu.Models.Menus {
	
    public class MenuRelationPart : ContentPart {
        public IEnumerable<MenuRelation> Menus { 
            get;
            set;
        }

        public IEnumerable<MenuRelation> SelectedMenus
        {
            get;
            set;
        }

    }
}
