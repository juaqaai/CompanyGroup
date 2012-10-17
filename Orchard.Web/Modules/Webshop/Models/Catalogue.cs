using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// webshop lista view-hoz tartozó típus, webshop catalogue controller index action JSonResult-ban
    /// </summary>
    public class Catalogue
    {
        public Catalogue(CompanyGroup.Dto.WebshopModule.Structures structures, 
                         CompanyGroup.Dto.WebshopModule.Products products, 
                         Cms.CommonCore.Models.Visitor visitor,
                         CompanyGroup.Dto.WebshopModule.ShoppingCart activeCart, 
                         List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> openedItems, 
                         List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> storedItems,
                         bool shoppingCartOpenStatus, 
                         bool catalogueOpenStatus, 
                         CompanyGroup.Dto.PartnerModule.DeliveryAddresses deliveryAddresses,
                         CompanyGroup.Dto.WebshopModule.BannerList bannerList, 
                         CompanyGroup.Dto.WebshopModule.LeasingOptions leasingOptions)
        {
            this.Structures = new Structures(structures);

            this.Products = products;   

            this.Visitor = visitor;

            this.ActiveCart = activeCart;

            this.OpenedItems = openedItems;

            this.StoredItems = storedItems;

            this.ShoppingCartOpenStatus = shoppingCartOpenStatus;

            this.CatalogueOpenStatus = catalogueOpenStatus;

            this.DeliveryAddresses = deliveryAddresses;

            this.BannerList = bannerList;

            this.LeasingOptions = leasingOptions;
        }

        public Structures Structures { get; set; }

        public CompanyGroup.Dto.WebshopModule.Products Products { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }

        public CompanyGroup.Dto.WebshopModule.ShoppingCart ActiveCart { get; set; } 

        public List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> OpenedItems { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> StoredItems { get; set; } 

        public bool ShoppingCartOpenStatus { get; set; }

        public bool CatalogueOpenStatus { get; set; }

        public CompanyGroup.Dto.PartnerModule.DeliveryAddresses DeliveryAddresses { get; set; }

        public CompanyGroup.Dto.WebshopModule.BannerList BannerList { get; set; }

        public CompanyGroup.Dto.WebshopModule.LeasingOptions LeasingOptions { get; set; }
    }

}
