using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Webshop.Models
{
    public class SaveShoppingCart
    {
        public SaveShoppingCart() : this("", "") { }

        public SaveShoppingCart(string cartId, string name)
        {
            this.CartId = cartId;

            this.Name = name;
        }

        public string CartId { get; set; }

        public string Name { get; set; }
    }
}
