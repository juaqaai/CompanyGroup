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
                             CompanyGroup.Dto.WebshopModule.BannerList bannerList)
        {
            this.Structures = new Structures(structures);

            this.Product = product;

            this.ReverseCompatibleProducts = reverseCompatibleProducts;

            this.CompatibleProducts = compatibleProducts;

            this.Visitor = visitor;

            this.BannerList = bannerList;
        }

        public Structures Structures { get; set; }

        public CompanyGroup.Dto.WebshopModule.Product Product { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> CompatibleProducts { get; set; }

        public List<CompanyGroup.Dto.WebshopModule.CompatibleProduct> ReverseCompatibleProducts { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }

        public CompanyGroup.Dto.WebshopModule.BannerList BannerList { get; set; }
    }
}
