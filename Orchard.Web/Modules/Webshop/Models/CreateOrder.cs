using System;

namespace Cms.Webshop.Models
{
    /// <summary>
    /// finance ajánlatkészítés kérés adatokat összefogó POCO
    /// </summary>
    public class CreateFinanceOffer
    {
        public string PersonName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string StatNumber { get; set; }

        public int NumOfMonth { get; set; }
    }
}
