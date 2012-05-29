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
        }

        public VisitorData(string objectId, string language, bool isShoppingCartOpened, bool isCatalogueOpened)
        {
            this.ObjectId = objectId;

            this.Language = language;

            this.IsShoppingCartOpened = isShoppingCartOpened;

            this.IsCatalogueOpened = isCatalogueOpened;
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
    }
}
