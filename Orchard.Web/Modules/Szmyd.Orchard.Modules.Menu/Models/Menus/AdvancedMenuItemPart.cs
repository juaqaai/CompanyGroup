using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using Orchard.ContentManagement;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Menu item from custom-defined menu
    /// </summary>
    public class AdvancedMenuItemPart : ContentPart<AdvancedMenuItemPartRecord>
    {
        [StringLength(255)]
        public string Text
        {
            get { return Record.Text; }
            set { Record.Text = value; }
        }

        [Required]
        public string Position
        {
            get { return Record.Position; }
            set { Record.Position = value; }
        }

        public string Url
        {
            get { return Record.Url; }
            set { Record.Url = value; }
        }

        /// <summary>
        /// Name of the corresponding menu
        /// </summary>
        public string MenuName
        {
            get { return Record.MenuName; }
            set { Record.MenuName = value; }
        }

        /// <summary>
        /// Name of the corresponding menu
        /// </summary>
        public string SubTitle
        {
            get { return Record.SubTitle; }
            set { Record.SubTitle = value; }
        }

        /// <summary>
        /// Additional CSS classes to add to this menu item
        /// </summary>
        public string Classes
        {
            get { return Record.Classes; }
            set { Record.Classes = value; }
        }


        /// <summary>
        /// Display menu name
        /// </summary>
        public bool DisplayText
        {
            get { return Record.DisplayText; }
            set { Record.DisplayText = value; }
        }

        /// <summary>
        /// Should display menu item as link
        /// </summary>
        public bool DisplayHref
        {
            get { return Record.DisplayHref; }
            set { Record.DisplayHref = value; }
        }

        public int RelatedItemId
        {
            get { return Record.RelatedContentId; }
        }

        public IContent RelatedItem { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }
}