using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Part defining the custom menu. To be attached only to the CustomMenu content item.
    /// </summary>
    public class AdvancedMenuPart : ContentPart<AdvancedMenuPartRecord>
    {
        [Required]
        public string Name
        {
            get { return Record.Name; }
            set { Record.Name = value; }
        }
        public IEnumerable<AdvancedMenuItemPart> Items { get; set; }
    }
}