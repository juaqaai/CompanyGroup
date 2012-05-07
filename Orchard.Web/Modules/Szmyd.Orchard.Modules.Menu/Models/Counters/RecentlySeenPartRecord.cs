using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models {
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPartRecord : ContentPartRecord {
        public virtual string PositiveFilterRegex { get; set; }
        public virtual string NegativeFilterRegex { get; set; }
        
        /* 1.2 features */
        public virtual bool ShowCounts { get; set; }
        public virtual int ItemCount { get; set; }
        public virtual VisitCounterType VisitCounter { get; set; }
    }
}