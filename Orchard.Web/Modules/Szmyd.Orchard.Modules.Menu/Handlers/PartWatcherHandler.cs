

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
    public class PartWatcherHandler : ContentHandler
    {
        public PartWatcherHandler(IPartWatcher watcher)
        {
            OnGetDisplayShape<RoutePart>((ctx, part) =>
            {
                if (ctx.DisplayType != "Detail") return;
                watcher.Watch(part);
            });
        }
    }
}