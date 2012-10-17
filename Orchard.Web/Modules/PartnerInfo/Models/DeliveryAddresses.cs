using System;
using System.Collections.Generic;

namespace Cms.PartnerInfo.Models
{
    public class DeliveryAddresses : CompanyGroup.Dto.RegistrationModule.DeliveryAddresses
    {
        public DeliveryAddresses() : base()
        {
            SelectedId = String.Empty;
        }

        /// <summary>
        /// módosításra kiválasztott szállítási cím azonosító
        /// </summary>
        public string SelectedId { get; set; }
    }
}
