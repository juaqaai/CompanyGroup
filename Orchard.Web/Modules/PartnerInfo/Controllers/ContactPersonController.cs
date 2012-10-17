using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    [Themed]
    public class ContactPersonController : Cms.CommonCore.Controllers.HomeController
    {
        public ActionResult Index()
        {
            //Cms.PartnerInfo.Models.ContactPerson

            return View("Index");
        }


        public ActionResult ChangePassword()
        {
            return View("Index");
        }
    }
}