using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    [OrchardFeature("Szmyd.Menu.Breadcrumbs")]
    public class BreadcrumbsPart : ContentPart<BreadcrumbsPartRecord> {
        /// <summary>
        /// Should display last breadcrumb as link or not
        /// </summary>
        public bool LastAsLink
        {
            get { return Record.LastAsLink; }
            set { Record.LastAsLink = value; }
        }

        /// <summary>
        /// Separator between crumbs
        /// </summary>
        public string Separator {
            get { return Record.Separator; }
            set { Record.Separator = value; }
        }

        /// <summary>
        /// Leading text before breadcrumbs
        /// </summary>
        public string LeadingText {
            get { return Record.LeadingText; }
            set { Record.LeadingText = value; }
        }

        /* Changes in 1.2 */

        /// <summary>
        /// Name of the menu the breadcrumb part is based on
        /// </summary>
        public string MenuName {
            get { return Record.MenuName; }
            set { Record.MenuName = value; }
        }

        public IEnumerable<dynamic> AvailableMenus { get; set; }
    }
}