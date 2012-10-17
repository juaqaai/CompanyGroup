using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models.Response
{
    //public class StoredOpenedShoppingCartCollection : CompanyGroup.Dto.WebshopModule.StoredOpenedShoppingCartCollection
    //{
    //    public StoredOpenedShoppingCartCollection() : this(new List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart>(), new List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart>()) { }

    //    public StoredOpenedShoppingCartCollection(List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> storedShoppingCart, List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> openedShoppingCart)
    //    {
    //        this.StoredItems.AddRange(storedShoppingCart);

    //        this.OpenedItems.AddRange(openedShoppingCart);

    //        this.ErrorMessage = String.Empty;
    //    }

    //    public string ErrorMessage { get; set; }
    //}

    //public class StoredShoppingCartCollection : List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart>
    //{
    //    public StoredShoppingCartCollection(List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> storedShoppingCarts)
    //    {
    //        this.AddRange(storedShoppingCarts);
    //    }
    //}

    //public class OpenedShoppingCartCollection : List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart>
    //{
    //    public OpenedShoppingCartCollection(List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> openedShoppingCarts)
    //    {
    //        this.AddRange(openedShoppingCarts);
    //    }
    //}

    public class ShoppingCartInfo : CompanyGroup.Dto.WebshopModule.ShoppingCartInfo
    {
        public string ErrorMessage { get; set; }
    }
}
