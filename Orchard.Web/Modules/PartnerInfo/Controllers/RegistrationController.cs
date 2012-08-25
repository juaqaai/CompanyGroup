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
        public ActionResult Index()
        {

            Cms.PartnerInfo.Models.CustomerRegistration model = new Cms.PartnerInfo.Models.CustomerRegistration();

            model.BankAccounts.Add(new CompanyGroup.Dto.PartnerModule.BankAccount() { Id = 1, Number = "", RecId = 0 });

            model.ContactPersons.Add(new CompanyGroup.Dto.PartnerModule.ContactPerson() 
                                         { 
                                             AllowOrder = false, 
                                             AllowReceiptOfGoods = false, 
                                             ContactPersonId = "", 
                                             Email = "", 
                                             EmailArriveOfGoods = false, 
                                             EmailOfDelivery = false, 
                                             EmailOfOrderConfirm = false, 
                                             FirstName = "", 
                                             Id = 1, 
                                             InvoiceInfo = false, 
                                             LastName = "", 
                                             LeftCompany = false, 
                                             Newsletter = false, 
                                             Password = "", 
                                             PriceListDownload = false, 
                                             SmsArriveOfGoods = false, 
                                             SmsOfDelivery = false, 
                                             SmsOrderConfirm = false, 
                                             Telephone = "", 
                                             UserName = "", 
                                             WebAdmin = false 
                                         });

            model.DeliveryAddresses.Add(new CompanyGroup.Dto.PartnerModule.DeliveryAddress() { City = "", CountryRegionId = "", Id = 1, RecId = 0, Street = "", ZipCode = "" });

            return View("Index", model);
        }

    }
}