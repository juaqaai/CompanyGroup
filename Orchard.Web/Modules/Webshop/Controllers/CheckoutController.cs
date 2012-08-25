using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class CheckoutController : Cms.CommonCore.Controllers.HomeController
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}