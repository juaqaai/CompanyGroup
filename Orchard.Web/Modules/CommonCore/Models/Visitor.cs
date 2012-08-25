using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models
{
    /// <summary>
    /// látogató model 
    /// </summary>
    public class Visitor : CompanyGroup.Dto.PartnerModule.Visitor
    {
        public string ErrorMessage { get; set; }
    }
}
