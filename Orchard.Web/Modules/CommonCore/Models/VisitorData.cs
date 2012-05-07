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
            ObjectId = String.Empty;

            Language = String.Empty;        
        }

        public VisitorData(string objectId, string language)
        {
            ObjectId = objectId;

            Language = language;
        }

        public string ObjectId { set; get; }

        public string Language { set; get; }
    }
}
