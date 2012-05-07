
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Handlers
{
    [OrchardFeature("Szmyd.Menu.Breadcrumbs")]
    public class BreadcrumbsPartHandler : ContentHandler
    {
        public BreadcrumbsPartHandler(IRepository<BreadcrumbsPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}