using System;
using System.Collections.Generic;

namespace Cms.PartnerInfo.Models
{
    /// <summary>
    /// számla info lista
    /// </summary>
    public class InvoiceInfoList
    {
        public InvoiceInfoList(List<Cms.PartnerInfo.Models.InvoiceInfo> items)
        {
            this.Items = items;

            this.ItemCount = items.Count;

            this.NettoSumCredit = String.Format("{0}", 0);
        }

        public List<Cms.PartnerInfo.Models.InvoiceInfo> Items { get; set; }

        public int ItemCount { get; set; }

        public string NettoSumCredit { get; set; }
    }

    /// <summary>
    /// számla info
    /// </summary>
    public class InvoiceInfo
    {
        /// <summary>
        /// VR, vagy BR azonosító
        /// </summary>
        public string SalesId { set; get; }

        /// <summary>
        /// bizonylat elkészülte
        /// </summary>
        public string InvoiceDate { set; get; }

        /// <summary>
        /// bizonylat lejárati ideje
        /// </summary>
        public string DueDate { set; get; }

        /// <summary>
        /// számla végösszege
        /// </summary>
        public string InvoiceAmount { set; get; }

        /// <summary>
        /// számlán lévő tartozás
        /// </summary>
        public string InvoiceCredit { set; get; }

        /// <summary>
        /// pénznem
        /// </summary>
        public string CurrencyCode { set; get; }

        /// <summary>
        /// számla azonosító
        /// </summary>
        public string InvoiceId { set; get; }

        /// <summary>
        /// fizetési feltétel
        /// </summary>
        public string Payment { set; get; }

        /// <summary>
        /// számla típusa (0 napló, 1 árajánlat, 2 előfizetés, 3 értékesítés, 4 viszáru, 5 keretrendelés, 6 cikkszükséglet)
        /// </summary>
        public int SalesType { set; get; }

        /// <summary>
        /// vevői hivatkozás
        /// </summary>
        public string CusomerRef { set; get; }

        /// <summary>
        /// számlán szereplő név
        /// </summary>
        public string InvoicingName { set; get; }

        /// <summary>
        /// számlázási cím
        /// </summary>
        public string InvoicingAddress { set; get; }

        /// <summary>
        /// kapcsolattartó
        /// </summary>
        public string ContactPersonId { set; get; }

        /// <summary>
        /// nyomtatva
        /// </summary>
        public bool Printed { set; get; }

        /// <summary>
        /// visszáru 
        /// </summary>
        public string ReturnItemId { set; get; }

        /// <summary>
        /// számla sorok     
        /// </summary>
        public List<Cms.PartnerInfo.Models.InvoiceLineInfo> Lines { get; set; }
    }

    /// <summary>
    /// számla sor info
    /// </summary>
    public class InvoiceLineInfo : CompanyGroup.Dto.PartnerModule.InvoiceLineInfo
    {
    }
}
