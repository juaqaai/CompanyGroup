using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.Webshop.Controllers
{
    /// <summary>
    /// Webshop controllers's base class
    /// </summary>
    [Themed]
    public class ControllerBase : System.Web.Mvc.Controller
    {
        private readonly static string ServiceBaseUrl = CompanyGroup.Helpers.ConfigSettingsParser.GetString("ServiceBaseUrl", "http://1Juhasza/CompanyGroup.ServicesHost/{0}.svc/");

        protected readonly static string DataAreaId = CompanyGroup.Helpers.ConfigSettingsParser.GetString("DataAreaId", "hrp");

        protected readonly static string CookieName = CompanyGroup.Helpers.ConfigSettingsParser.GetString("CookieName", "companygroup");

        private static string BaseUrl(string serviceName)
        {
            return String.Format(ServiceBaseUrl, serviceName);
        }

        /// <summary>
        /// post json data to service url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">Catalogue</param>
        /// <param name="resource">GetStructures</param>
        /// <param name="requestBody">new { DataAreaId = dataAreaId, ActionFilter = false, BargainFilter = false, NewFilter = false, StockFilter = false, TextFilter = textFilter }</param>
        /// <returns></returns>
        protected T PostJSonData<T>(string serviceName, string resource, object requestBody) where T : new()
        {
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
        /// get data from service url
        /// http://localhost/CompanyGroup.ServicesHost/PictureService.svc/GetItem/5637193425/PFI702GY/hrp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        protected T GetJSonData<T>(string serviceName, string resource) where T : new()
        {
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

        protected Cms.Webshop.Models.Visitor GetVisitorInfo()
        {
            string objectId = ReadObjectIdFromCookie(this.Request);

            if (String.IsNullOrEmpty(objectId))
            {
                return new Cms.Webshop.Models.Visitor();
            }

            Cms.Webshop.Models.Visitor visitor = this.PostJSonData<Cms.Webshop.Models.Visitor>("CustomerService", "GetVisitorInfo", new { ObjectId = objectId, DataAreaId = ControllerBase.DataAreaId });

            return visitor;
        }

        protected static string ReadObjectIdFromCookie(System.Web.HttpRequestBase request)
        {
            System.Web.HttpCookie cookie = request.Cookies.Get(CookieName); 

            if (cookie == null)
            {
                return String.Empty;
            }

            return cookie.Value;
        }

        public static void WriteObjectIdToCookie(System.Web.HttpRequestBase request, System.Web.HttpResponseBase response, string objectId)
        {
            try
            {
                if (String.IsNullOrEmpty(objectId)) { return; }

                System.Web.HttpCookie cookie = request.Cookies.Get(CookieName);
                if (cookie == null)
                {
                    response.Cookies.Add(new System.Web.HttpCookie(CookieName, objectId) { Expires = DateTime.Now.AddDays(30d) });
                }
                else
                {
                    cookie.Value = objectId;
                    response.Cookies.Set(cookie);
                }
            }
            catch { }
        }

    }
}
