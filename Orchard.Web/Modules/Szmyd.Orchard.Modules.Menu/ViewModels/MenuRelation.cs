using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Szmyd.Orchard.Modules.Menu.ViewModels
{
    public class MenuRelation
    {
        [Required]
        public string MenuName { get; set; }

        [Required]
        public string MenuText { get; set; }

        [Required]
        public string Position { get; set; }

        public bool Selected { get; set; }

    }
}