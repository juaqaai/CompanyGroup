using System;
using System.Collections.Generic;
using Orchard;
using Orchard.UI.Navigation;

namespace Szmyd.Orchard.Modules.Menu.Providers
{
    /// <summary>
    /// Defines factory for dynamic INavigationProvider generation from the db data
    /// </summary>
    public interface INavigationProviderFactory : IDependency
    {
        /// <summary>
        /// Gets providers for the dynamically created menus
        /// </summary>
        IEnumerable<INavigationProvider> Providers { get; }
    }
}