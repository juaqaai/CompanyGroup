using System;
using System.Collections.Generic;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// kosár azonosító alapján történő lekérdezés adatait összefogó POCO
    /// </summary>
    public class GetCartByKey 
    {
        /// <summary>
        /// kosár azonosító
        /// </summary>
        public string CartId { get; set; }
    }
}
