using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Core.Navigation.Models;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.UI.Navigation;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Providers
{
    [OrchardSuppressDependency("Orchard.Core.Navigation.Services.MainMenuNavigationProvider")]
    public class NavigationProviderFactory : INavigationProviderFactory
    {
        private readonly Work<IAdvancedMenuService> _menuService;
        private readonly ICacheManager _cache;
        private readonly ISignals _signals;
        private readonly IEnumerable<INavigationProvider> _providers;

        public NavigationProviderFactory(Work<IAdvancedMenuService> menuService, ICacheManager cache, ISignals signals,
            IEnumerable<INavigationProvider> providers)
        {
            _menuService = menuService;
            _cache = cache;
            _signals = signals;
            _providers = providers;
        }

        #region Implementation of INavigationProviderFactory

        /// <summary>
        /// Gets providers for the dynamically created menus. 
        /// todo: Providers are cached for boosting performance. Cache gets refreshed every time the menus are modified.
        /// </summary>
        public IEnumerable<INavigationProvider> Providers
        {
            get
            {
                foreach (var p in _providers)
                {
                    yield return p;
                }
                foreach (var p in _menuService.Value.GetMenus()
                    .Select(m => new NavigationProvider
                    {
                        MenuName = m.Name,
                        Items = _menuService.Value.GetMenuItems(m.Name)
                    })) {
                    yield return p;
                }
            }
        }

        #endregion
    }
}