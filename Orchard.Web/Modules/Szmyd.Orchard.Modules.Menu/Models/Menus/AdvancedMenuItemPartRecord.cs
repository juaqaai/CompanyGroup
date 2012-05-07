using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /* Changes in 1.2 */

    /// <summary>
    /// A record for storing menu item.
    /// </summary>
    public class AdvancedMenuItemPartRecord : ContentPartRecord
    {
        public virtual string Text { get; set; }
        public virtual string Url { get; set; }
        public virtual string Position { get; set; }
        public virtual string MenuName { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual string Classes { get; set; }
        public virtual bool DisplayText { get; set; }
        public virtual bool DisplayHref { get; set; }
        public virtual int RelatedContentId { get; set; }
    }
}