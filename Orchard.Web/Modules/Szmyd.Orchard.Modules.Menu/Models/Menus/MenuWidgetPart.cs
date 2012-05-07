using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Part for displaying the menu.
    /// </summary>
    public class MenuWidgetPart : ContentPart<MenuWidgetPartRecord>
    {
        /// <summary>
        /// Menu part display mode
        /// </summary>
        [Required]
        public MenuWidgetMode Mode {
            get { return (MenuWidgetMode) Record.Mode; }
            set { Record.Mode = (int) value; }
        }

        /// <summary>
        /// Name of the corresponding menu to display items from
        /// </summary>
        [Required]
        public string MenuName {
            get { return Record.MenuName; }
            set { Record.MenuName = value; }
        }

        public IEnumerable<dynamic> AvailableModes { get; set; }
        public IEnumerable<dynamic> AvailableMenus { get; set; }

        /* Changes in 1.2 */
        /// <summary>
        /// The root node (position - dot-notated) from which to start display
        /// </summary>
        public string RootNode
        {
            get { return Record.RootNode; }
            set { Record.RootNode = value; }
        }

        /// <summary>
        /// Should children be wrapped in div tags
        /// </summary>
        public bool WrapChildrenInDiv
        {
            get { return Record.WrapChildrenInDivs; }
            set { Record.WrapChildrenInDivs = value; }
        }

        /// <summary>
        /// How many levels to display max. (0 = all).
        /// </summary>
        public int Levels
        {
            get { return Record.Levels; }
            set { Record.Levels = value; }
        }

        /// <summary>
        /// Should cut or flatten levels below threshold.
        /// </summary>
        public bool CutOrFlattenLower
        {
            get { return Record.CutOrFlattenLower; }
            set { Record.CutOrFlattenLower = value; }
        }
    }
}