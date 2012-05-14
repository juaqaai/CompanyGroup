using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Cms.Webshop {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Webshop/Catalogue",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "Catalogue"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Checkout",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "Checkout"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Webshop/Catalogue/{ProductId}/Details",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "Catalogue"},
                                                                                      {"action", "Details"},
                                                                                      {"productId", ""}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Webshop/Catalogue/{productId}/{recId}/{dataAreaId}/{maxWidth}/{maxHeight}/Picture",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "Catalogue"},
                                                                                      {"action", "Picture"},
                                                                                      {"productId", ""}, {"recId", ""}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             },
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Webshop/Pricelist",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "Pricelist"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "Webshop/ShoppingCart",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"},
                                                                                      {"controller", "ShoppingCart"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Webshop"}
                                                         },
                                                         new MvcRouteHandler())
                             }
                         };
        }
    }
}