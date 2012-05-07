using System.Collections.Generic;

using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Szmyd.Orchard.Modules.Menu
{
    
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission EditMenus = new Permission { Description = "Create/edit custom menus", Name = "EditCustomMenus" };
        public static readonly Permission EditMenuItems = new Permission { Description = "Create/edit custom menu items", Name = "EditCustomMenuItems" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                EditMenus, EditMenuItems
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {EditMenus, EditMenuItems}
                }
            };
        }

    }
}
