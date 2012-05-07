using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    public class Products
    {
        public Products(CompanyGroup.Dto.WebshopModule.Products items, Cms.CommonCore.Models.Visitor visitor)
        {
            this.Items = items;

            this.Visitor = visitor;
        }

        public CompanyGroup.Dto.WebshopModule.Products Items { get; set; }

        public long ListCount { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }

    }
}
