using System;
using System.Collections.Generic;

namespace Cms.PartnerInfo.Models
{
    public class WebAdministrator : CompanyGroup.Dto.RegistrationModule.WebAdministrator
    {
        public WebAdministrator(CompanyGroup.Dto.RegistrationModule.WebAdministrator webAdministrator)
        {
            this.AllowOrder = webAdministrator.AllowOrder;
            this.AllowReceiptOfGoods = webAdministrator.AllowReceiptOfGoods;
            this.ContactPersonId = webAdministrator.ContactPersonId;
            this.Email = webAdministrator.Email;
            this.EmailArriveOfGoods = webAdministrator.EmailArriveOfGoods;
            this.EmailOfDelivery = webAdministrator.EmailOfDelivery;
            this.EmailOfOrderConfirm = webAdministrator.EmailOfOrderConfirm;
            this.FirstName = webAdministrator.FirstName;
            this.InvoiceInfo = webAdministrator.InvoiceInfo;
            this.LastName = webAdministrator.LastName;
            this.LeftCompany = webAdministrator.LeftCompany;
            this.Newsletter = webAdministrator.Newsletter;
            this.Password = webAdministrator.Password;
            this.PriceListDownload = webAdministrator.PriceListDownload;
            this.RecId = webAdministrator.RecId;
            this.RefRecId = webAdministrator.RefRecId;
            this.SmsArriveOfGoods = webAdministrator.SmsArriveOfGoods;
            this.SmsOfDelivery = webAdministrator.SmsOfDelivery;
            this.SmsOrderConfirm = webAdministrator.SmsOrderConfirm;
            this.Telephone = webAdministrator.Telephone;
            this.UserName = webAdministrator.UserName;
        }

        public WebAdministrator() { }
    }

    public class BankAccount : CompanyGroup.Dto.RegistrationModule.BankAccount
    {
        public BankAccount(CompanyGroup.Dto.RegistrationModule.BankAccount bankAccount)
        {
            this.Id = bankAccount.Id;
            this.Part1 = bankAccount.Part1;
            this.Part2 = bankAccount.Part2;
            this.Part3 = bankAccount.Part3;
            this.RecId = bankAccount.RecId;
        }

        public BankAccount() { }
    }

    public class RemoveBankAccount
    {
        public string Id { get; set; }
    }

    public class CompanyData : CompanyGroup.Dto.RegistrationModule.CompanyData
    {
        public CompanyData(CompanyGroup.Dto.RegistrationModule.CompanyData companyData)
        {
            this.CountryRegionId = companyData.CountryRegionId;
            this.CustomerId = companyData.CustomerId;
            this.CustomerName = companyData.CustomerName;
            this.EUVatNumber = companyData.EUVatNumber;
            this.MainEmail = companyData.MainEmail;
            this.NewsletterToMainEmail = companyData.NewsletterToMainEmail;
            this.RegistrationNumber = companyData.RegistrationNumber;
            this.SignatureEntityFile = companyData.SignatureEntityFile;
            this.VatNumber = companyData.VatNumber;
        }

        public CompanyData() { }
    }

    public class ContactPerson : CompanyGroup.Dto.RegistrationModule.ContactPerson
    {
        public ContactPerson(CompanyGroup.Dto.RegistrationModule.ContactPerson contactPerson)
        {
            this.AllowOrder = contactPerson.AllowOrder;
            this.AllowReceiptOfGoods = contactPerson.AllowReceiptOfGoods;
            this.ContactPersonId = contactPerson.ContactPersonId;
            this.Email = contactPerson.Email;
            this.EmailArriveOfGoods = contactPerson.EmailArriveOfGoods;
            this.EmailOfDelivery = contactPerson.EmailOfDelivery;
            this.EmailOfOrderConfirm = contactPerson.EmailOfOrderConfirm;
            this.FirstName = contactPerson.FirstName;
            this.InvoiceInfo = contactPerson.InvoiceInfo;
            this.LastName = contactPerson.LastName;
            this.LeftCompany = contactPerson.LeftCompany;
            this.Newsletter = contactPerson.Newsletter;
            this.Password = contactPerson.Password;
            this.PriceListDownload = contactPerson.PriceListDownload;
            //this.RecId = contactPerson.RecId;
            //this.RefRecId = contactPerson.RefRecId;
            this.SmsArriveOfGoods = contactPerson.SmsArriveOfGoods;
            this.SmsOfDelivery = contactPerson.SmsOfDelivery;
            this.SmsOrderConfirm = contactPerson.SmsOrderConfirm;
            this.Telephone = contactPerson.Telephone;
            this.UserName = contactPerson.UserName;
            this.WebAdmin = contactPerson.WebAdmin;
        }

