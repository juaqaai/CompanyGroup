using System;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Maps.Models {
    /// <summary>
    /// content part data is represented by a Record class, which represents the fields that are stored to a database table, 
    /// </summary>
    public class MapRecord : ContentPartRecord {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }

    /// <summary>
    /// ContentPart class that uses the Record for storage
    /// </summary>
    public class MapPart : ContentPart<MapRecord> {
        [Required]
        public double Latitude
        {
            get { return Record.Latitude; }
            set { Record.Latitude = value; }
        }

        [Required]
        public double Longitude
        {
            get { return Record.Longitude; }
            set { Record.Longitude = value; }
        }
    }
}