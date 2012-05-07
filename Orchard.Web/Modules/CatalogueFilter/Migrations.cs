using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Cms.CatalogueFilter {

    /// <summary>
    /// data migration for our Maps module
    /// 
    /// </summary>
    /// <remarks>
    /// The reason is that defining a Record and Part class to store the data doesn't actually impact the database in any way. 
    /// A data migration is what tells Orchard how to update the database schema when the Maps feature is enabled 
    /// (the migration runs when the feature is activated). 
    /// A migration can also upgrade the database schema from prior versions of a module to the schema required by a newer version of a module.
    /// </remarks>
    public class Migrations : DataMigrationImpl {

        /// <summary>
        /// The migration class contains a single Create() method that defines a database table structure based on the Record classes in project. 
        /// Because we only have a single MapRecord class with latitude and longitude properties, the migration class is fairly simple. 
        /// Note that the Create method is called at the time the feature is activated, and the database will be updated accordingly.
        /// </summary>
        /// <returns></returns>
        public int Create() {
            return 1;
        }

        public int UpdateFrom1()
        {
            // Create a new widget content type 
            ContentDefinitionManager.AlterTypeDefinition("CatalogueFilterWidget", cfg => cfg
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 2;
        }
    }
}