using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Webshop.Models
{
    public class AddShoppingCartItem 
    {
        /// <summary>
        /// kosár azonosító
        /// </summary>
        public string CartId { get; set; }

        /// <summary>
        /// termékazonosító
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// mennyiség
        /// </summary>
        public int Quantity { get; set; }
    }
}
