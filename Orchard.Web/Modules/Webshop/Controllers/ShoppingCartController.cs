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
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCartByKey([Bind(Prefix = "")] Cms.Webshop.Models.GetCartByKey request)
        {
            CompanyGroup.Dto.ServiceRequest.GetCartByKey requestBody = new CompanyGroup.Dto.ServiceRequest.GetCartByKey(this.ReadLanguageFromCookie(), request.CartId, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "GetCartByKey", requestBody);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// látogatóhoz tartozó összes kosár lekérdezése
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCartCollectionByVisitorId([Bind(Prefix = "")] Cms.Webshop.Models.GetCartCollectionByVisitorId request)
        {
            CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor requestBody = new CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCartCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartCollection>("ShoppingCartService", "GetCartCollectionByVisitorId", requestBody);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult AddCart([Bind(Prefix = "")] Cms.Webshop.Models.AddCart request)
        {
            CompanyGroup.Dto.ServiceRequest.AddCart addCart = new CompanyGroup.Dto.ServiceRequest.AddCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie(), request.Name ?? String.Empty);

            CompanyGroup.Dto.ServiceResponse.AddCart response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.AddCart>("ShoppingCartService", "AddCart", addCart);

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            return Json(new { ActiveCart = response.ActiveCart, OpenedCarts = response.OpenedItems, StoredCarts = response.StoredItems, ShoppingCartOpenStatus = shoppingCartOpenStatus, CatalogueOpenStatus = catalogueOpenStatus }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ActivateCart([Bind(Prefix = "")] Cms.CommonCore.Models.Request.ActivateCart request)
        {
            CompanyGroup.Dto.ServiceRequest.ActivateCart activateCart = new CompanyGroup.Dto.ServiceRequest.ActivateCart(request.CartId, this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "ActivateCart", activateCart);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveCart()
        {
            CompanyGroup.Dto.ServiceRequest.RemoveCart request = new CompanyGroup.Dto.ServiceRequest.RemoveCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.ServiceResponse.RemoveCart response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.RemoveCart>("ShoppingCartService", "RemoveCart", request);

            //CompanyGroup.Dto.ServiceResponse.RemoveCart response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.RemoveCart>("ShoppingCartService", "GetStoredOpenedShoppingCartCollectionByVisitor", new CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor(request.Language, request.VisitorId));

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult SaveCart([Bind(Prefix = "")] Cms.Webshop.Models.SaveCart request)
        {
            CompanyGroup.Dto.ServiceRequest.SaveCart saveCart = new CompanyGroup.Dto.ServiceRequest.SaveCart(this.ReadLanguageFromCookie(), this.ReadObjectIdFromCookie(), request.CartId, request.Name);

            CompanyGroup.Dto.ServiceResponse.SaveCart response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.SaveCart>("ShoppingCartService", "SaveCart", saveCart);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
        }

        [HttpPost]
        public JsonResult AddLine([Bind(Prefix = "")] Cms.Webshop.Models.AddLine request)
        {
            CompanyGroup.Dto.ServiceRequest.AddLine addLine = new CompanyGroup.Dto.ServiceRequest.AddLine(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "AddLine", addLine);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveLine([Bind(Prefix = "")] Cms.Webshop.Models.RemoveLine request)
        {
            CompanyGroup.Dto.ServiceRequest.RemoveLine removeLine = new CompanyGroup.Dto.ServiceRequest.RemoveLine(request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "RemoveLine", removeLine);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult UpdateLineQuantity([Bind(Prefix = "")] Cms.Webshop.Models.UpdateLineQuantity request)
        {
            CompanyGroup.Dto.ServiceRequest.UpdateLineQuantity updateLineQuantity = new CompanyGroup.Dto.ServiceRequest.UpdateLineQuantity(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, this.ReadObjectIdFromCookie());

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "UpdateLineQuantity", updateLineQuantity);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
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