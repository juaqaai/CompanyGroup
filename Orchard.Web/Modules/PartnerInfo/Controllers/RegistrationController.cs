using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    /// <summary>
    /// PartnerInfo home controller 
    /// - SignIn (inherited)
    /// - SignOut (inherited)
    /// </summary>
    [Themed]
    public class RegistrationController : Cms.CommonCore.Controllers.HomeController
    {
        /// <summary>
        /// partnerhez tartozó regisztrációs adatok, view betöltésekor fut
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CompanyGroup.Dto.RegistrationModule.Registration response = null;

            //regisztrációs azonosító kiolvasása sütiből
            string registrationId = this.ReadRegistrationIdFromCookie();

            //ha nem volt regisztrációs azonosítója, akkor adatok olvasása az ERP-ből     
            if (String.IsNullOrEmpty(registrationId))
            {

                CompanyGroup.Dto.ServiceRequest.GetCustomerRegistration request = new CompanyGroup.Dto.ServiceRequest.GetCustomerRegistration()
                                                                                      {
                                                                                          DataAreaId = RegistrationController.DataAreaId,
                                                                                          VisitorId = this.ReadObjectIdFromCookie()
                                                                                      };

                response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.Registration>("CustomerService", "GetCustomerRegistration", request);

            }
            else //volt már regisztrációs azonosítója, ezért az ahhoz tartozó adatokat kell visszaolvasni a cacheDb-ből
            {
                CompanyGroup.Dto.ServiceRequest.GetRegistrationByKey request = new CompanyGroup.Dto.ServiceRequest.GetRegistrationByKey() { Id = registrationId };

                response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.Registration>("RegistrationService", "GetByKey", request); 
            }

            Cms.PartnerInfo.Models.Registration model = new Cms.PartnerInfo.Models.Registration(response.BankAccounts,
                                                                                                response.CompanyData,
                                                                                                response.ContactPersons,
                                                                                                response.DataRecording,
                                                                                                response.DeliveryAddresses,
                                                                                                response.InvoiceAddress,
                                                                                                response.MailAddress,
                                                                                                response.RegistrationId,
                                                                                                response.VisitorId,
                                                                                                response.WebAdministrator);

            return View("Index", model);
        }

        /// <summary>
        /// új regisztráció hozzáadás (akkor hívódik, ha elfogadásra kerültek a szerződési feltételek)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddNew()
        {
            CompanyGroup.Dto.RegistrationModule.Registration response = null;

            //regisztrációs azonosító kiolvasása sütiből
            string registrationId = this.ReadRegistrationIdFromCookie();

            //ha volt már regisztrációs azonosító, akkor a regisztráció kiolvasása történik a cacheDb-ből     
            if (!String.IsNullOrEmpty(registrationId))
            {
                CompanyGroup.Dto.ServiceRequest.GetRegistrationByKey getRegistrationByKey = new CompanyGroup.Dto.ServiceRequest.GetRegistrationByKey() { Id = registrationId };

                response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.Registration>("RegistrationService", "GetByKey", getRegistrationByKey);

            }

            //ha nem volt korábban regisztráció, vagy volt, de nem érvényes a státusz flag, akkr új regisztráció hozzáadása történik
            if ((response == null) || (response.RegistrationId.Equals(RegistrationController.MongoDbEmptyObjectId)))
            {
                CompanyGroup.Dto.ServiceRequest.AddNewRegistration addNewRegistration = new CompanyGroup.Dto.ServiceRequest.AddNewRegistration()
                                                                                            {
                                                                                                VisitorId = this.ReadObjectIdFromCookie(),
                                                                                                LanguageId = this.ReadLanguageFromCookie()
                                                                                            };

                response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.Registration>("RegistrationService", "AddNew", addNewRegistration);
            }

            //ha a válaszban nincs bankszámlaszám, akkor egy üres elemet hozzá kell adni
            //if (response.BankAccounts.Count.Equals(0))
            //{ 
            //    response.BankAccounts.Add(new CompanyGroup.Dto.RegistrationModule.BankAccount(){ Id = "", Part1 = "", Part2 = "", Part3 = "", RecId = 0 });
            //}

            //ha a válaszban nincs kapcsolattartó, akkor egy üres elemet hozzá kell adni
            //if (response.ContactPersons.Count.Equals(0))
            //{
            //    response.ContactPersons.Add(new CompanyGroup.Dto.RegistrationModule.ContactPerson() 
            //                                    { 
            //                                        AllowOrder = false, 
            //                                        AllowReceiptOfGoods = false, 
            //                                        ContactPersonId = "", 
            //                                        Email = "", 
            //                                        EmailArriveOfGoods = false, 
            //                                        EmailOfDelivery = false, 
            //                                        EmailOfOrderConfirm = false, 
            //                                        FirstName = "", 
            //                                        Id = "", 
            //                                        InvoiceInfo = false, 
            //                                        LastName = "", 
            //                                        LeftCompany = false, 
            //                                        Newsletter = false, 
            //                                        Password = "", 
            //                                        PriceListDownload = false, 
            //                                        SmsArriveOfGoods = false, 
            //                                        SmsOfDelivery = false, 
            //                                        SmsOrderConfirm = false, 
            //                                        Telephone = "", 
            //                                        UserName = "", 
            //                                        WebAdmin = false 
            //                                    });
            //}

            //ha a válaszban nincs szállítási cím, akkor egy üres elemet hozzá kell adni
            //if (response.DeliveryAddresses.Count.Equals(0))
            //{
            //    response.DeliveryAddresses.Add(new CompanyGroup.Dto.RegistrationModule.DeliveryAddress()
            //    {
            //        City = "",
            //        CountryRegionId = "",
            //        Id = "",
            //        RecId = 0,
            //        Street = "",
            //        ZipCode = ""
            //    });
            //}

            //létrehozott regisztrációs azonosító beírása sütibe
            this.WriteRegistrationIdToCookie(response.RegistrationId);

            Cms.PartnerInfo.Models.Registration model = new Cms.PartnerInfo.Models.Registration(response.BankAccounts, 
                                                                                                response.CompanyData, 
                                                                                                response.ContactPersons, 
                                                                                                response.DataRecording, 
                                                                                                response.DeliveryAddresses, 
                                                                                                response.InvoiceAddress, 
                                                                                                response.MailAddress, 
                                                                                                response.RegistrationId, 
                                                                                                response.VisitorId, 
                                                                                                response.WebAdministrator);

            return Json(model, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// regisztráció elküldése az ERP -nek
        /// </summary>
        /// <returns>Successed, Message</returns>
        [HttpPost]
        public JsonResult Post()
        {
            CompanyGroup.Dto.ServiceRequest.PostRegistration request = new CompanyGroup.Dto.ServiceRequest.PostRegistration() 
                                                                           { 
                                                                               RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                               LanguageId = this.ReadLanguageFromCookie() 
                                                                           };

            CompanyGroup.Dto.ServiceResponse.PostRegistration response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.PostRegistration>("RegistrationService", "Post", request);

            //ha a feladás sikeres, akkor a felhasználói sütiből a regisztrációs azonosító törlésre kerül
            if (response.Successed)
            {
                this.RemoveRegistrationIdFromCookie();
            }

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// adatrögzítő adatainak felvitele
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Successed, Message</returns>
        [HttpPost]
        public JsonResult UpdateDataRecording([Bind(Prefix = "")] Cms.PartnerInfo.Models.DataRecording request)
        { 
            CompanyGroup.Dto.ServiceRequest.UpdateDataRecording req = new CompanyGroup.Dto.ServiceRequest.UpdateDataRecording() 
                                                                          { 
                                                                              RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                              LanguageId = this.ReadLanguageFromCookie(), 
                                                                              DataRecording = request 
                                                                          };

            CompanyGroup.Dto.ServiceResponse.UpdateDataRecording response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.UpdateDataRecording>("RegistrationService", "UpdateDataRecording", req);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// céges regisztrációs adatok, számlák, levelezési címek felvitele
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Successed, Message</returns>
        [HttpPost]
        public JsonResult UpdateRegistrationData([Bind(Prefix = "")] Cms.PartnerInfo.Models.UpdateRegistrationData request)
        { 
            CompanyGroup.Dto.ServiceRequest.UpdateRegistrationData req = new CompanyGroup.Dto.ServiceRequest.UpdateRegistrationData() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                CompanyData = request.CompanyData, 
                                                                                InvoiceAddress = request.InvoiceAddress, 
                                                                                MailAddress = request.MailAddress 
                                                                            };

            CompanyGroup.Dto.ServiceResponse.UpdateRegistrationData response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.UpdateRegistrationData>("RegistrationService", "UpdateRegistrationData", req);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// webadminisztrátor adatok felvitele
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Successed, Message</returns>
        [HttpPost]
        public JsonResult UpdateWebAdministrator([Bind(Prefix = "")] Cms.PartnerInfo.Models.WebAdministrator request)
        {
            request.ContactPersonId = request.ContactPersonId ?? String.Empty;

            CompanyGroup.Dto.ServiceRequest.UpdateWebAdministrator req = new CompanyGroup.Dto.ServiceRequest.UpdateWebAdministrator() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                WebAdministrator = request
                                                                            };

            CompanyGroup.Dto.ServiceResponse.UpdateWebAdministrator response = this.PostJSonData<CompanyGroup.Dto.ServiceResponse.UpdateWebAdministrator>("RegistrationService", "UpdateWebAdministrator", req);

            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        #region "DeliveryAddress"

        /// <summary>
        /// kiszállítási cím felvitele
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// RecId, City, Street, ZipCode, CountryRegionId, Id
        /// </returns>
        [HttpPost]
        public JsonResult AddDeliveryAddress([Bind(Prefix = "")] Cms.PartnerInfo.Models.DeliveryAddress request)
        {
            request.Id = String.Empty;

            CompanyGroup.Dto.ServiceRequest.AddDeliveryAddress req = new CompanyGroup.Dto.ServiceRequest.AddDeliveryAddress() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                DeliveryAddress = request
                                                                            };

            CompanyGroup.Dto.RegistrationModule.DeliveryAddresses response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.DeliveryAddresses>("RegistrationService", "AddDeliveryAddress", req);

            Cms.PartnerInfo.Models.DeliveryAddresses deliveryAddresses = new Cms.PartnerInfo.Models.DeliveryAddresses() { Items = response.Items, SelectedId = request.Id };

            return Json(deliveryAddresses, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult SelectForUpdateDeliveryAddress([Bind(Prefix = "")] Cms.PartnerInfo.Models.SelectForUpdateDeliveryAddress request)
        {
            CompanyGroup.Dto.ServiceRequest.GetDeliveryAddress req = new CompanyGroup.Dto.ServiceRequest.GetDeliveryAddress()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie()
            };

            CompanyGroup.Dto.RegistrationModule.DeliveryAddresses response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.DeliveryAddresses>("RegistrationService", "GetDeliveryAddresses", req);

            Cms.PartnerInfo.Models.DeliveryAddresses deliveryAddresses = new Cms.PartnerInfo.Models.DeliveryAddresses() { Items = response.Items, SelectedId = request.SelectedId };

            return Json(deliveryAddresses, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// kiszállítási cím módosítása
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// RecId, City, Street, ZipCode, CountryRegionId, Id
        /// </returns>
        [HttpPost]
        public JsonResult UpdateDeliveryAddress([Bind(Prefix = "")] Cms.PartnerInfo.Models.DeliveryAddress request)
        {
            CompanyGroup.Dto.ServiceRequest.UpdateDeliveryAddress req = new CompanyGroup.Dto.ServiceRequest.UpdateDeliveryAddress() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                DeliveryAddress = request
                                                                            };

            CompanyGroup.Dto.RegistrationModule.DeliveryAddresses response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.DeliveryAddresses>("RegistrationService", "UpdateDeliveryAddress", req);

            Cms.PartnerInfo.Models.DeliveryAddresses deliveryAddresses = new Cms.PartnerInfo.Models.DeliveryAddresses() { Items = response.Items, SelectedId = String.Empty };

            return Json(deliveryAddresses, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// kiszállítási cím törlése
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// RecId, City, Street, ZipCode, CountryRegionId, Id
        /// </returns>
        [HttpPost]
        public JsonResult RemoveDeliveryAddress([Bind(Prefix = "")] Cms.PartnerInfo.Models.RemoveDeliveryAddress request)
        {
            CompanyGroup.Dto.ServiceRequest.RemoveDeliveryAddress req = new CompanyGroup.Dto.ServiceRequest.RemoveDeliveryAddress() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                DeliveryAddressId = request.Id
                                                                            };

            CompanyGroup.Dto.RegistrationModule.DeliveryAddresses response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.DeliveryAddresses>("RegistrationService", "RemoveDeliveryAddress", req);

            Cms.PartnerInfo.Models.DeliveryAddresses deliveryAddresses = new Cms.PartnerInfo.Models.DeliveryAddresses() { Items = response.Items, SelectedId = String.Empty };

            return Json(deliveryAddresses, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region "BankAccounts"

        /// <summary>
        /// bankszámlaszám hozzáadása
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Part1 Part2 Part3 RecId Id</returns>
        [HttpPost]
        public JsonResult AddBankAccount([Bind(Prefix = "")] Cms.PartnerInfo.Models.BankAccount request)
        {
            request.Id = String.Empty;

            CompanyGroup.Dto.ServiceRequest.AddBankAccount req = new CompanyGroup.Dto.ServiceRequest.AddBankAccount()
                                                                            {
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(),
                                                                                LanguageId = this.ReadLanguageFromCookie(),
                                                                                BankAccount = request
                                                                            };

            CompanyGroup.Dto.RegistrationModule.BankAccounts response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.BankAccounts>("RegistrationService", "AddBankAccount", req);

            Cms.PartnerInfo.Models.BankAccounts bankAccounts = new Cms.PartnerInfo.Models.BankAccounts() { Items = response.Items, SelectedId = String.Empty };

            return Json(bankAccounts, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult SelectForUpdateBankAccount([Bind(Prefix = "")] Cms.PartnerInfo.Models.SelectForUpdateBankAccount request)
        {
            CompanyGroup.Dto.ServiceRequest.GetBankAccounts req = new CompanyGroup.Dto.ServiceRequest.GetBankAccounts()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie()
            };

            CompanyGroup.Dto.RegistrationModule.BankAccounts response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.BankAccounts>("RegistrationService", "GetBankAccounts", req);

            Cms.PartnerInfo.Models.BankAccounts bankAccounts = new Cms.PartnerInfo.Models.BankAccounts() { Items = response.Items, SelectedId = request.SelectedId };

            return Json(bankAccounts, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// bankszámlaszám módosítása
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Part1 Part2 Part3 RecId Id</returns>
        [HttpPost]
        public JsonResult UpdateBankAccount([Bind(Prefix = "")] Cms.PartnerInfo.Models.BankAccount request)
        {
            CompanyGroup.Dto.ServiceRequest.UpdateBankAccount req = new CompanyGroup.Dto.ServiceRequest.UpdateBankAccount()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie(),
                BankAccount = request
            };

            CompanyGroup.Dto.RegistrationModule.BankAccounts response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.BankAccounts>("RegistrationService", "UpdateBankAccount", req);

            Cms.PartnerInfo.Models.BankAccounts bankAccounts = new Cms.PartnerInfo.Models.BankAccounts() { Items = response.Items, SelectedId = String.Empty };

            return Json(bankAccounts, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// bankszámlaszám törlése
        /// </summary>
        /// <param name="request">Part1 Part2 Part3 RecId Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveBankAccount([Bind(Prefix = "")] Cms.PartnerInfo.Models.RemoveBankAccount request)
        {
            CompanyGroup.Dto.ServiceRequest.RemoveBankAccount req = new CompanyGroup.Dto.ServiceRequest.RemoveBankAccount()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie(),
                BankAccountId = request.Id
            };

            CompanyGroup.Dto.RegistrationModule.BankAccounts response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.BankAccounts>("RegistrationService", "RemoveBankAccount", req);

            Cms.PartnerInfo.Models.BankAccounts bankAccounts = new Cms.PartnerInfo.Models.BankAccounts() { Items = response.Items, SelectedId = String.Empty };

            return Json(bankAccounts, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region "ContactPersons"

        /// <summary>
        /// kapcsolattartó hozzáadása
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <summary>
        /// kapcsolattartó egyedi azonosító
        /// </summary>
        ///ContactPersonId,FirstName,LastName,AllowOrder ,AllowReceiptOfGoods ,SmsArriveOfGoods,SmsOrderConfirm,SmsOfDelivery ,EmailArriveOfGoods ,EmailOfOrderConfirm ,EmailOfDelivery ,WebAdmin ,PriceListDownload 
        ///InvoiceInfo,UserName,Password ,Newsletter,Telephone,Email ,LeftCompany,Id
        /// </returns>
        [HttpPost]
        public JsonResult AddContactPerson([Bind(Prefix = "")] Cms.PartnerInfo.Models.ContactPerson request)
        {
            request.ContactPersonId = request.ContactPersonId ?? String.Empty;

            CompanyGroup.Dto.ServiceRequest.AddContactPerson req = new CompanyGroup.Dto.ServiceRequest.AddContactPerson() 
                                                                            { 
                                                                                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                                                                                LanguageId = this.ReadLanguageFromCookie(), 
                                                                                ContactPerson = request
                                                                            };

            CompanyGroup.Dto.RegistrationModule.ContactPersons response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.ContactPersons>("RegistrationService", "AddContactPerson", req);

            Cms.PartnerInfo.Models.ContactPersons contactPersons = new Cms.PartnerInfo.Models.ContactPersons() { Items = response.Items, SelectedId = String.Empty };

            return Json(contactPersons, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// kapcsolattartók kiolvasása
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SelectForUpdateContactPerson([Bind(Prefix = "")] Cms.PartnerInfo.Models.SelectForUpdateContactPerson request)
        {
            CompanyGroup.Dto.ServiceRequest.GetContactPerson req = new CompanyGroup.Dto.ServiceRequest.GetContactPerson()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(), 
                LanguageId = this.ReadLanguageFromCookie()
            };

            CompanyGroup.Dto.RegistrationModule.ContactPersons response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.ContactPersons>("RegistrationService", "GetContactPersons", req);

            Cms.PartnerInfo.Models.ContactPersons contactPersons = new Cms.PartnerInfo.Models.ContactPersons() { Items = response.Items, SelectedId = request.SelectedId };

            return Json(contactPersons, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// kapcsolattartó módosítása
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateContactPerson([Bind(Prefix = "")] Cms.PartnerInfo.Models.ContactPerson request)
        {
            CompanyGroup.Dto.ServiceRequest.UpdateContactPerson req = new CompanyGroup.Dto.ServiceRequest.UpdateContactPerson()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie(),
                ContactPerson = request
            };

            CompanyGroup.Dto.RegistrationModule.ContactPersons response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.ContactPersons>("RegistrationService", "UpdateContactPerson", req);

            Cms.PartnerInfo.Models.ContactPersons contactPersons = new Cms.PartnerInfo.Models.ContactPersons() { Items = response.Items, SelectedId = String.Empty };

            return Json(contactPersons, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// kapcsolattartó törlése
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveContactPerson([Bind(Prefix = "")] Cms.PartnerInfo.Models.RemoveContactPerson request)
        {
            CompanyGroup.Dto.ServiceRequest.RemoveContactPerson req = new CompanyGroup.Dto.ServiceRequest.RemoveContactPerson()
            {
                RegistrationId = this.ReadRegistrationIdFromCookie(),
                LanguageId = this.ReadLanguageFromCookie(),
                Id = request.Id
            };

            CompanyGroup.Dto.RegistrationModule.ContactPersons response = this.PostJSonData<CompanyGroup.Dto.RegistrationModule.ContactPersons>("RegistrationService", "RemoveContactPerson", req);

            Cms.PartnerInfo.Models.ContactPersons contactPersons = new Cms.PartnerInfo.Models.ContactPersons() { Items = response.Items, SelectedId = String.Empty };

            return Json(contactPersons, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        #endregion

        /// <summary>
        /// aláírási címpéldány file feltöltés   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile()
        {
            Cms.PartnerInfo.Models.UploadedFile response = new Cms.PartnerInfo.Models.UploadedFile();

            foreach (string file in Request.Files)
            {
                System.Web.HttpPostedFileBase postedFile = Request.Files[file] as System.Web.HttpPostedFileBase;
                if (postedFile.ContentLength == 0)
                    continue;

                string fileName = String.Format("{0}_{1}{2}{3}", postedFile.FileName, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                string savedFileName = System.IO.Path.Combine(Server.MapPath("~/App_Data/Uploads"), System.IO.Path.GetFileName(fileName));

                postedFile.SaveAs(savedFileName);

                response.Name = fileName;
                response.Length = postedFile.ContentLength;
                response.Type = postedFile.ContentType;
            }
            return Json(response, "application/json; charset=utf-8", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }        
    }
}