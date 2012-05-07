using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Szmyd.Orchard.Modules.Menu.Providers
{
    /// <summary>
    /// Hacked manager to allow injecting factory-produced navigation providers.
    /// </summary>
    [OrchardSuppressDependency("Orchard.UI.Navigation.NavigationManager")]
    public class HackedNavigationManager : NavigationManager
    {
        public HackedNavigationManager(INavigationProviderFactory factory, IAuthorizationService authorizationService, UrlHelper urlHelper, IOrchardServices orchardServices) 
            : base(factory.Providers, authorizationService, urlHelper, orchardServices) {}
    }
}