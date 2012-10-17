using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models
{
    public static class Factory
    {
        public static Cms.CommonCore.Models.Response.ShoppingCartInfo CreateShoppingCartInfo()
        {
            return new Cms.CommonCore.Models.Response.ShoppingCartInfo()
                       {
                           ActiveCart = new CompanyGroup.Dto.WebshopModule.ShoppingCart()
                                            {
                                                DeliveryTerms = 1,
                                                Id = "",
                                                Items = new List<CompanyGroup.Dto.WebshopModule.ShoppingCartItem>(),
                                                PaymentTerms = 0,
                                                Shipping = new CompanyGroup.Dto.WebshopModule.Shipping() { AddrRecId = 0, City = "", Country = "", DateRequested = DateTime.Now, InvoiceAttached = false, Street = "", ZipCode = "" },
                                                SumTotal = 0
                                            },
                           ErrorMessage = "",
                           OpenedItems = new List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart>(),
                           StoredItems = new List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart>()
                       };
        }
    }
}
