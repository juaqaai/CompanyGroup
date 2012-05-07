using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPartHandler : ContentHandler {
        public RecentlySeenPartHandler(IRepository<RecentlySeenPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}