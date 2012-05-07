using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Webshop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// konstruktorok ősosztálya címkézhető ezzel: [WebshopAuthorizedAttribute]
    /// ekkor az összes leszármazott authorizációt kér
    /// ebben az esetben viszont nem: [WebshopAuthorizedAttribute (false)]
    /// </remarks>
    public class WebshopAuthorizedAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private readonly bool _authorize;

        public string UserGroups;

        public WebshopAuthorizedAttribute()
        {
            _authorize = true;
        }

        public WebshopAuthorizedAttribute(bool authorize)
        {
            _authorize = authorize;
        }

        /// <summary>
        /// jogosultság ellenörzés
        /// 
        /// 1. megvizsgálja, hogy kell-e jogosultságot ellenőrizni
        /// 2. ha kell, akkor megvizsgálja a paraméter attribútum tömböt,
        ///     ha nincs eleme, akkor visszatér false-al
        ///     ha van eleme, akkor a bejelentkezett felhasználóhoz kiolvasásra kerülnek a beállított jogosultságok
        ///     ha a felhasynaloi jogosultsagnak és a kontroller (kontroller action)-nek van metszete, akkor true-val, 
        ///     ha nincs, akkor false-val tér vissza 
        /// <param name="httpContext"></param>
        /// <returns>ha a jogosultság megfelelő, akkor true-val tér vissza, egyébként false-al</returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!_authorize) { return true; }

            List<string> groupList = UserGroups != null ? UserGroups.Split(';').ToList() : new List<string>();

            if (groupList.Count.Equals(0))
            {
                return false;
            }

            //olvasás sütiből
            string cookieName = CompanyGroup.Helpers.ConfigSettingsParser.GetString("CookieName", "companygroup");

            System.Web.HttpCookie cookie = httpContext.Request.Cookies.Get(cookieName); 

            if (cookie == null || String.IsNullOrWhiteSpace(cookie.Value))
            {
                return false;
            }

            //szervizhívás, ami kiolvassa a felhasználóhoz tartozó jogosultságokat

            string ServiceBaseUrl = CompanyGroup.Helpers.ConfigSettingsParser.GetString("ServiceBaseUrl", "http://1Juhasza/CompanyGroup.ServicesHost/{0}.svc/");

            RestSharp.RestClient client = new RestSharp.RestClient(String.Format(ServiceBaseUrl, "VisitorService"));

            RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.POST);

            request.RequestFormat = RestSharp.DataFormat.Json;

            request.Resource = "GetRoles"; 

            request.AddBody(new { ObjectId = cookie.Value, DataAreaId = CompanyGroup.Helpers.ConfigSettingsParser.GetString("DataAreaId", "hrp") }); 

            RestSharp.RestResponse<List<string>> response = client.Execute<List<string>>(request);

            List<string> roles =  response.Data;

            return groupList.Intersect(roles).Count() > 0;
        }

        //public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        //{
        //    if (filterContext == null)
        //    {
        //        throw new ArgumentNullException("filterContext");
        //    }

        //    if (AuthorizeCore(filterContext.HttpContext))
        //    {
        //        base.OnAuthorization(filterContext);
        //    }
        //    else
        //    {
        //        //itt az authorizationError
        //        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
        //        {
        //            filterContext.Result = new ContentResult { Content = "Insufficent user rights", ContentEncoding = Encoding.Unicode };
        //        }
        //        else
        //        {
        //            string errorViewName = string.IsNullOrEmpty(ViewName) ? "AuthError" : ViewName;
        //            ViewDataDictionary viewData = new ViewDataDictionary();
        //            viewData.Add(Constants.MESSAGE, "Insufficent user rights");
        //            viewData.Add(Constants.USERNAME, filterContext.HttpContext.User.Identity.Name);
        //            filterContext.Result = new ViewResult { MasterName = "Default", ViewName = errorViewName, ViewData = viewData };
        //        }

        //    }

        //}
    }
}