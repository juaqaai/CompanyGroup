using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Company.Controllers
{
    [Themed]
    public class NewsletterController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}