        public ContactPerson() { }
    }

    public class RemoveContactPerson
    {
        public string Id { get; set; }
    }

    public class DataRecording : CompanyGroup.Dto.RegistrationModule.DataRecording
    {
        public DataRecording(CompanyGroup.Dto.RegistrationModule.DataRecording dataRecording)
        {
            this.Email = dataRecording.Email;
            this.Name = dataRecording.Name;
            this.Phone = dataRecording.Phone;
        }

        public DataRecording() { }
    }

    public class DeliveryAddress : CompanyGroup.Dto.RegistrationModule.DeliveryAddress
    {
        public DeliveryAddress(CompanyGroup.Dto.RegistrationModule.DeliveryAddress deliveryAddress)
        {
            this.City = deliveryAddress.City;
            this.CountryRegionId = deliveryAddress.CountryRegionId;
            this.Id = deliveryAddress.Id;
            this.RecId = deliveryAddress.RecId;
            this.Street = deliveryAddress.Street;
        }

        public DeliveryAddress() { }
    }

    public class RemoveDeliveryAddress
    {
        public string Id { get; set; }
    }

    public class InvoiceAddress : CompanyGroup.Dto.RegistrationModule.InvoiceAddress
    {
        public InvoiceAddress(CompanyGroup.Dto.RegistrationModule.InvoiceAddress invoiceAddress)
        {
            this.City = invoiceAddress.City;
            this.CountryRegionId = invoiceAddress.CountryRegionId;
            this.Phone = invoiceAddress.Phone;
            this.Street = invoiceAddress.Street;
            this.ZipCode = invoiceAddress.ZipCode;
        }

        public InvoiceAddress() { }
    }

    public class UpdateRegistrationData
    {
        public CompanyData CompanyData { get; set; }

        public InvoiceAddress InvoiceAddress { get; set; }

        public MailAddress MailAddress { get; set; }
    }

    public class MailAddress : CompanyGroup.Dto.RegistrationModule.MailAddress
    {
        public MailAddress(CompanyGroup.Dto.RegistrationModule.MailAddress mailAddress)
        {
            this.City = mailAddress.City;
            this.CountryRegionId = mailAddress.CountryRegionId;
            this.Street = mailAddress.Street;
            this.ZipCode = mailAddress.ZipCode;
        }

        public MailAddress() { }
    }

    public class SelectForUpdateBankAccount
    {
        /// <summary>
        /// módosításra kiválasztott bankszámla azonosító
        /// </summary>
        public string SelectedId { get; set; }
    }

    public class SelectForUpdateDeliveryAddress
    {
        /// <summary>
        /// módosításra kiválasztott szállítási cím azonosító
        /// </summary>
        public string SelectedId { get; set; }
    }

    public class SelectForUpdateContactPerson
    {
        /// <summary>
        /// módosításra kiválasztott kapcsolattart= azonosító
        /// </summary>
        public string SelectedId { get; set; }
    }

    /// <summary>
    /// jelszómódosítás
    /// </summary>
    public class ChangePassword
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string UserName { get; set; }
    }

    /// <summary>
    /// elfelejtett jelszó
    /// </summary>
    public class ForgetPassword
    {
        public string UserName { get; set; }
    }
}
