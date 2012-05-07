using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Environment.Extensions;

namespace Szmyd.Orchard.Modules.Menu.Settings
{
    /// <summary>
    /// Content type settings for SilverlightPart.
    /// </summary>
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPartTypePartSettings
    {
        /// <summary>
        /// The default source file.
        /// </summary>
        public bool ShowCounts { get; set; }

    }

    /// <summary>
    /// Overrides default editors to enable putting settings on RecentlySeen part.
    /// </summary>
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPartSettingsHooks : ContentDefinitionEditorEventsBase
    {

        /// <summary>
        /// Overrides editor shown when part is attached to content type. Enables adding setting field to the content part
        /// attached.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "RecentlySeenPart")
                yield break;
            var model = definition.Settings.GetModel<RecentlySeenPartTypePartSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "RecentlySeenPart")
                yield break;

            var model = new RecentlySeenPartTypePartSettings();

            updateModel.TryUpdateModel(model, "RecentlySeenPartTypePartSettings", null, null);
            builder.WithSetting("RecentlySeenPartTypePartSettings.ShowCounts", model.ShowCounts.ToString());
            yield return DefinitionTemplate(model);
        }

    }
}
