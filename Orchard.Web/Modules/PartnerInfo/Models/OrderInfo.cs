using System;
using System.Collections.Generic;

namespace Cms.PartnerInfo.Models
{
    /// <summary>
    /// megrendelés info lista
    /// </summary>
    public class OrderInfoList
    {
        public OrderInfoList(List<Cms.PartnerInfo.Models.OrderInfo> items)
        {
            this.Items = items;
        }

        public List<Cms.PartnerInfo.Models.OrderInfo> Items { get; set; }
    }

    /// <summary>
    /// megrendelés info
    /// </summary>
    public class OrderInfo
    {
        public string SalesId { set; get; }

        public DateTime CreatedDate { set; get; }

        public List<Cms.PartnerInfo.Models.OrderLineInfo> Lines { get; set; }
    }

    /// <summary>
    /// megrendelés sor info
    /// </summary>
    public class OrderLineInfo : CompanyGroup.Dto.PartnerModule.OrderLineInfo
    {
    }
}
