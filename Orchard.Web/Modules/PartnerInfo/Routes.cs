using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Cms.PartnerInfo {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/Home",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "Home"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/Registration",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "Registration"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/Invoice",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "Invoice"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/SalesOrder",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "SalesOrder"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             },
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/ContactPerson",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "ContactPerson"},
                                                                                      {"action", "Index"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             },
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/ForgetPassword",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "ContactPerson"},
                                                                                      {"action", "ForgetPassword"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             },
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/ChangePassword",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "ContactPerson"},
                                                                                      {"action", "ChangePassword"}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             },
                             new RouteDescriptor {   Priority = 5,
                                                     Route = new Route(
                                                         "PartnerInfo/ContactPerson/{ChangePasswordId}/UndoChangePassword",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"},
                                                                                      {"controller", "ContactPerson"},
                                                                                      {"action", "UndoChangePassword"},
                                                                                      {"changePasswordId", ""}
                                                         },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "PartnerInfo"}
                                                         },
                                                         new MvcRouteHandler())
                             }, 
                         };
        }
    }
}