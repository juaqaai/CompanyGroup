using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    public class Catalogue
    {
        public Catalogue(CompanyGroup.Dto.WebshopModule.Structures structures, CompanyGroup.Dto.WebshopModule.Products products, Cms.CommonCore.Models.Visitor visitor)
        {
            this.Structures = new Structures(structures);

            //structures.FirstLevelCategories.ForEach(x => Structures.FirstLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            //structures.Manufacturers.ForEach(x => Structures.Manufacturers.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            //structures.SecondLevelCategories.ForEach(x => Structures.SecondLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            //structures.ThirdLevelCategories.ForEach(x => Structures.ThirdLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            this.Products = products;   //new CompanyGroup.Dto.WebshopModule.Products();

            this.Visitor = visitor;
        }

        public Structures Structures { get; set; }

        public CompanyGroup.Dto.WebshopModule.Products Products { get; set; }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }
    }
}
