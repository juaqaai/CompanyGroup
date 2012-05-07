using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    public class AdvancedMenuPartHandler : ContentHandler {
        public AdvancedMenuPartHandler(IRepository<AdvancedMenuPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            OnRemoved<AdvancedMenuPart>((ctx, part) => repository.Delete(part.Record));
        }
    }
}