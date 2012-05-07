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

        [HttpPost]
        public JsonResult AddCart(Cms.Webshop.Models.NewShoppingCart request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.NewShoppingCart newShoppingCart = new CompanyGroup.Dto.ServiceRequest.NewShoppingCart(this.ReadLanguageFromCookie(), request.Name, visitor.Id);

            CompanyGroup.Dto.ServiceResponse.Empty response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.Empty>("ShoppingCartService", "AddCart", newShoppingCart);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveCart([Bind(Prefix = "")] Cms.Webshop.Models.DeleteShoppingCart request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.DeleteShoppingCart deleteShoppingCart = new CompanyGroup.Dto.ServiceRequest.DeleteShoppingCart(request.CartId, this.ReadLanguageFromCookie(), visitor.Id);

            CompanyGroup.Dto.ServiceResponse.Empty response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.Empty>("ShoppingCartService", "RemoveCart", deleteShoppingCart);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult AddItem([Bind(Prefix = "")] Cms.Webshop.Models.AddShoppingCartItem request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.NewShoppingCartItem newShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.NewShoppingCartItem(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, visitor.Id);

            CompanyGroup.Dto.ServiceResponse.Empty response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.Empty>("ShoppingCartService", "AddItem", newShoppingCartItem);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveItem([Bind(Prefix = "")] Cms.Webshop.Models.DeleteShoppingCartItem request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.DeleteShoppingCartItem deleteShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.DeleteShoppingCartItem(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, visitor.Id);

            CompanyGroup.Dto.ServiceResponse.Empty response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.Empty>("ShoppingCartService", "RemoveItem", deleteShoppingCartItem);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult UpdateItemQuantity(Cms.Webshop.Models.UpdateShoppingCartItem request)
        {
            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.UpdateShoppingCartItem updateShoppingCartItem = new CompanyGroup.Dto.ServiceRequest.UpdateShoppingCartItem(request.CartId, request.ProductId, this.ReadLanguageFromCookie(), ShoppingCartController.DataAreaId, request.Quantity, visitor.Id);

            CompanyGroup.Dto.ServiceResponse.Empty response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.Empty>("ShoppingCartService", "UpdateItemQuantity", updateShoppingCartItem);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);        
        }
    }
}