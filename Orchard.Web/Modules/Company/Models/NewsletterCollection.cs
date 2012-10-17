using System;
using System.Collections.Generic;

namespace Cms.Company.Models
{
    public class NewsletterCollection : CompanyGroup.Dto.WebshopModule.NewsletterCollection
    {
        public NewsletterCollection(CompanyGroup.Dto.WebshopModule.NewsletterCollection newsletterCollection)
        {
            this.Items = newsletterCollection.Items;
        }
    }
}
