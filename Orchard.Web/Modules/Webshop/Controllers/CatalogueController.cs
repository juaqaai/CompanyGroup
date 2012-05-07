using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class CatalogueController : Cms.CommonCore.Controllers.HomeController //ControllerBase
    {
        public ActionResult Index()
        {
            CompanyGroup.Dto.ServiceRequest.StructureFilter structureFilter = new CompanyGroup.Dto.ServiceRequest.StructureFilter() 
                                                                                  { 
                                                                                      ActionFilter = false, 
                                                                                      BargainFilter = false,
                                                                                      Category1IdList = new List<string>(),
                                                                                      Category2IdList = new List<string>(),
                                                                                      Category3IdList = new List<string>(),
                                                                                      DataAreaId = CatalogueController.DataAreaId,
                                                                                      ManufacturerIdList = new List<string>(), 
                                                                                      NewFilter = false, 
                                                                                      StockFilter = false, 
                                                                                      TextFilter = "" 
                                                                                  };

            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", structureFilter);

            CompanyGroup.Dto.ServiceRequest.ProductFilter productFilter = new CompanyGroup.Dto.ServiceRequest.ProductFilter() 
                                                                              { 
                                                                                  ActionFilter = false, 
                                                                                  BargainFilter = false, 
                                                                                  Category1IdList = new List<string>(), 
                                                                                  Category2IdList = new List<string>(), 
                                                                                  Category3IdList = new List<string>(), 
                                                                                  CurrentPageIndex = 0, 
                                                                                  DataAreaId = CatalogueController.DataAreaId, 
                                                                                  ItemsOnPage = 20, 
                                                                                  ManufacturerIdList = new List<string>(), 
                                                                                  NewFilter = false, 
                                                                                  Sequence = 2, 
                                                                                  StockFilter = false, 
                                                                                  TextFilter = "" 
                                                                              };

            CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", productFilter);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            Cms.Webshop.Models.Catalogue catalogue = new Cms.Webshop.Models.Catalogue(structures, products, visitor);

            if (visitor.IsValidLogin)
            {
                //CompanyGroup.Dto.WebshopModule.ShoppingCartCollection GetItemsByVisitorId(string visitorId)
                CompanyGroup.Dto.WebshopModule.ShoppingCartCollection shoppingCartCollection = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCartCollection>("ShoppingCartService", "GetItemsByVisitorId", visitor.Id);

                CompanyGroup.Dto.WebshopModule.ShoppingCart activeCart = shoppingCartCollection.Carts.Find(x => x.Id.Equals(shoppingCartCollection.ActiveCartId)); //new Cms.Webshop.Models.ShoppingCartCollection();

                ViewData["ActiveCartItems"] = activeCart != null ? activeCart.Items : new List<CompanyGroup.Dto.WebshopModule.ShoppingCartItem>();
            }

            return View("Index", catalogue);
        }

        [HttpPost]
        public JsonResult GetStructure([Bind(Prefix = "")] CompanyGroup.Dto.ServiceRequest.StructureFilter request)
        {
            request.DataAreaId = CatalogueController.DataAreaId;

            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", request);

            Cms.Webshop.Models.Structures response = new Cms.Webshop.Models.Structures(structures);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public JsonResult GetProducts([Bind(Prefix = "")] CompanyGroup.Dto.ServiceRequest.ProductFilter request) //CompanyGroup.Dto.ServiceRequest.CatalogueFilter
        {
            request.DataAreaId = CatalogueController.DataAreaId;

            //CompanyGroup.Dto.ServiceRequest.ProductFilter productFilter = new CompanyGroup.Dto.ServiceRequest.ProductFilter() 
            //                                                                  { 
            //                                                                      ActionFilter = false, 
            //                                                                      BargainFilter = false, 
            //                                                                      Category1IdList = new List<string>(),
            //                                                                      Category2IdList = new List<string>(),
            //                                                                      Category3IdList = new List<string>(), 
            //                                                                      CurrentPageIndex = 0, 
            //                                                                      DataAreaId = CatalogueController.DataAreaId, 
            //                                                                      ItemsOnPage = 20,
            //                                                                      ManufacturerIdList = new List<string>(), 
            //                                                                      NewFilter = false, 
            //                                                                      Sequence = 2, 
            //                                                                      StockFilter = false, 
            //                                                                      TextFilter = "" 
            //                                                                  };

            CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", request);

            return Json(new { Items = products.Items, ListCount = products.ListCount, Pager = products.Pager, Visitor = this.GetVisitorInfo() }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// részletes termék adatlap
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string ProductId)
        {
            if (String.IsNullOrWhiteSpace(ProductId)) { ProductId = "PGI7BK"; }

            CompanyGroup.Dto.ServiceRequest.StructureFilter structureFilter = new CompanyGroup.Dto.ServiceRequest.StructureFilter();

            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", structureFilter);

            CompanyGroup.Dto.ServiceRequest.ProductItemFilter productFilter = new CompanyGroup.Dto.ServiceRequest.ProductItemFilter() { ProductId = ProductId, DataAreaId = CatalogueController.DataAreaId };

            CompanyGroup.Dto.WebshopModule.Product product = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Product>("ProductService", "GetItemByProductId", productFilter);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            return View("Details", new Cms.Webshop.Models.CatalogueItem( structures, product, visitor));
        }

        [WebshopAuthorizedAttribute(true, UserGroups = "WebAdministrator")]
        public void Test()
        { 
            
        }

    }
}