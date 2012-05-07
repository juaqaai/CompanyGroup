using System;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models {
    /// <summary>
    /// Record for storing count info about specific content item.
    /// </summary>
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class CounterRecord {
        public virtual int Id { get; set; }

        /// <summary>
        /// Item the counter counts for
        /// </summary>
        public virtual int ForItemId { get; set; }

        /// <summary>
        /// Item, in context of which the couter runs
        /// </summary>
        public virtual int InContextOfItemId { get; set; }

        /// <summary>
        /// Current count
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// Type of the counter for distinguishing different ones.
        /// </summary>
        public virtual CounterType Type { get; set; }

        /// <summary>
        /// Last modified
        /// </summary>
        public virtual DateTime LastModified { get; set; }
    }
}