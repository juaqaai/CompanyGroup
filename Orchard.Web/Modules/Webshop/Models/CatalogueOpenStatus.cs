using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    public class CatalogueOpenStatus
    {
        public CatalogueOpenStatus()
        {
            this.IsOpen = false;
        }

        public bool IsOpen { get; set; }
    }
}
