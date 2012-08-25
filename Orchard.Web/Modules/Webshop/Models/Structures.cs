using System;
using System.Collections.Generic;
using System.Linq;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// struktúra model 
    /// </summary>
    public class Structures
    {
        public Structures()
        {
            Manufacturers = new Manufacturers();

            FirstLevelCategories = new FirstLevelCategories();

            SecondLevelCategories = new SecondLevelCategories();

            ThirdLevelCategories = new ThirdLevelCategories();
        }

        public Structures(CompanyGroup.Dto.WebshopModule.Structures structures)
        {
            Manufacturers = new Manufacturers();

            FirstLevelCategories = new FirstLevelCategories();

            SecondLevelCategories = new SecondLevelCategories();

            ThirdLevelCategories = new ThirdLevelCategories();

            structures.FirstLevelCategories.ForEach(x => FirstLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            structures.Manufacturers.ForEach(x => Manufacturers.Add(new ManufacturerItem() { Id = x.Id, Name = x.Name }));

            structures.SecondLevelCategories.ForEach(x => SecondLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));

            structures.ThirdLevelCategories.ForEach(x => ThirdLevelCategories.Add(new StructureItem() { Id = x.Id, Name = x.Name }));
        }

        public Manufacturers Manufacturers { get; set; }

        public FirstLevelCategories FirstLevelCategories { get; set; }

        public SecondLevelCategories SecondLevelCategories { get; set; }

        public ThirdLevelCategories ThirdLevelCategories { get; set; }
    }
}
