using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    public class AdvancedMenuItemPartHandler : ContentHandler {
        public AdvancedMenuItemPartHandler(IRepository<AdvancedMenuItemPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            OnLoaded<AdvancedMenuItemPart>((ctx, part) => {
                                               var relatedItem = ctx.ContentManager.Get(part.Record.RelatedContentId);
                                               if (relatedItem != null) {
                                                   part.RelatedItem = relatedItem;
                                                   part.RouteValues = ctx.ContentManager.GetItemMetadata(relatedItem).DisplayRouteValues;
                                               }
                                           });
            OnRemoved<AdvancedMenuItemPart>((ctx, part) => repository.Delete(part.Record));
        }
    }
}