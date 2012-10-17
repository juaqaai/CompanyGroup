using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Company.Controllers
{
    [Themed]
    public class NewsletterController : Cms.CommonCore.Controllers.HomeController
    {
        public ActionResult Index()
        {
            CompanyGroup.Dto.ServiceRequest.GetNewsletterCollection request = new CompanyGroup.Dto.ServiceRequest.GetNewsletterCollection()
            {
                Language = this.ReadLanguageFromCookie(),
                VisitorId = this.ReadObjectIdFromCookie(), 
                ManufacturerId = String.Empty
            };

            CompanyGroup.Dto.WebshopModule.NewsletterCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.NewsletterCollection>("NewsletterService", "GetNewsletterCollection", request);

            Cms.Company.Models.NewsletterCollection model = new Cms.Company.Models.NewsletterCollection(response);

            return View("Index", model);
        }
    }
}