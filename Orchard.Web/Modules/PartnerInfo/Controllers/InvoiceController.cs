using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    [Themed]
    public class InvoiceController : Cms.CommonCore.Controllers.HomeController
    {

        public ActionResult Index()
        {
            CompanyGroup.Dto.ServiceRequest.GetInvoiceInfo request = new CompanyGroup.Dto.ServiceRequest.GetInvoiceInfo()
            {
                LanguageId = this.ReadLanguageFromCookie(),
                VisitorId = this.ReadObjectIdFromCookie(), 
                PaymentType = 1
            };

            List<CompanyGroup.Dto.PartnerModule.InvoiceInfo> response = this.PostJSonData<List<CompanyGroup.Dto.PartnerModule.InvoiceInfo>>("CustomerService", "GetInvoiceInfo", request);

            List<Cms.PartnerInfo.Models.InvoiceInfo> invoiceInfoList = new List<Cms.PartnerInfo.Models.InvoiceInfo>();

            invoiceInfoList.AddRange(response.ConvertAll(x => ConverInvoiceInfoToInvoiceInfo(x)));

            Cms.PartnerInfo.Models.InvoiceInfoList model = new Cms.PartnerInfo.Models.InvoiceInfoList(invoiceInfoList);

            return View("Index", model);
        }

        public JsonResult FilteredInvoice(Cms.PartnerInfo.Models.GetInvoiceInfo request)
        {
            CompanyGroup.Dto.ServiceRequest.GetInvoiceInfo req = new CompanyGroup.Dto.ServiceRequest.GetInvoiceInfo()
            {
                LanguageId = this.ReadLanguageFromCookie(),
                VisitorId = this.ReadObjectIdFromCookie(),
                PaymentType = request.PaymentType
            };

            List<CompanyGroup.Dto.PartnerModule.InvoiceInfo> response = this.PostJSonData<List<CompanyGroup.Dto.PartnerModule.InvoiceInfo>>("CustomerService", "GetInvoiceInfo", req);

            List<Cms.PartnerInfo.Models.InvoiceInfo> invoiceInfoList = new List<Cms.PartnerInfo.Models.InvoiceInfo>();

            invoiceInfoList.AddRange(response.ConvertAll(x => ConverInvoiceInfoToInvoiceInfo(x)));

            Cms.PartnerInfo.Models.InvoiceInfoList model = new Cms.PartnerInfo.Models.InvoiceInfoList(invoiceInfoList);

            return Json(model, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        private Cms.PartnerInfo.Models.InvoiceInfo ConverInvoiceInfoToInvoiceInfo(CompanyGroup.Dto.PartnerModule.InvoiceInfo from)
        {
            return new Cms.PartnerInfo.Models.InvoiceInfo() 
                       { 
                           ContactPersonId = from.ContactPersonId, 
                           CurrencyCode = from.CurrencyCode, 
                           CusomerRef = from.CusomerRef, 
                           DueDate = from.DueDate, 
                           InvoiceAmount = from.InvoiceAmount, 
                           InvoiceCredit = from.InvoiceCredit, 
                           InvoiceDate = from.InvoiceDate, 
                           InvoiceId = from.InvoiceId, 
                           InvoicingAddress = from.InvoicingAddress, 
                           InvoicingName = from.InvoicingName, 
                           Payment = from.Payment, 
                           Printed = from.Printed, 
                           ReturnItemId = from.ReturnItemId, 
                           SalesType = from.SalesType,
                           SalesId = from.SalesId, 
                           Lines = from.Lines.ConvertAll(x => ConvertInvoiceLineInfoToInvoiceLineInfo(x)) };
        }

        private Cms.PartnerInfo.Models.InvoiceLineInfo ConvertInvoiceLineInfoToInvoiceLineInfo(CompanyGroup.Dto.PartnerModule.InvoiceLineInfo from)
        {
            return new Cms.PartnerInfo.Models.InvoiceLineInfo()
            {
                CurrencyCode = from.CurrencyCode,
                DeliveryType = from.DeliveryType,
                ItemDate = from.ItemDate, 
                ItemId = from.ItemId,
                LineAmount = from.LineAmount,
                Name = from.Name,
                TaxAmount = from.TaxAmount,
                Quantity = from.Quantity,
                SalesPrice = from.SalesPrice
            };
        }
    }
}