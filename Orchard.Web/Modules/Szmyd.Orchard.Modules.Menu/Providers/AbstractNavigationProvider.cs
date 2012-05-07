using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Localization;
using Orchard.UI;
using Orchard.UI.Navigation;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Providers
{
    /// <summary>
    /// Abstract class for building hierarchical menus
    /// </summary>
    internal abstract class AbstractNavigationProvider : INavigationProvider
    {
        #region Implementation of INavigationProvider

        public abstract string MenuName { get; internal set; }

        public virtual void GetNavigation(NavigationBuilder builder) {
            BuildHierarchy(Items, builder);
        }

        #endregion

        public abstract IEnumerable<AdvancedMenuItemPart> Items { get; internal set; }

        private static void BuildHierarchy(IEnumerable<AdvancedMenuItemPart> items, NavigationBuilder builder, int level = 1)
        {
            // Filtering item collection to only the current level
            foreach (var menuPart in items
                .Where(i => i.Position.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Count() == level)
                .OrderBy(m => m.Position, new FlatPositionComparer()))
            {
                if (menuPart == null) continue;
                var part = menuPart;

                // Choosing descendants of a current item
                var descendants = items.Where(i =>
                    i.Position.StartsWith(part.Position + ".")
                    && i.Position.Trim() != part.Position.Trim());

                if (part.RelatedItem == null){
                    builder.Add(new LocalizedString(HttpUtility.HtmlEncode(part.Text)), part.Position,
                                item => 
                                {
                                    item.IdHint(part.ContentItem.Id.ToString());
                                    item.Url(part.Url);
                                    if (descendants.Any()) 
                                        BuildHierarchy(descendants, item, level + 1);
                                });
                }
                else{
                    builder.Add(new LocalizedString(HttpUtility.HtmlEncode(part.Text)), part.Position,
                                item => {
                                    item.IdHint(part.ContentItem.Id.ToString());
                                    item.Action(part.RouteValues);
                                    if (descendants.Any()) 
                                        BuildHierarchy(descendants, item, level + 1);
                                });
                }
            }
        }
    }
}