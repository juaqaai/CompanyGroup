

using Orchard;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;
using Szmyd.Orchard.Modules.Menu.Settings;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class ItemCounterPartHandler : ContentHandler
    {

        public ItemCounterPartHandler(ICounterService counters, IAuthenticationService auth, IOrchardServices services)
        {
            OnRemoved<ItemCounterPart>((ctx, part) => counters.RemoveAllCounters(part.ContentItem.Id));

            OnGetDisplayShape<ItemCounterPart>((ctx, part) =>
            {
                var settings = part.Settings.GetModel<ItemCounterPartTypePartSettings>();
                var type = part.Record == null ? settings.Type : part.Type;

                // Incrementing visit counters here
                var user = auth.GetAuthenticatedUser();
                var site = services.WorkContext.CurrentSite;
                var countUser = 0;
                var countSite = 0;
                var countSession = 0;
                if (user != null){
                    countUser = ctx.DisplayType != "Detail" ? 
                        counters.GetCounter(part.ContentItem.Id, user.Id, CounterType.Visits, CounterStoreType.Database) 
                        : counters.Increment(part.ContentItem.Id, user.Id, CounterType.Visits, CounterStoreType.Database);
                }
                if (site != null){
                     countSite = ctx.DisplayType != "Detail" ?
                        counters.GetCounter(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Database)
                        : counters.Increment(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Database);
                }
                if (site != null && services.WorkContext.HttpContext != null){
                    countSession = ctx.DisplayType != "Detail" ?
                        counters.GetCounter(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Session)
                        : counters.Increment(part.ContentItem.Id, site.Id, CounterType.Visits, CounterStoreType.Session);
                }

                /* Setting appropriate counter */
                part.Count = (type == VisitCounterType.PerSite)
                                 ? countSite
                                 : (type == VisitCounterType.PerUser &&
                                    user != null)
                                       ? countUser
                                       : countSession;
            });
        }
    }
}