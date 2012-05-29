using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models.Response
{
    public class StoredOpenedShoppingCartCollection : CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection
    {
        public StoredOpenedShoppingCartCollection() : this(new List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart>(), new List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart>()) { }

        public StoredOpenedShoppingCartCollection(List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> storedShoppingCart, List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> openedShoppingCart)
        {
            this.StoredItems.AddRange(storedShoppingCart);

            this.OpenedItems.AddRange(openedShoppingCart);

            this.ErrorMessage = String.Empty;
        }

        public string ErrorMessage { get; set; }
    }

    public class ShoppingCart : CompanyGroup.Dto.WebshopModule.ShoppingCart
    {
        public ShoppingCart(string id, List<CompanyGroup.Dto.WebshopModule.ShoppingCartItem> items, double sumTotal)
        { 
            this.Id = id;

            this.Items.AddRange(items);

            this.SumTotal = sumTotal;
        }
    }
}
