using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Szmyd.Frooth {
    
    public class Routes : IRouteProvider {
        #region IRouteProvider Members

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (RouteDescriptor routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Priority = 12,
                    Route = new Route(
                        "Admin/Navigation",
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"},
                            {"controller", "MenuAdmin"},
                            {"action", "Index"},
                            {"menuName", "main"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Priority = 12,
                    Route = new Route(
                        "Admin/Navigation/Create",
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"},
                            {"controller", "MenuAdmin"},
                            {"action", "Create"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Priority = 12,
                    Route = new Route(
                        "Admin/Navigation/{menuName}/CreateItem/{type}",
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"},
                            {"controller", "MenuAdmin"},
                            {"action", "CreateItem"},
                            {"type", "Simple"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Priority = 12,
                    Route = new Route(
                        "Admin/Navigation/{menuName}/{action}/{itemId}",
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"},
                            {"controller", "MenuAdmin"},
                            {"action", "Index"},
                            {"menuName", "main"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Priority = 12,
                    Route = new Route(
                        "Admin/Navigation/{menuName}/{action}",
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"},
                            {"controller", "MenuAdmin"},
                            {"action", "Index"},
                            {"menuName", "main"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Szmyd.Orchard.Modules.Menu"}
                        },
                        new MvcRouteHandler())
                }
            };
        }

        #endregion
    }
}