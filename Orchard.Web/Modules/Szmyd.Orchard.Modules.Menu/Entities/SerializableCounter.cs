using System;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Entities
{
    /// <summary>
    /// Serializable counter for allowing counter session storage in SQL Server Session mode.
    /// </summary>
    [Serializable]
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class SerializableCounter
    {
        public int Id { get; set; }
        public int ForItemId { get; set; }
        public int InContextOfItemId { get; set; }
        public int Count { get; set; }
        public CounterType Type { get; set; }
        public DateTime LastModified { get; set; }
    }
}