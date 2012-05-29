using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models.Request
{
    /// <summary>
    /// felhasználó 
    /// </summary>
    public class Visitor
    {
        public string VisitorId { get; set; }
    }

    public class ActivateCart
    {
        public string CartId { get; set; }
    }

}
