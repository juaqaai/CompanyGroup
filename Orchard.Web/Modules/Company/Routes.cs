using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Cms.Company {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Carreer",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"},
                                                                                      {"controller", "Carreer"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Brand",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"},
                                                                                      {"controller", "Brand"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Newsletter",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"},
                                                                                      {"controller", "Newsletter"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Company"}
                                                         },
                                                         new MvcRouteHandler())
                             }
                         };
        }
    }
}