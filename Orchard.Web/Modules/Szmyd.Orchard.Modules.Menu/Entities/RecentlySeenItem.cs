using Orchard.Environment.Extensions;
using Orchard.UI.Navigation;

namespace Szmyd.Orchard.Modules.Menu.Entities
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenItem : MenuItem
    {
        public int Visits { get; set; }
    }
}