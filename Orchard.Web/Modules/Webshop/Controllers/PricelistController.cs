using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class PricelistController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}