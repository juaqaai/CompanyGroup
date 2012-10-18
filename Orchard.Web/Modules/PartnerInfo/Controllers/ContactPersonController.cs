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

        /// <summary>
        /// jelszómódosítás űrlap
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return View("ChangePassword", visitor);
        }

        /// <summary>
        /// jelszómódosítás művelet
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePwd([Bind(Prefix = "")] Cms.PartnerInfo.Models.ChangePassword request)
        {
            CompanyGroup.Dto.ServiceRequest.ChangePassword req = new CompanyGroup.Dto.ServiceRequest.ChangePassword()
            {
                VisitorId = this.ReadObjectIdFromCookie(),
                Language = this.ReadLanguageFromCookie(), 
                NewPassword = request.NewPassword, 
                OldPassword = request.OldPassword, 
                UserName = request.UserName
            };

            CompanyGroup.Dto.PartnerModule.ChangePassword response = this.PostJSonData<CompanyGroup.Dto.PartnerModule.ChangePassword>("ContactPersonService", "ChangePassword", req);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}