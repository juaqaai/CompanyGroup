using Orchard.ContentManagement.Records;

namespace Szmyd.Orchard.Modules.Menu.Models {
    public class MenuWidgetPartRecord : ContentPartRecord {
        public virtual int Mode { get; set; }
        public virtual string MenuName { get; set; }

        /* Changes in 1.2 */
        public virtual string RootNode { get; set; }
        public virtual bool WrapChildrenInDivs { get; set; }
        public virtual int Levels { get; set; }
        public virtual bool CutOrFlattenLower { get; set; }
    }
}