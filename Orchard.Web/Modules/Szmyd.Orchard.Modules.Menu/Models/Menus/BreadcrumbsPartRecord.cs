using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace Szmyd.Orchard.Modules.Menu.Models {
    [OrchardFeature("Szmyd.Menu.Breadcrumbs")]
    public class BreadcrumbsPartRecord : ContentPartRecord {
        public virtual bool LastAsLink { get; set; }
        public virtual string Separator { get; set; }
        public virtual string LeadingText { get; set; }

        /* Changes in 1.2 */
        public virtual string MenuName { get; set; }
    }
}