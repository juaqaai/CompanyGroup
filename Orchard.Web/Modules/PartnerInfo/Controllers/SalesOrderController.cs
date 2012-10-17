using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    [Themed]
    public class SalesOrderController : Cms.CommonCore.Controllers.HomeController
    {
        /// <summary>
        /// nyitott vevőrendelés info lista
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CompanyGroup.Dto.ServiceRequest.GetOrderInfo request = new CompanyGroup.Dto.ServiceRequest.GetOrderInfo()
            {
                LanguageId = this.ReadLanguageFromCookie(),
                VisitorId = this.ReadObjectIdFromCookie()
            };

            List<CompanyGroup.Dto.PartnerModule.OrderInfo> response = this.PostJSonData<List<CompanyGroup.Dto.PartnerModule.OrderInfo>>("SalesOrderService", "GetOrderInfo", request);

            List<Cms.PartnerInfo.Models.OrderInfo> orderInfoList = new List<Cms.PartnerInfo.Models.OrderInfo>();

            orderInfoList.AddRange(response.ConvertAll(x => ConvertOrderInfoToOrderInfo(x)));

            Cms.PartnerInfo.Models.OrderInfoList model = new Cms.PartnerInfo.Models.OrderInfoList(orderInfoList);

            return View("Index", model);
        }

        private Cms.PartnerInfo.Models.OrderInfo ConvertOrderInfoToOrderInfo(CompanyGroup.Dto.PartnerModule.OrderInfo from)
        {
            return new Cms.PartnerInfo.Models.OrderInfo() { CreatedDate = from.CreatedDate, SalesId = from.SalesId, Lines = from.Lines.ConvertAll(x => ConvertOrderLineInfoToOrderLineInfo(x)) };   
        }

        private Cms.PartnerInfo.Models.OrderLineInfo ConvertOrderLineInfoToOrderLineInfo(CompanyGroup.Dto.PartnerModule.OrderLineInfo from)
        {
            return new Cms.PartnerInfo.Models.OrderLineInfo()
            {
                CurrencyCode = from.CurrencyCode,
                InventLocationId = from.InventLocationId,
                ItemId = from.ItemId,
                LineAmount = from.LineAmount,
                Name = from.Name,
                Payment = from.Payment,
                Quantity = from.Quantity,
                SalesPrice = from.SalesPrice,
                ShippingDateRequested = from.ShippingDateRequested,
                StatusIssue = from.StatusIssue
            };
        }
    }
}