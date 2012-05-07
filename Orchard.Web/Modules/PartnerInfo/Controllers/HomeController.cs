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
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

    }
}