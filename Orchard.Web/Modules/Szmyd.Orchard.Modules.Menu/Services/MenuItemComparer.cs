using System.Collections.Generic;
using Orchard.UI.Navigation;

namespace Szmyd.Orchard.Modules.Menu.Services
{
    public class MenuItemComparer : IEqualityComparer<MenuItem>
    {
        #region Implementation of IEqualityComparer<in MenuItem>

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
        public bool Equals(MenuItem x, MenuItem y) {
            return (x.Url != null) ? x.Url == y.Url : x.Href == y.Href;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
        public int GetHashCode(MenuItem obj) {
            return (obj.Url != null) ? obj.Url.GetHashCode() : obj.Href.GetHashCode();
        }

        #endregion
    }
}