using Orchard.ContentManagement.Records;

namespace Szmyd.Orchard.Modules.Menu.Models {
    public class MenuSettingsPartRecord : ContentPartRecord {
        public virtual int Levels { get; set; }
    }
}