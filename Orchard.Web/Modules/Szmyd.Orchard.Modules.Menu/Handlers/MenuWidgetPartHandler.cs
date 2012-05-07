using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    public class MenuWidgetPartHandler : ContentHandler {
        public MenuWidgetPartHandler(IRepository<MenuWidgetPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}