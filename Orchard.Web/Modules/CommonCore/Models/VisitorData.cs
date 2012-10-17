using System;
using System.Collections.Generic;

namespace Cms.CommonCore.Models
{
    /// <summary>
    /// visitor related data
    /// </summary>
    public class VisitorData
    {
        public VisitorData()
        {
            this.ObjectId = String.Empty;

            this.Language = String.Empty;

            this.IsShoppingCartOpened = false;

            this.IsCatalogueOpened = false;

            this.Currency =String.Empty;

            this.PermanentId = String.Empty;

            this.CartId = String.Empty;

            this.RegistrationId = String.Empty;
        }

        public VisitorData(string objectId, string language, bool isShoppingCartOpened, bool isCatalogueOpened, string currency, string permanentId, string cartId, string registrationId)
        {
            this.ObjectId = objectId;

            this.Language = language;

            this.IsShoppingCartOpened = isShoppingCartOpened;

            this.IsCatalogueOpened = isCatalogueOpened;

            this.Currency = currency;

            this.PermanentId = permanentId;

            this.CartId = cartId;

            this.RegistrationId = registrationId;
        }

        /// <summary>
        /// egyedi látogató azonosító
        /// </summary>
        public string ObjectId { set; get; }

        /// <summary>
        /// beállított nyelv
        /// </summary>
        public string Language { set; get; }

        /// <summary>
        /// kosár nyitva van-e?
        /// </summary>
        public bool IsShoppingCartOpened { set; get; }

        /// <summary>
        /// katalógus nyitva van-e?
        /// </summary>
        public bool IsCatalogueOpened { set; get; }

        /// <summary>
        /// beállított valutanem
        /// </summary>
        public string Currency { set; get; }

        /// <summary>
        /// megmarado objectId
        /// </summary>
        public string PermanentId { set; get; }

        /// <summary>
        /// kosár azonosító
        /// </summary>
        public string CartId { set; get; }

        /// <summary>
        /// regisztrációs azonosító
        /// </summary>
        public string RegistrationId { set; get; }
    }
}
