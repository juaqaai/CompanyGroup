using System;
using Maps.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Maps.Handlers {

    /// <summary>
    /// Handler for the Map part. 
    /// A handler in Orchard is a class that defines the behavior of the part, handling events or manipulating data model prior to rendering the part. 
    /// The Map part is very simple, and in this case, our handler class will only specify that an IRepository of MapRecord should be used as the storage for this part. 
    /// </summary>
    public class MapHandler : ContentHandler {
        public MapHandler(IRepository<MapRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
