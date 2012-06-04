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
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.GetCartByKey requestBody = new CompanyGroup.Dto.ServiceRequest.GetCartByKey(this.ReadLanguageFromCookie(), request.CartId, visitor.Id);

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
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor requestBody = new CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor(this.ReadLanguageFromCookie(), visitor.CompanyId, visitor.PersonId);

            CompanyGroup.Dto.WebshopModule.ShoppingCartCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartCollection>("ShoppingCartService", "GetCartCollectionByVisitorId", requestBody);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult AddCart([Bind(Prefix = "")] Cms.Webshop.Models.NewShoppingCart request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.NewShoppingCart newShoppingCart = new CompanyGroup.Dto.ServiceRequest.NewShoppingCart(visitor.LanguageId, visitor.Id);

            CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection>("ShoppingCartService", "AddCart", newShoppingCart);

            CompanyGroup.Dto.WebshopModule.OpenedShoppingCart openedCart = response.OpenedItems.Find(x => x.Active);

            CompanyGroup.Dto.WebshopModule.StoredShoppingCart storedCart = response.StoredItems.Find(x => x.Active);

            string cartId = openedCart != null ? openedCart.Id : "";

            if (String.IsNullOrEmpty(cartId))
            {
                cartId = storedCart != null ? storedCart.Id : "";
            }

            return Json(new { Id = cartId, OpenedItems = response.OpenedItems, StoredItems = response.StoredItems }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ActivateCart([Bind(Prefix = "")] Cms.CommonCore.Models.Request.ActivateCart request)
        { 
             
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.ActivateCart requestBody = new CompanyGroup.Dto.ServiceRequest.ActivateCart(request.CartId, visitor.LanguageId, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "ActivateCart", requestBody);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveCart()
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.RemoveShoppingCart request = new CompanyGroup.Dto.ServiceRequest.RemoveShoppingCart(visitor.LanguageId, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart shoppingCart = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "RemoveCart", request);

            CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection storedOpenedShoppingCartCollection = this.PostJSonData<CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection>("ShoppingCartService", "GetStoredOpenedShoppingCartCollectionByVisitor", new CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor(visitor.LanguageId, visitor.CompanyId, visitor.PersonId));

            return Json(new { ShoppingCart = shoppingCart, OpenedItems = storedOpenedShoppingCartCollection.OpenedItems, StoredItems = storedOpenedShoppingCartCollection.StoredItems }, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        public JsonResult SaveCart([Bind(Prefix = "")] Cms.Webshop.Models.SaveShoppingCart request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.SaveShoppingCart saveShoppingCart = new CompanyGroup.Dto.ServiceRequest.SaveShoppingCart(visitor.LanguageId, visitor.Id, request.CartId, request.Name);

            CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection>("ShoppingCartService", "SaveCart", saveShoppingCart);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
        }

        [HttpPost]
        public JsonResult AddLine([Bind(Prefix = "")] Cms.Webshop.Models.AddShoppingCartLine request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.NewShoppingCartItem newShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.NewShoppingCartItem(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "AddLine", newShoppingCartItem);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveLine([Bind(Prefix = "")] Cms.Webshop.Models.DeleteShoppingCartItem request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.DeleteShoppingCartItem deleteShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.DeleteShoppingCartItem(request.CartId, request.ProductId, visitor.LanguageId, ShoppingCartController.DataAreaId, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "RemoveLine", deleteShoppingCartItem);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult UpdateLineQuantity([Bind(Prefix = "")] Cms.Webshop.Models.UpdateShoppingCartItem request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.UpdateShoppingCartItem updateShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.UpdateShoppingCartItem(request.CartId, request.ProductId, visitor.LanguageId, ShoppingCartController.DataAreaId, request.Quantity, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "UpdateLineQuantity", updateShoppingCartItem);

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