using System;
using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Settings
{
    public class ItemCounterPartTypePartSettings
    {
        public VisitCounterType Type { get; set; }
    }

    public class ItemCounterPartSettingsHooks : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "ItemCounterPart")
                yield break;
            var model = definition.Settings.GetModel<ItemCounterPartTypePartSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "ItemCounterPart")
                yield break;

            var model = new ItemCounterPartTypePartSettings();

            updateModel.TryUpdateModel(model, "ItemCounterPartTypePartSettings", null, null);
            builder.WithSetting("ItemCounterPartTypePartSettings.Type", Enum.GetName(typeof(VisitCounterType), model.Type));
            yield return DefinitionTemplate(model);
        }

    }
}
