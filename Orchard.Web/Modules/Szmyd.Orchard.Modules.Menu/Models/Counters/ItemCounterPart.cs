using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Simple counter for adding visit countability to specific content types.
    /// </summary>
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class ItemCounterPart : ContentPart<ItemCounterPartRecord> {
        public int Count { get; set; }

        public VisitCounterType Type
        {
            get { return Record.Type; }
            set { Record.Type = value; }
        }
    }
}