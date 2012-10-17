using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    /// <summary>
    /// PartnerInfo home controller 
    /// - SignIn (inherited)
    /// - SignOut (inherited)
    /// </summary>
    [Themed]
    public class HomeController : Cms.CommonCore.Controllers.HomeController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return View("Index", visitor);
        }

    }
}