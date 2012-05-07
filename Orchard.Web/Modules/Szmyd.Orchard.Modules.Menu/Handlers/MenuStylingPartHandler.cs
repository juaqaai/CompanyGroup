using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    public class MenuStylingPartHandler : ContentHandler {
        public MenuStylingPartHandler(IRepository<MenuStylingPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}