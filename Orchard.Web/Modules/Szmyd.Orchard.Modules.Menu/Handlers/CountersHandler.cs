

using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Routable.Models;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Orchard.Settings;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class CountersHandler : ContentHandler
    {
        public CountersHandler(ICounterService counters, IAuthenticationService auth, IOrchardServices services)
        {
            
            OnRemoved<RoutePart>((ctx, part) => counters.RemoveAllCounters(part.ContentItem.Id));

            OnGetDisplayShape<RoutePart>((ctx, part) =>
            {
                if (ctx.DisplayType != "Detail") return;

                // Avoiding double incrementing counter for route parts and itemcounter parts
                if (part.Is<ItemCounterPart>()) return;

                // Incrementing visit counters here
                var user = auth.GetAuthenticatedUser();
                var site = services.WorkContext.CurrentSite;
                if (user != null)
                    counters.Increment(part.ContentItem.Id, user.Id, CounterType.Visits, CounterStoreType.Database);
                if (site != null)
                    counters.Increment(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Database);
                if (site != null && services.WorkContext.HttpContext != null)
                    counters.Increment(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Session);
            });
        }
    }
}