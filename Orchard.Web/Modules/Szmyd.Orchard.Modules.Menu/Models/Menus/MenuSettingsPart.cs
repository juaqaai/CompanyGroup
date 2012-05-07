using Orchard.ContentManagement;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Part defining menu settings.
    /// </summary>
    public class MenuSettingsPart : ContentPart<MenuSettingsPartRecord>
    {
        /// <summary>
        /// How many menu levels to display?
        /// Items below will be pushed up to the lowest displayed level and flattened.
        /// </summary>
        public int Levels
        {
            get { return Record.Levels; }
            set { Record.Levels = value; }
        }
    }
}