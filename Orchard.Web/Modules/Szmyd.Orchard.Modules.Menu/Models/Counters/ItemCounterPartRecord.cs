using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class ItemCounterPartRecord : ContentPartRecord {
        public virtual VisitCounterType Type { get; set; }
    }
}