using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class ShoppingCartController :  Cms.CommonCore.Controllers.HomeController
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// kosár azonosító alapján történő lekérdezése
        /// CompanyGroup.Dto.WebshopModule.ShoppingCart GetCartByKey(CompanyGroup.Dto.ServiceRequest.GetCartByKey request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCartByKey([Bind(Prefix = "")] Cms.Webshop.Models.GetCartByKey request)
        {
            CompanyGroup.Dto.ServiceRequest.GetCartByKey requestBody = new CompanyGroup.Dto.ServiceRequest.GetCartByKey(this.ReadLanguageFromCookie(), this.ReadCartIdFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "GetCartByKey", requestBody);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// látogatóhoz tartozó aktív kosár lekérdezése
        /// CompanyGroup.Dto.WebshopModule.ShoppingCartInfo GetActiveCart(CompanyGroup.Dto.ServiceRequest.GetActiveCart request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetActiveCart([Bind(Prefix = "")] Cms.Webshop.Models.GetActiveCart request)
        {
            CompanyGroup.Dto.ServiceRequest.GetActiveCart requestBody = new CompanyGroup.Dto.ServiceRequest.GetActiveCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartInfo response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartInfo>("ShoppingCartService", "GetActiveCart", requestBody);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { Visitor = visitor,
                              ActiveCart = response.ActiveCart,
                              OpenedItems = response.OpenedItems,
                              StoredItems = response.StoredItems, 
                              ShoppingCartOpenStatus = shoppingCartOpenStatus, 
                              CatalogueOpenStatus = catalogueOpenStatus,
                              LeasingOptions = response.LeasingOptions
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCartInfo AddCart(CompanyGroup.Dto.ServiceRequest.AddCart request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddCart([Bind(Prefix = "")] Cms.CommonCore.Models.Request.AddCart request)
        {
            CompanyGroup.Dto.ServiceRequest.AddCart addCart = new CompanyGroup.Dto.ServiceRequest.AddCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie(), String.Empty);

            CompanyGroup.Dto.WebshopModule.ShoppingCartInfo response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartInfo>("ShoppingCartService", "AddCart", addCart);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            return Json(new { Visitor = visitor,
                              ActiveCart = response.ActiveCart,
                              OpenedItems = response.OpenedItems,
                              StoredItems = response.StoredItems, 
                              ShoppingCartOpenStatus = shoppingCartOpenStatus, 
                              CatalogueOpenStatus = catalogueOpenStatus,
                              LeasingOptions = response.LeasingOptions
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCartInfo ActivateCart(CompanyGroup.Dto.ServiceRequest.ActivateCart request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ActivateCart([Bind(Prefix = "")] Cms.CommonCore.Models.Request.ActivateCart request)
        {
            CompanyGroup.Dto.ServiceRequest.ActivateCart activateCart = new CompanyGroup.Dto.ServiceRequest.ActivateCart(request.CartId, this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartInfo response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartInfo>("ShoppingCartService", "ActivateCart", activateCart);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            //this.WriteCartIdToCookie(request.CartId);
            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            return Json(new {  Visitor = visitor,
                               ActiveCart = response.ActiveCart,
                               OpenedItems = response.OpenedItems,
                               StoredItems = response.StoredItems, 
                              ShoppingCartOpenStatus = shoppingCartOpenStatus,
                              CatalogueOpenStatus = catalogueOpenStatus,
                               LeasingOptions = response.LeasingOptions
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCartInfo RemoveCart(CompanyGroup.Dto.ServiceRequest.RemoveCart request)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveCart()
        {
            CompanyGroup.Dto.ServiceRequest.RemoveCart removeCart = new CompanyGroup.Dto.ServiceRequest.RemoveCart(this.ReadCartIdFromCookie(), this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartInfo response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartInfo>("ShoppingCartService", "RemoveCart", removeCart);

            this.WriteCartIdToCookie(response.ActiveCart.Id);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            return Json(new { Visitor = visitor,
                              ActiveCart = response.ActiveCart,
                              OpenedItems = response.OpenedItems,
                              StoredItems = response.StoredItems, 
                              ShoppingCartOpenStatus = shoppingCartOpenStatus,
                              CatalogueOpenStatus = catalogueOpenStatus,
                              LeasingOptions = response.LeasingOptions
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCartInfo SaveCart(CompanyGroup.Dto.ServiceRequest.SaveCart request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCart([Bind(Prefix = "")] Cms.Webshop.Models.SaveCart request)
        {
            CompanyGroup.Dto.ServiceRequest.SaveCart saveCart = new CompanyGroup.Dto.ServiceRequest.SaveCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie(), this.ReadCartIdFromCookie(), request.Name);

            CompanyGroup.Dto.WebshopModule.ShoppingCartInfo response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartInfo>("ShoppingCartService", "SaveCart", saveCart);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            return Json(new { Visitor = visitor,
                              ActiveCart = response.ActiveCart,
                              OpenedItems = response.OpenedItems,
                              StoredItems = response.StoredItems, 
                              ShoppingCartOpenStatus = shoppingCartOpenStatus,
                              CatalogueOpenStatus = catalogueOpenStatus,
                              LeasingOptions = response.LeasingOptions
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
        }

        /// <summary>
        /// finanszírozási ajánlat elküldése
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateFinanceOffer([Bind(Prefix = "")] Cms.Webshop.Models.CreateFinanceOffer request)
        {

            CompanyGroup.Dto.ServiceRequest.CreateFinanceOffer createFinanceOffer = new CompanyGroup.Dto.ServiceRequest.CreateFinanceOffer() 
                                                                                        { 
                                                                                            Address = request.Address, 
                                                                                            CartId = this.ReadCartIdFromCookie(), 
                                                                                            NumOfMonth = request.NumOfMonth, 
                                                                                            PersonName = request.PersonName, 
                                                                                            Phone = request.Phone, 
                                                                                            StatNumber = request.StatNumber, 
                                                                                            VisitorId = this.ReadObjectIdFromCookie() 
                                                                                        };

            CompanyGroup.Dto.WebshopModule.FinanceOfferFulFillment response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.FinanceOfferFulFillment>("ShoppingCartService", "CreateFinanceOffer", createFinanceOffer);

            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new
            {
                Visitor = visitor,
                ActiveCart = response.ActiveCart,
                OpenedItems = response.OpenedItems,
                StoredItems = response.StoredItems,
                ShoppingCartOpenStatus = shoppingCartOpenStatus,
                CatalogueOpenStatus = catalogueOpenStatus,
                LeasingOptions = response.LeasingOptions,
                EmailNotification = response.EmaiNotification,
                Message = (!response.EmaiNotification) ? "Az értesítés elküldése nem sikerült!" : response.Message
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult CreateOrder([Bind(Prefix = "")] Cms.Webshop.Models.CreateOrder request)
        {
            CompanyGroup.Dto.ServiceRequest.CreateOrder createOrder = new CompanyGroup.Dto.ServiceRequest.CreateOrder() 
            { 
                CartId = this.ReadCartIdFromCookie(), 
                CustomerOrderId = request.CustomerOrderId, 
                CustomerOrderNote = request.CustomerOrderNote, 
                DeliveryAddressRecId = request.DeliveryAddressRecId, 
                DeliveryDate = request.DeliveryDate, 
                DeliveryRequest = request.DeliveryRequest, 
                DeliveryTerm = request.DeliveryTerm, 
                PaymentTerm = request.PaymentTerm, 
                VisitorId = this.ReadObjectIdFromCookie() 
            };

            CompanyGroup.Dto.WebshopModule.OrderFulFillment response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.OrderFulFillment>("ShoppingCartService", "CreateOrder", createOrder);

            //aktív kosár azonosítójának mentése http cookie-ba
            this.WriteCartIdToCookie(response.ActiveCart.Id);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new
            {
                Visitor = visitor,
                ActiveCart = response.ActiveCart,
                OpenedItems = response.OpenedItems,
                StoredItems = response.StoredItems,
                ShoppingCartOpenStatus = shoppingCartOpenStatus,
                CatalogueOpenStatus = catalogueOpenStatus,
                LeasingOptions = response.LeasingOptions,
                Created = response.Created,
                WaitForAutoPost = response.WaitForAutoPost,
                Message = response.Message
            }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCart AddLine(CompanyGroup.Dto.ServiceRequest.AddLine request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddLine([Bind(Prefix = "")] Cms.Webshop.Models.AddLine request)
        {
            CompanyGroup.Dto.ServiceRequest.AddLine addLine = new CompanyGroup.Dto.ServiceRequest.AddLine(this.ReadCartIdFromCookie(), request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions shoppingCart = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions>("ShoppingCartService", "AddLine", addLine);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { Visitor = visitor, ActiveCart = shoppingCart }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCart RemoveLine(CompanyGroup.Dto.ServiceRequest.RemoveLine request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveLine([Bind(Prefix = "")] Cms.Webshop.Models.RemoveLine request)
        {
            CompanyGroup.Dto.ServiceRequest.RemoveLine removeLine = new CompanyGroup.Dto.ServiceRequest.RemoveLine(this.ReadCartIdFromCookie(), request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions shoppingCart = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions>("ShoppingCartService", "RemoveLine", removeLine);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { Visitor = visitor, ActiveCart = shoppingCart }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.ShoppingCart UpdateLineQuantity(CompanyGroup.Dto.ServiceRequest.UpdateLineQuantity request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateLineQuantity([Bind(Prefix = "")] Cms.Webshop.Models.UpdateLineQuantity request)
        {
            CompanyGroup.Dto.ServiceRequest.UpdateLineQuantity updateLineQuantity = new CompanyGroup.Dto.ServiceRequest.UpdateLineQuantity(this.ReadCartIdFromCookie(), request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions shoppingCart = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartAndLeasingOptions>("ShoppingCartService", "UpdateLineQuantity", updateLineQuantity);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return Json(new { Visitor = visitor, ActiveCart = shoppingCart }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
        }

        [HttpPost]
        public JsonResult SaveShoppingCartOpenStatus([Bind(Prefix = "")] Cms.Webshop.Models.ShoppingCartOpenStatus request)
        {
            this.WriteShoppingCartOpenedToCookie(request.IsOpen);

            return Json(request.IsOpen, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ReadShoppingCartOpenStatus()
        {
            return Json(this.ReadShoppingCartOpenedFromCookie(), "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult SaveCatalogueOpenStatus([Bind(Prefix = "")] Cms.Webshop.Models.CatalogueOpenStatus request)
        {
            this.WriteCatalogueOpenedToCookie(request.IsOpen);

            return Json(request.IsOpen, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ReadCatalogueOpenStatus()
        {
            return Json(this.ReadCatalogueOpenedFromCookie(), "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
    }
}