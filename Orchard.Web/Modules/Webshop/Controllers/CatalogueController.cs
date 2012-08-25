using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    [Themed]
    public class CatalogueController : Cms.CommonCore.Controllers.HomeController 
    {
        /// <summary>
        /// view első betöltődés
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CompanyGroup.Dto.ServiceRequest.GetAllStructure allStructure = new CompanyGroup.Dto.ServiceRequest.GetAllStructure() 
                                                                               { 
                                                                                   ActionFilter = false, 
                                                                                   BargainFilter = false,
                                                                                   Category1IdList = new List<string>(),
                                                                                   Category2IdList = new List<string>(),
                                                                                   Category3IdList = new List<string>(),
                                                                                   HrpFilter = true,
                                                                                   BscFilter = true,
                                                                                   IsInNewsletterFilter = false, 
                                                                                   ManufacturerIdList = new List<string>(), 
                                                                                   NewFilter = false, 
                                                                                   StockFilter = false, 
                                                                                   TextFilter = String.Empty,
                                                                                   PriceFilter = "0",
                                                                                   PriceFilterRelation = "0", 
                                                                                   NameOrPartNumberFilter = ""
                                                                               };

            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", allStructure);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.GetAllProduct allProduct = new CompanyGroup.Dto.ServiceRequest.GetAllProduct() 
                                                                           { 
                                                                               ActionFilter = false, 
                                                                               BargainFilter = false, 
                                                                               Category1IdList = new List<string>(), 
                                                                               Category2IdList = new List<string>(), 
                                                                               Category3IdList = new List<string>(), 
                                                                               Currency = this.ReadCurrencyFromCookie(),
                                                                               CurrentPageIndex = 1, 
                                                                               HrpFilter = true,
                                                                               BscFilter = true,
                                                                               IsInNewsletterFilter = false,
                                                                               ItemsOnPage = 30, 
                                                                               ManufacturerIdList = new List<string>(), 
                                                                               NewFilter = false, 
                                                                               Sequence = 0, 
                                                                               StockFilter = false,
                                                                               TextFilter = String.Empty,
                                                                               PriceFilter = "0",
                                                                               PriceFilterRelation = "0",
                                                                               VisitorId = visitor.Id, 
                                                                               NameOrPartNumberFilter = ""
                                                                           };

            CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", allProduct);

            CompanyGroup.Dto.ServiceRequest.GetBannerList bannerListRequest = new CompanyGroup.Dto.ServiceRequest.GetBannerList()
                                                                              {
                                                                                  BscFilter = true,
                                                                                  HrpFilter = true,
                                                                                  Category1IdList = new List<string>(),
                                                                                  Category2IdList = new List<string>(),
                                                                                  Category3IdList = new List<string>(),
                                                                                  Currency = this.ReadCurrencyFromCookie(),
                                                                                  VisitorId = visitor.Id
                                                                              };

            CompanyGroup.Dto.WebshopModule.BannerList bannerList = this.PostJSonData<CompanyGroup.Dto.WebshopModule.BannerList>("ProductService", "GetBannerList", bannerListRequest); 

            bool shoppingCartOpenStatus = this.ReadShoppingCartOpenedFromCookie();

            bool catalogueOpenStatus = this.ReadCatalogueOpenedFromCookie();

            CompanyGroup.Dto.WebshopModule.ShoppingCart activeCart = (visitor.IsValidLogin) ? this.GetActiveCart() : new CompanyGroup.Dto.WebshopModule.ShoppingCart();

            Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = (visitor.IsValidLogin) ? this.GetStoredOpenedShoppingCartCollectionByVisitor() : new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

            CompanyGroup.Dto.PartnerModule.DeliveryAddresses deliveryAddresses;
            if (visitor.IsValidLogin)
            {
                CompanyGroup.Dto.ServiceRequest.GetDeliveryAddresses getDeliveryAddresses = new CompanyGroup.Dto.ServiceRequest.GetDeliveryAddresses() { DataAreaId = CatalogueController.DataAreaId, VisitorId = visitor.Id };

                deliveryAddresses = this.PostJSonData<CompanyGroup.Dto.PartnerModule.DeliveryAddresses>("CustomerService", "GetDeliveryAddresses", new { DataAreaId = CatalogueController.DataAreaId, VisitorId = visitor.Id });
            }
            else
            {
                deliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses() { Items = new List<CompanyGroup.Dto.PartnerModule.DeliveryAddress>() };
            }

            Cms.Webshop.Models.Catalogue catalogue = new Cms.Webshop.Models.Catalogue(structures, products, visitor, storedOpenedCarts.StoredItems, storedOpenedCarts.OpenedItems, activeCart, shoppingCartOpenStatus, catalogueOpenStatus, deliveryAddresses, bannerList);

            return View("Index", catalogue);
        }

        [HttpPost]
        public JsonResult SignInCatalogue([Bind(Prefix = "")] Cms.CommonCore.Models.SignIn request)
        {
            try
            {
                CompanyGroup.Helpers.DesignByContract.Require((request != null), "SignIn request can not be null!");

                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(request.Password), "A jelszó megadása kötelező!");

                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(request.UserName), "A belépési név megadása kötelező!");

                CompanyGroup.Dto.ServiceRequest.SignIn signIn = new CompanyGroup.Dto.ServiceRequest.SignIn()
                {
                    DataAreaId = CatalogueController.DataAreaId,
                    IPAddress = this.Request.UserHostAddress,
                    Password = request.Password,
                    UserName = request.UserName,
                    ObjectId = this.ReadPermanentIdFromCookie()
                };

                Cms.CommonCore.Models.Visitor visitor = this.PostJSonData<Cms.CommonCore.Models.Visitor>("CustomerService", "SignIn", signIn);

                CompanyGroup.Dto.WebshopModule.ShoppingCart activeCart = null;

                Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = null;

                CompanyGroup.Dto.PartnerModule.DeliveryAddresses deliveryAddresses;

                //check status
                if (!visitor.IsValidLogin)
                {
                    visitor.ErrorMessage = "A bejelentkezés nem sikerült!";

                    activeCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart();

                    storedOpenedCarts = new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

                    deliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses() { Items = new List<CompanyGroup.Dto.PartnerModule.DeliveryAddress>() };
                }
                else    //SignIn process, set http cookie, etc...
                {
                    CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(visitor.Id), "A bejelentkezés nem sikerült! (üres azonosító)");

                    CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(visitor.CompanyId), "A bejelentkezés nem sikerült! (üres cégazonosító)");

                    //visitor adatok http sütibe írása     
                    this.WriteCookie(new Cms.CommonCore.Models.VisitorData(visitor.Id, visitor.LanguageId, false, false, visitor.Currency, visitor.Id));

                    visitor.ErrorMessage = String.Empty;

                    activeCart = this.GetActiveCart();

                    storedOpenedCarts = this.GetStoredOpenedShoppingCartCollectionByVisitor();

                    deliveryAddresses = this.PostJSonData<CompanyGroup.Dto.PartnerModule.DeliveryAddresses>("CustomerService", "GetDeliveryAddresses", new { DataAreaId = CatalogueController.DataAreaId, VisitorId = visitor.Id });
                }

                CompanyGroup.Dto.ServiceRequest.GetAllProduct allProduct = new CompanyGroup.Dto.ServiceRequest.GetAllProduct()
                {
                    ActionFilter = false,
                    BargainFilter = false,
                    Category1IdList = new List<string>(),
                    Category2IdList = new List<string>(),
                    Category3IdList = new List<string>(),
                    Currency = visitor.Currency, 
                    CurrentPageIndex = 1,
                    HrpFilter = true,
                    BscFilter = true,
                    IsInNewsletterFilter = false, 
                    ItemsOnPage = 20,
                    ManufacturerIdList = new List<string>(),
                    NewFilter = false,
                    Sequence = 2,
                    StockFilter = false,
                    TextFilter = "",
                    PriceFilter = "0", 
                    PriceFilterRelation = "0",
                    VisitorId = visitor.Id,
                    NameOrPartNumberFilter = ""
                };

                CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", allProduct);

                return Json(new { Products = products, 
                                  Visitor = visitor, 
                                  StoredCarts = storedOpenedCarts.StoredItems, 
                                  OpenedCarts = storedOpenedCarts.OpenedItems, 
                                  ActiveCart = activeCart,
                                  ShoppingCartOpenStatus = false,
                                  CatalogueOpenStatus = false,
                                  DeliveryAddresses = deliveryAddresses
                }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
            }
            catch (CompanyGroup.Helpers.DesignByContractException ex)
            {
                Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

                return Json(new
                {
                    Products = new CompanyGroup.Dto.WebshopModule.Products(),
                    Visitor = new Cms.CommonCore.Models.Visitor() { ErrorMessage = ex.Message },
                    StoredCarts = storedOpenedCarts.StoredItems,
                    OpenedCarts = storedOpenedCarts.OpenedItems,
                    ActiveCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart(),
                    ShoppingCartOpenStatus = false,
                    CatalogueOpenStatus = false, 
                    DeliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses()
                }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
            }
            catch 
            {
                Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

                return Json(new
                {
                    Products = new CompanyGroup.Dto.WebshopModule.Products(),
                    Visitor = new Cms.CommonCore.Models.Visitor() { ErrorMessage = "A bejelentkezés nem sikerült! (hiba)" },
                    StoredCarts = storedOpenedCarts.StoredItems,
                    OpenedCarts = storedOpenedCarts.OpenedItems,
                    ActiveCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart(),
                    ShoppingCartOpenStatus = false,
                    CatalogueOpenStatus = false,
                    DeliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses()
                }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
            }
        }

        [HttpPost]
        public JsonResult SignOutCatalogue([Bind(Prefix = "")] Cms.CommonCore.Models.SignOut request)
        {
            try
            {
                Cms.CommonCore.Models.Empty empty = this.PostJSonData<Cms.CommonCore.Models.Empty>("CustomerService", "SignOut", new CompanyGroup.Dto.ServiceRequest.SignOut() { DataAreaId = CatalogueController.DataAreaId, ObjectId = this.ReadObjectIdFromCookie() });

                this.RemoveObjectIdFromCookie();

                Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = new CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

                CompanyGroup.Dto.ServiceRequest.GetAllProduct allProduct = new CompanyGroup.Dto.ServiceRequest.GetAllProduct()
                {
                    ActionFilter = false,
                    BargainFilter = false,
                    Category1IdList = new List<string>(),
                    Category2IdList = new List<string>(),
                    Category3IdList = new List<string>(),
                    Currency = this.ReadCurrencyFromCookie(), 
                    CurrentPageIndex = 1,
                    HrpFilter = true,
                    BscFilter = true,
                    IsInNewsletterFilter = false, 
                    ItemsOnPage = 20,
                    ManufacturerIdList = new List<string>(),
                    NewFilter = false,
                    Sequence = 2,
                    StockFilter = false,
                    TextFilter = "",
                    PriceFilter = "0",
                    PriceFilterRelation = "0", 
                    VisitorId = "", 
                    NameOrPartNumberFilter = ""
                };

                CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", allProduct);

                return Json(new
                {
                    Products = products,
                    Visitor = new Cms.CommonCore.Models.Visitor(),
                    StoredCarts = storedOpenedCarts.StoredItems,
                    OpenedCarts = storedOpenedCarts.OpenedItems,
                    ActiveCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart(),
                    ShoppingCartOpenStatus = false,
                    CatalogueOpenStatus = false,
                    DeliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses()
                }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
            }
            catch(Exception ex)
            {
                Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection storedOpenedCarts = new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();

                return Json(new
                {
                    Products = new CompanyGroup.Dto.WebshopModule.Products(),
                    Visitor = new Cms.CommonCore.Models.Visitor() { ErrorMessage = ex.Message },
                    StoredCarts = storedOpenedCarts.StoredItems,
                    OpenedCarts = storedOpenedCarts.OpenedItems,
                    ActiveCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart(),
                    ShoppingCartOpenStatus = false,
                    CatalogueOpenStatus = false,
                    DeliveryAddresses = new CompanyGroup.Dto.PartnerModule.DeliveryAddresses()
                }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);            
            }
        }

        /// <summary>
        /// struktúrák lekérdezése
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetStructure([Bind(Prefix = "")] CompanyGroup.Dto.ServiceRequest.GetAllStructure request)
        {
            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", request);

            Cms.Webshop.Models.Structures response = new Cms.Webshop.Models.Structures(structures);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// terméklista lekérdezése
        /// </summary>
        /// <param name="request"></param>
        /// <returns>terméklista objektum és a látogató objektum JSON formátumban</returns>
        [HttpPost]
        public JsonResult GetProducts([Bind(Prefix = "")] CompanyGroup.Dto.ServiceRequest.GetAllProduct request) //CompanyGroup.Dto.ServiceRequest.CatalogueFilter
        {
            Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

            request.VisitorId = visitorData.ObjectId;   //this.ReadObjectIdFromCookie();

            request.Currency = visitorData.Currency;    // this.ReadCurrencyFromCookie();

            request.NameOrPartNumberFilter = request.NameOrPartNumberFilter ?? String.Empty;

            CompanyGroup.Dto.WebshopModule.Products products = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Products>("ProductService", "GetAll", request);

            return Json(new { Products = products, Visitor = this.GetVisitorInfo() }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// részletes termék adatlap
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return Details(CompanyGroup.Helpers.QueryStringParser.GetString("ProductId"));
        }

        /// <summary>
        /// részletes termék adatlap
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string ProductId)
        {
            if (String.IsNullOrWhiteSpace(ProductId)) { ProductId = "PGI7BK"; }

            CompanyGroup.Dto.ServiceRequest.GetAllStructure allStructure = new CompanyGroup.Dto.ServiceRequest.GetAllStructure();

            CompanyGroup.Dto.WebshopModule.Structures structures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Structures>("StructureService", "GetAll", allStructure);

            CompanyGroup.Dto.ServiceRequest.GetItemByProductId request = new CompanyGroup.Dto.ServiceRequest.GetItemByProductId() 
                                                                                  { 
                                                                                      ProductId = ProductId, 
                                                                                      DataAreaId = CatalogueController.DataAreaId, 
                                                                                      VisitorId = this.ReadObjectIdFromCookie(), 
                                                                                      Currency = this.ReadCurrencyFromCookie()
                                                                                  };

            CompanyGroup.Dto.WebshopModule.Product product = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Product>("ProductService", "GetItemByProductId", request);

            CompanyGroup.Dto.WebshopModule.CompatibleProducts compatibleProducts = this.PostJSonData<CompanyGroup.Dto.WebshopModule.CompatibleProducts>("ProductService", "GetCompatibleProducts", request);

            Cms.CommonCore.Models.Visitor visitor = this.GetVisitorInfo();

            CompanyGroup.Dto.ServiceRequest.GetBannerList bannerListRequest = new CompanyGroup.Dto.ServiceRequest.GetBannerList()
            {
                BscFilter = true,
                HrpFilter = true,
                Category1IdList = new List<string>() { product.FirstLevelCategory.Id },
                Category2IdList = new List<string>() { product.SecondLevelCategory.Id },
                Category3IdList = new List<string>() { product.ThirdLevelCategory.Id },
                Currency = this.ReadCurrencyFromCookie(),
                VisitorId = visitor.Id
            };

            CompanyGroup.Dto.WebshopModule.BannerList bannerList = this.PostJSonData<CompanyGroup.Dto.WebshopModule.BannerList>("ProductService", "GetBannerList", bannerListRequest);

            return View("Details", new Cms.Webshop.Models.CatalogueItem(structures, product, compatibleProducts.Items, compatibleProducts.ReverseItems, visitor, bannerList));
        }

        [HttpGet]
        public FileStreamResult PictureItem()
        { 
            string productId = CompanyGroup.Helpers.QueryStringParser.GetString("ProductId"); 

            string recId = CompanyGroup.Helpers.QueryStringParser.GetString("RecId");

            string dataAreaId = CompanyGroup.Helpers.QueryStringParser.GetString("DataAreaId");

            string maxWidth = CompanyGroup.Helpers.QueryStringParser.GetString("MaxWidth");

            string maxHeight = CompanyGroup.Helpers.QueryStringParser.GetString("MaxHeight");

            return Picture(productId, recId, dataAreaId, maxWidth, maxHeight);
        }

        /// <summary>
        /// kép lekérdezése stream-be
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="recId"></param>
        /// <param name="dataAreaId"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult Picture(string productId, string recId, string dataAreaId, string maxWidth, string maxHeight)
        {
            byte[] picture = this.DownloadData("PictureService", String.Format("GetItem/{0}/{1}/{2}/{3}/{4}", productId, recId, dataAreaId, maxWidth, maxHeight));

            return new FileStreamResult(new MemoryStream(picture), "image/jpeg");
        }

        /// <summary>
        /// termékhez tartozó képek lekérdezése
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListByProduct([Bind(Prefix = "")] CompanyGroup.Dto.ServiceRequest.PictureFilter request)
        {
            request.DataAreaId = CatalogueController.DataAreaId;

            CompanyGroup.Dto.WebshopModule.Pictures pictures = this.PostJSonData<CompanyGroup.Dto.WebshopModule.Pictures>("PictureService", "GetListByProduct", request);

            return Json(new { Items = pictures.Items }, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public ActionResult DownloadPriceList() //CompanyGroup.Dto.ServiceRequest.CatalogueFilter
        {
            /*
+		Request.QueryString	{ManufacturerIdList%5b%5d=A004&ActionFilter=false&BargainFilter=false&NewFilter=false&StockFilter=false&TextFilter=&HrpFilter=true&BscFilter=true&PriceFilter=0&PriceFilterRelation=0&NameOrPartNumberFilter=&Sequence=0&CurrentPageIndex=1&ItemsOnPage=30&Clear=undefined}	System.Collections.Specialized.NameValueCollection {System.Web.HttpValueCollection}
             
             */
            CompanyGroup.Dto.ServiceRequest.GetPriceList request = new CompanyGroup.Dto.ServiceRequest.GetPriceList()
                                                                       {
                                                                           ActionFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("ActionFilter", false),
                                                                           BargainFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("BargainFilter", false),
                                                                           BscFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("BscFilter", true),
                                                                           Category1IdList = ConvertDelimitedStringToList(CompanyGroup.Helpers.QueryStringParser.GetString("Category1IdList[]", "")),
                                                                           Category2IdList = ConvertDelimitedStringToList(CompanyGroup.Helpers.QueryStringParser.GetString("Category2IdList[]", "")),
                                                                           Category3IdList = ConvertDelimitedStringToList(CompanyGroup.Helpers.QueryStringParser.GetString("Category3IdList[]", "")),
                                                                           Currency = this.ReadCurrencyFromCookie(),
                                                                           HrpFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("HrpFilter", true),
                                                                           IsInNewsletterFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("IsInNewsletterFilter", false),
                                                                           ManufacturerIdList = ConvertDelimitedStringToList(CompanyGroup.Helpers.QueryStringParser.GetString("ManufacturerIdList[]", "")),
                                                                           NameOrPartNumberFilter = CompanyGroup.Helpers.QueryStringParser.GetString("NameOrPartNumberFilter", ""),
                                                                           NewFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("NewFilter", false),
                                                                           PriceFilter = CompanyGroup.Helpers.QueryStringParser.GetString("PriceFilter", ""),
                                                                           PriceFilterRelation = CompanyGroup.Helpers.QueryStringParser.GetString("PriceFilterRelation", ""),
                                                                           Sequence = CompanyGroup.Helpers.QueryStringParser.GetInt("Sequence", 0),
                                                                           StockFilter = CompanyGroup.Helpers.QueryStringParser.GetBool("StockFilter", false),
                                                                           TextFilter = CompanyGroup.Helpers.QueryStringParser.GetString("TextFilter", ""),
                                                                           VisitorId = this.ReadObjectIdFromCookie()
                                                                       };


            request.NameOrPartNumberFilter = request.NameOrPartNumberFilter ?? String.Empty;

            CompanyGroup.Dto.WebshopModule.PriceList priceList = this.PostJSonData<CompanyGroup.Dto.WebshopModule.PriceList>("ProductService", "GetPriceList", request);

            System.Data.DataSet ds = CreateDataSet(priceList);

            MemoryStream ms = new MemoryStream();

            ExcelLibrary.DataSetHelper.CreateWorkbook(ms, ds);

            string fileDownloadName = "pricelist.xls";

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", fileDownloadName));

            ms.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();

            return new EmptyResult();
            //return File(ms, "application/vnd.ms-excel", fileDownloadName);
        }

        private System.Data.DataSet CreateDataSet(CompanyGroup.Dto.WebshopModule.PriceList from)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("CannotCancel", typeof(bool));
            dt.Columns.Add("Currency", typeof(string));
            dt.Columns.Add("DataAreaId", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("EndOfSales", typeof(bool));
            dt.Columns.Add("FirstLevelCategoryId", typeof(string));
            dt.Columns.Add("FirstLevelCategoryName", typeof(string));
            dt.Columns.Add("GarantyMode", typeof(string));
            dt.Columns.Add("GarantyTime", typeof(string));
            dt.Columns.Add("InnerStock", typeof(int));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ManufacturerId", typeof(string));
            dt.Columns.Add("ManufacturerName", typeof(string));
            dt.Columns.Add("New", typeof(bool));
            dt.Columns.Add("OuterStock", typeof(int));
            dt.Columns.Add("PartNumber", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("PurchaseInProgress", typeof(bool));
            dt.Columns.Add("SecondLevelCategoryId", typeof(string));
            dt.Columns.Add("SecondLevelCategoryName", typeof(string));
            dt.Columns.Add("ShippingDate", typeof(DateTime));
            dt.Columns.Add("ThirdLevelCategoryId", typeof(string));
            dt.Columns.Add("ThirdLevelCategoryName", typeof(string));

            foreach (CompanyGroup.Dto.WebshopModule.PriceListItem item in from.Items)
            {
                System.Data.DataRow row = dt.NewRow();

                row["CannotCancel"] = item.CannotCancel;
                row["Currency"] = item.Currency;
                row["DataAreaId"] = item.DataAreaId;
                row["Description"] = item.Description;
                row["EndOfSales"] = item.EndOfSales;
                row["FirstLevelCategoryId"] = item.FirstLevelCategory.Id;
                row["FirstLevelCategoryName"] = item.FirstLevelCategory.Name;
                row["GarantyMode"] = item.GarantyMode;
                row["GarantyTime"] = item.GarantyTime;
                row["InnerStock"] = item.InnerStock;
                row["ItemName"] = item.ItemName;
                row["ManufacturerId"] = item.Manufacturer.Id;
                row["ManufacturerName"] = item.Manufacturer.Name;
                row["New"] = item.New;
                row["OuterStock"] = item.OuterStock;
                row["PartNumber"] = item.PartNumber;
                row["Price"] = item.Price;
                row["ProductId"] = item.ProductId;
                row["PurchaseInProgress"] = item.PurchaseInProgress;
                row["SecondLevelCategoryId"] = item.SecondLevelCategory.Id;
                row["SecondLevelCategoryName"] = item.SecondLevelCategory.Name;
                row["ShippingDate"] = item.ShippingDate;
                row["ThirdLevelCategoryId"] = item.ThirdLevelCategory.Id;
                row["ThirdLevelCategoryName"] = item.ThirdLevelCategory.Name;

                dt.Rows.Add(row);
            }

            System.Data.DataSet ds = new System.Data.DataSet();

            ds.Tables.Add(dt);

            return ds;
        }

        private List<string> ConvertDelimitedStringToList(string s)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    return new List<string>();
                }

                List<string> list = new List<string>();

                foreach (string item in s.Split(','))
                {
                    list.Add(item);
                }

                return list;
            }
            catch
            {
                return new List<string>();
            }
        }

    }
}