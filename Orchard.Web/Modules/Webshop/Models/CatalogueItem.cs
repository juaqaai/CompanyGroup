using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// CatalogueItem model for product details view
    /// </summary>
    public class CatalogueItem
    {
        public CatalogueItem(CompanyGroup.Dto.WebshopModule.Structures structures, CompanyGroup.Dto.WebshopModule.Product product, Cms.CommonCore.Models.Visitor visitor)
        {
            this.Structures = new Structures(structures);

            this.Product = product;  

            this.Visitor = visitor;
        }

        public Structures Structures { get; set; }

        public CompanyGroup.Dto.WebshopModule.Product Product { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }
    }
}
