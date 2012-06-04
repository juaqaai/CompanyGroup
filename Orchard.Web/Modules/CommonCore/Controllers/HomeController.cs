using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Web.Mvc;
using Orchard.Themes;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Cms.CommonCore.Controllers
{
    /// <summary>
    /// Cms base controller
    /// - PostJSonData
    /// - GetJSonData
    /// - GetVisitorInfo
    /// - ReadObjectIdFromCookie
    /// - WriteObjectIdToCookie
    /// </summary>
    [Themed]
    public class HomeController : System.Web.Mvc.Controller
    {
        private readonly static string ServiceBaseUrl = CompanyGroup.Helpers.ConfigSettingsParser.GetString("ServiceBaseUrl", "http://1Juhasza/CompanyGroup.ServicesHost/{0}.svc/");

        protected readonly static string DataAreaId = CompanyGroup.Helpers.ConfigSettingsParser.GetString("DataAreaId", "hrp");

        protected readonly static string CookieName = CompanyGroup.Helpers.ConfigSettingsParser.GetString("CookieName", "companygroup");

        protected readonly static string LanguageEnglish = CompanyGroup.Helpers.ConfigSettingsParser.GetString("LanguageEnglish", "en");

        protected readonly static string LanguageHungarian = CompanyGroup.Helpers.ConfigSettingsParser.GetString("LanguageHungarian", "hu");

        private static string BaseUrl(string serviceName)
        {
            return String.Format(ServiceBaseUrl, serviceName);
        }

        #region "RestSharp (T PostJSonData<T>(string serviceName, string resource, object requestBody) where T : new()), T GetJSonData<T>(string serviceName, string resource) where T : new(), byte[] DownloadData(string serviceName, string resource))"

        /// <summary>
        /// post json data to an application service url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">Catalogue</param>
        /// <param name="resource">GetStructures</param>
        /// <param name="requestBody">new { DataAreaId = dataAreaId, ActionFilter = false, BargainFilter = false, NewFilter = false, StockFilter = false, TextFilter = textFilter }</param>
        /// <returns></returns>
        protected T PostJSonData<T>(string serviceName, string resource, object requestBody) where T : new()
        {
            
            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(serviceName), "Service name can not be null or empty!");

            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(resource), "Resource name can not be null or empty!");

            CompanyGroup.Helpers.DesignByContract.Require((requestBody != null), "RequestBody can not be null!");

            RestSharp.RestClient client = null;

            try
            {
                client = new RestSharp.RestClient(BaseUrl(serviceName));

                RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.POST);

                request.RequestFormat = RestSharp.DataFormat.Json;

                request.Resource = resource;

                request.AddBody(requestBody);

                RestSharp.RestResponse<T> response = client.Execute<T>(request);

                return response.Data;
            }
            catch { return default(T); }
        }

        /// <summary>
        /// retriewe json data from an application service url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        /// <example>http://localhost/CompanyGroup.ServicesHost/PictureService.svc/GetItem/5637193425/PFI702GY/hrp</example>
        protected T GetJSonData<T>(string serviceName, string resource) where T : new()
        {
            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(serviceName), "Service name can not be null or empty!");

            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(resource), "Resource name can not be null or empty!");

            RestSharp.RestClient client = null;

            try
            {
                client = new RestSharp.RestClient(BaseUrl(serviceName));

                RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.GET);

                request.RequestFormat = RestSharp.DataFormat.Json;

                request.Resource = resource;
                //request.BaseUrl = "http://carma.org";
                //request.Action = "api/1.1/searchPlants";
                //request.AddParameter("location", 4338);
                //request.AddParameter("limit", 10);
                //request.AddParameter("color", "red");
                //request.AddParameter("format", "xml");
                //request.ResponseFormat = ResponseFormat.Xml;
                //var plants = client.Execute<PowerPlantsDTO>(request);
                RestSharp.RestResponse<T> response = client.Execute<T>(request);

                return response.Data;
            }
            catch { return default(T); }
        }

        /// <summary>
        /// retriewe raw byte array data from an application service url
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        protected byte[] DownloadData(string serviceName, string resource)
        { 
            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(serviceName), "Service name can not be null or empty!");

            CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(resource), "Resource name can not be null or empty!");

            RestSharp.RestClient client = null;

            try
            {
                client = new RestSharp.RestClient(BaseUrl(serviceName));

                RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.GET);

                request.Resource = resource;

                return client.DownloadData(request);
            }
            catch 
            {
                return new byte[] {};
            }
        }

        #endregion

        #region "JsonResult SignIn([Bind(Prefix = "")] Cms.CommonCore.Models.SignIn request), JsonResult SignOut([Bind(Prefix = "")] Cms.CommonCore.Models.SignOut request), JsonResult VisitorInfo(), Cms.CommonCore.Models.Visitor GetVisitorInfo()"

        /// <summary>
        /// Enter into the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignIn([Bind(Prefix = "")] Cms.CommonCore.Models.SignIn request)
        {
            try
            {
                CompanyGroup.Helpers.DesignByContract.Require((request != null), "SignIn request can not be null!");

                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(request.Password), "A jelszó megadása kötelező!");

                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(request.UserName), "A belépési név megadása kötelező!");

                CompanyGroup.Dto.ServiceRequest.SignIn signIn = new CompanyGroup.Dto.ServiceRequest.SignIn() { DataAreaId = HomeController.DataAreaId, 
                                                                                                               IPAddress = this.Request.UserHostAddress, 
                                                                                                               Password = request.Password, 
                                                                                                               UserName = request.UserName  };

                Cms.CommonCore.Models.Visitor visitor = this.PostJSonData<Cms.CommonCore.Models.Visitor>("CustomerService", "SignIn", signIn);

                //check status
                if (!visitor.LoggedIn)
                {
                    visitor.ErrorMessage = "A bejelentkezés nem sikerült!";
                }
                else    //SignIn process, set http cookie, etc...
                {
                    CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(visitor.Id), "A bejelentkezés nem sikerült! (üres azonosító)");

                    CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(visitor.CompanyId), "A bejelentkezés nem sikerült! (üres cégazonosító)");

                    this.WriteObjectIdToCookie(visitor.Id);

                    visitor.ErrorMessage = String.Empty;
                }
                return Json(visitor);
            }
            catch (CompanyGroup.Helpers.DesignByContractException ex)
            {
                return Json(new Cms.CommonCore.Models.Visitor() { ErrorMessage = ex.Message });
            }
            catch { return Json(new Cms.CommonCore.Models.Visitor() { ErrorMessage = "A bejelentkezés nem sikerült! (hiba)" }); }
        }

        /// Sign out from the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignOut([Bind(Prefix = "")] Cms.CommonCore.Models.SignOut request)
        {
            Cms.CommonCore.Models.Empty empty = this.PostJSonData<Cms.CommonCore.Models.Empty>("CustomerService", "SignOut", new CompanyGroup.Dto.ServiceRequest.SignOut() { DataAreaId = HomeController.DataAreaId, ObjectId = request.ObjectId });

            this.RemoveObjectIdFromCookie();

            return Json(empty, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// retrieve visitor information
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult VisitorInfo()
        {
            Cms.CommonCore.Models.Visitor visitor = GetVisitorInfo();

            return Json(visitor, "application/json; charset=utf-8", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// retrieve visitor information from the application layer
        /// </summary>
        /// <returns></returns>
        protected Cms.CommonCore.Models.Visitor GetVisitorInfo()
        {
            try
            {
                string objectId = ReadObjectIdFromCookie();

                if (String.IsNullOrWhiteSpace(objectId))
                {
                    return new Cms.CommonCore.Models.Visitor();
                }

                Cms.CommonCore.Models.Visitor visitor = this.PostJSonData<Cms.CommonCore.Models.Visitor>("VisitorService", "GetVisitorInfo", new { ObjectId = objectId, DataAreaId = HomeController.DataAreaId });

                visitor.ErrorMessage = String.Empty;

                return visitor;
            }
            catch (Exception ex) { return new Cms.CommonCore.Models.Visitor() { ErrorMessage = ex.Message }; }
        }

        #endregion

        #region "private cookie funkciók - ReadCookie, WriteCookie"

        /// <summary>
        /// visitor adatok kiolvasása http cookie-ból (string -> Json conversion)
        /// </summary>
        /// <returns></returns>
        private Cms.CommonCore.Models.VisitorData ReadCookie()
        {
            try
            {
                System.Web.HttpCookie cookie = this.Request.Cookies.Get(HomeController.CookieName);

                if (cookie == null) { return new Cms.CommonCore.Models.VisitorData(); }

                return CompanyGroup.Helpers.JsonConverter.FromJSON<Cms.CommonCore.Models.VisitorData>(cookie.Value);
            }
            catch { return new Cms.CommonCore.Models.VisitorData(); }
        }

        /// <summary>
        /// visitor adatok mentése http cookie-ba
        /// </summary>
        /// <param name="visitorData"></param>
        private void WriteCookie(Cms.CommonCore.Models.VisitorData visitorData)
        {
            try
            {
                CompanyGroup.Helpers.DesignByContract.Require((visitorData != null), "Object can not be null or empty!");

                //konverzió json string-be
                string json = CompanyGroup.Helpers.JsonConverter.ToJSON<Cms.CommonCore.Models.VisitorData>(visitorData);

                System.Web.HttpCookie cookie = this.Request.Cookies.Get(HomeController.CookieName);

                //ha nincs cookie, akkor létrehozásra kerül
                if (cookie == null)
                {
                    this.Response.Cookies.Add(new System.Web.HttpCookie(HomeController.CookieName, json) { Expires = DateTime.Now.AddDays(30d) });
                }
                else
                {
                    //van cookie, értékadás, lejárati dátum beállítás történik
                    cookie.Value = json;

                    cookie.Expires = DateTime.Now.AddDays(30d);

                    this.Response.Cookies.Set(cookie);
                }
            }
            catch { }
        }

        #endregion

        #region "ObjectId cookie functions (string ReadObjectIdFromCookie(), void WriteObjectIdToCookie(string objectId), void RemoveObjectIdFromCookie())"

        /// <summary>
        /// read objectId string from http cookie
        /// </summary>
        /// <returns></returns>
        protected string ReadObjectIdFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                return visitorData.ObjectId;
            }
            catch { return String.Empty; }
        }

        /// <summary>
        /// write the objectId string to http cookie (if the named cookie exists than existing cookie will be used, otherwise new cookie will be created)
        /// The cookie expiring date is: current date + 30 day 
        /// </summary>
        /// <param name="objectId"></param>
        protected void WriteObjectIdToCookie(string objectId)
        {
            try
            {
                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(objectId), "ObjectId can not be null or empty!");

                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.ObjectId = objectId;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        /// <summary>
        /// delete objectId value from the http cookie
        /// </summary>
        /// <param name="objectId"></param>
        protected void RemoveObjectIdFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.ObjectId = String.Empty;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        #endregion

        #region "Language cookie functions (string ReadLanguageFromCookie(), void WriteLanguageToCookie(string language), void RemoveLanguageFromCookie())"

        /// <summary>
        /// read language string from http cookie
        /// </summary>
        /// <returns></returns>
        protected string ReadLanguageFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                return visitorData.Language;
            }
            catch { return String.Empty; }
        }

        /// <summary>
        /// write the language string to http cookie (if the named cookie exists than existing cookie will be used, otherwise new cookie will be created)
        /// The cookie expiring date is: current date + 30 day 
        /// </summary>
        /// <param name="objectId"></param>
        protected void WriteLanguageToCookie(string language)
        {
            try
            {
                CompanyGroup.Helpers.DesignByContract.Require(!String.IsNullOrWhiteSpace(language), "Language can not be null or empty!");

                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.Language = language;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        /// <summary>
        /// delete language value from the http cookie
        /// </summary>
        /// <param name="objectId"></param>
        protected void RemoveLanguageFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.Language = String.Empty;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        #endregion

        #region "IsShoppingCartOpened cookie functions (string ReadIsShoppingCartOpenedFromCookie(), void WriteIsShoppingCartOpenedToCookie(string language), void RemoveIsShoppingCartOpenedFromCookie())"

        /// <summary>
        /// read IsCartOpened value from http cookie
        /// </summary>
        /// <returns></returns>
        protected bool ReadShoppingCartOpenedFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                return visitorData.IsShoppingCartOpened;
            }
            catch { return false; }
        }

        /// <summary>
        /// write the IsShoppingCartOpened value to http cookie (if the named cookie exists than existing cookie will be used, otherwise new cookie will be created)
        /// The cookie expiring date is: current date + 30 day 
        /// </summary>
        /// <param name="objectId"></param>
        protected void WriteShoppingCartOpenedToCookie(bool isShoppingCartOpened)
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.IsShoppingCartOpened = isShoppingCartOpened;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        /// <summary>
        /// delete isShoppingCartOpened value from the http cookie
        /// </summary>
        /// <param name="objectId"></param>
        protected void RemoveShoppingCartOpenedFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.IsShoppingCartOpened = false;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        #endregion

        #region "IsCatalogueOpened cookie functions (string ReadIsCatalogueOpenedFromCookie(), void WriteIsCatalogueOpenedToCookie(string language), void RemoveIsCatalogueOpenedFromCookie())"

        /// <summary>
        /// read IsCatalogueOpened value from http cookie
        /// </summary>
        /// <returns></returns>
        protected bool ReadCatalogueOpenedFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                return visitorData.IsCatalogueOpened;
            }
            catch { return false; }
        }

        /// <summary>
        /// write the IsCatalogueOpened value to http cookie (if the named cookie exists than existing cookie will be used, otherwise new cookie will be created)
        /// The cookie expiring date is: current date + 30 day 
        /// </summary>
        /// <param name="objectId"></param>
        protected void WriteCatalogueOpenedToCookie(bool isCatalogueOpened)
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.IsCatalogueOpened = isCatalogueOpened;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        /// <summary>
        /// delete IsCatalogueOpened value from the http cookie
        /// </summary>
        /// <param name="objectId"></param>
        protected void RemoveCatalogueOpenedFromCookie()
        {
            try
            {
                Cms.CommonCore.Models.VisitorData visitorData = this.ReadCookie();

                visitorData.IsCatalogueOpened = false;

                this.WriteCookie(visitorData);
            }
            catch { }
        }

        #endregion

        #region "ShoppingCart"

        /// <summary>
        /// CompanyGroup.Dto.WebshopModule.StoredShoppingCartCollection GetStoredShoppingCartCollectionByVisitor(CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor request)
        /// </summary>
        /// <returns></returns>
        public Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection GetStoredOpenedShoppingCartCollectionByVisitor(Cms.CommonCore.Models.Visitor visitor)
        {
            try
            {
                if (visitor == null)
                {
                    return new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection();
                }

                CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor request = new CompanyGroup.Dto.ServiceRequest.GetCartCollectionByVisitor(visitor.LanguageId, visitor.CompanyId, visitor.PersonId);

                CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection>("ShoppingCartService", "GetStoredOpenedShoppingCartCollectionByVisitor", request);

                return new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection(response.StoredItems, response.OpenedItems);
            }
            catch (Exception ex) { return new Cms.CommonCore.Models.Response.StoredOpenedShoppingCartCollection() { ErrorMessage = ex.Message }; }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public Cms.CommonCore.Models.Response.ShoppingCart GetActiveCart(Cms.CommonCore.Models.Visitor visitor)
        {
            CompanyGroup.Dto.ServiceRequest.GetActiveCart request = new CompanyGroup.Dto.ServiceRequest.GetActiveCart(visitor.LanguageId, visitor.Id);

            CompanyGroup.Dto.WebshopModule.ShoppingCart response = this.PostJSonData<CompanyGroup.Dto.WebshopModule.ShoppingCart>("ShoppingCartService", "GetActiveCart", request);

            if (response == null) { response = new CompanyGroup.Dto.WebshopModule.ShoppingCart(); }

            return new Models.Response.ShoppingCart(response.Id, response.Items, response.SumTotal);
        }

        #endregion

    }
}
