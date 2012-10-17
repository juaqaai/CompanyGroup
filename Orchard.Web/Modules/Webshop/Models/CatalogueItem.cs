using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// CatalogueItem model for product details view
    /// </summary>
    public class CatalogueItem
    {
        public CatalogueItem(CompanyGroup.Dto.WebshopModule.Structures structures, 
                             CompanyGroup.Dto.WebshopModule.Product product, 
                             List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> compatibleProducts,
                             List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> reverseCompatibleProducts, 
                             Cms.CommonCore.Models.Visitor visitor,
                             CompanyGroup.Dto.WebshopModule.BannerList bannerList,
                             CompanyGroup.Dto.WebshopModule.ShoppingCart activeCart,
                             List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> openedItems,
                             List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> storedItems,
                             bool shoppingCartOpenStatus,
                             bool catalogueOpenStatus,
                             CompanyGroup.Dto.PartnerModule.DeliveryAddresses deliveryAddresses,
                             CompanyGroup.Dto.WebshopModule.LeasingOptions leasingOptions)
        {
            this.Structures = new Structures(structures);

            this.Product = product;

            this.ReverseCompatibleProducts = reverseCompatibleProducts;

            this.CompatibleProducts = compatibleProducts;

            this.Visitor = visitor;

            this.BannerList = bannerList;

            this.ActiveCart = activeCart;

            this.OpenedItems = openedItems;

            this.StoredItems = storedItems;

            this.ShoppingCartOpenStatus = shoppingCartOpenStatus;

            this.CatalogueOpenStatus = catalogueOpenStatus;

            this.DeliveryAddresses = deliveryAddresses;

            this.LeasingOptions = leasingOptions;
        }

        public Structures Structures { get; set; }

        public CompanyGroup.Dto.WebshopModule.Product Product { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> CompatibleProducts { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> ReverseCompatibleProducts { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }

        public CompanyGroup.Dto.WebshopModule.BannerList BannerList { get; set; }

        public CompanyGroup.Dto.WebshopModule.ShoppingCart ActiveCart { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.OpenedShoppingCart> OpenedItems { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.StoredShoppingCart> StoredItems { get; set; }

        public bool ShoppingCartOpenStatus { get; set; }

        public bool CatalogueOpenStatus { get; set; }

        public CompanyGroup.Dto.PartnerModule.DeliveryAddresses DeliveryAddresses { get; set; }

        public CompanyGroup.Dto.WebshopModule.LeasingOptions LeasingOptions { get; set; }
    }
}
