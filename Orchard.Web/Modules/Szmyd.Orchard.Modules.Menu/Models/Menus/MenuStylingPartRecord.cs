using Orchard.ContentManagement.Records;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    public class MenuStylingPartRecord : ContentPartRecord
    {
        public virtual string BackColor { get; set; }
        public virtual string ForeColor { get; set; }
        public virtual string SelectedBackColor { get; set; }
        public virtual string SelectedForeColor { get; set; }
        public virtual string HoverBackColor { get; set; }
        public virtual string HoverForeColor { get; set; }
        public virtual MenuStyles Style { get; set; }

    }
}