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

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { ChangePassword = response, Visitor = visitor }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// jelszómódosítást visszavonó művelet
        /// </summary>
        /// <param name="undoChangePassword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UndoChangePassword(string undoChangePassword)
        {
            CompanyGroup.Dto.PartnerModule.UndoChangePassword response;

            if (String.IsNullOrEmpty(undoChangePassword))
            {
                CompanyGroup.Dto.ServiceRequest.UndoChangePassword request = new CompanyGroup.Dto.ServiceRequest.UndoChangePassword(undoChangePassword);

                response = this.PostJSonData<CompanyGroup.Dto.PartnerModule.UndoChangePassword>("ContactPersonService", "UndoChangePassword", request);
            }
            else
            { 
                response = new CompanyGroup.Dto.PartnerModule.UndoChangePassword(){ Succeeded = false, Message = "A jelszómódosítás visszavonásához tartozó azonosító nem lett megadva!"};
            }

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            Cms.PartnerInfo.Models.UndoChangePassword model = new Cms.PartnerInfo.Models.UndoChangePassword(response, visitor);

            return View("UndoChangePassword2", model);
        }

        /// <summary>
        /// elfelejtett jelszó űrlap
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgetPassword()
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return View("ForgetPassword", visitor);
        }

        /// <summary>
        /// jelszóemlékeztező művelet
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ForgetPwd([Bind(Prefix = "")] Cms.PartnerInfo.Models.ForgetPassword request)
        {
            CompanyGroup.Dto.ServiceRequest.ForgetPassword req = new CompanyGroup.Dto.ServiceRequest.ForgetPassword()
            {
                Language = this.ReadLanguageFromCookie(), 
                UserName = request.UserName
            };

            CompanyGroup.Dto.PartnerModule.ForgetPassword response = this.PostJSonData<CompanyGroup.Dto.PartnerModule.ForgetPassword>("ContactPersonService", "ForgetPassword", req);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { ForgetPassword = response, Visitor = visitor }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}