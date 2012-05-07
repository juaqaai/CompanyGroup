using System;
using Maps.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Maps.Drivers {

    /// <summary>
    /// Add a driver for our Map part. 
    /// A driver in Orchard is a class that can define associations of shapes to display for each context in which the Map part can render. 
    /// For example, when displaying a Map on the front-end, a "Display" method defines the name of the template to use for different displayTypes 
    /// (for example, "details" or summary"). 
    /// Similarly, an "Editor" method of the driver defines the template to use for displaying the editor of the Map part 
    /// (for entering values of the latitude and longitude fields). 
    /// We are going to keep this part simple and just use "Map" as the name of the shape to use for both Display and Editor contexts 
    /// (and all displayTypes).
    /// </summary>
    public class MapDriver : ContentPartDriver<MapPart> {
        protected override DriverResult Display(
            MapPart part, string displayType, dynamic shapeHelper) {

            return ContentShape("Parts_Map", () => shapeHelper.Parts_Map(
                Longitude: part.Longitude,
                Latitude: part.Latitude));
        }

        //GET
        protected override DriverResult Editor(
            MapPart part, dynamic shapeHelper) {

            return ContentShape("Parts_Map_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/Map",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            MapPart part, IUpdateModel updater, dynamic shapeHelper) {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}