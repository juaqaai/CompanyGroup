using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class CheckoutController : Cms.CommonCore.Controllers.HomeController
    {
        public ActionResult Index()
        {
            ViewData["InitialProductList"] = new CompanyGroup.Dto.WebshopModule.Products() { Items = new List<CompanyGroup.Dto.WebshopModule.Product>(), ListCount = 0, Pager = new CompanyGroup.Dto.WebshopModule.Pager() };

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            ViewData["VisitorInfo"] = visitor;

            Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts;

            if (visitor.IsValidLogin)
            {
                ViewData["ActiveCart"] = this.GetActiveCart(visitor);

                storedOpenedCarts = this.GetStoredOpenedShoppingCartCollectionByVisitor(visitor);
            }
            else
            {
                ViewData["ActiveCart"] = new CompanyGroup.Dto.WebshopModule.ShoppingCart();

                storedOpenedCarts = new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();
            }

            ViewData["StoredCarts"] = storedOpenedCarts.StoredItems;

            ViewData["OpenedCarts"] = storedOpenedCarts.OpenedItems;

            ViewData["ShoppingCartOpenStatus"] = this.ReadShoppingCartOpenedFromCookie();

            ViewData["CatalogueOpenStatus"] = this.ReadCatalogueOpenedFromCookie();

            return View("Index");
        }
    }
}