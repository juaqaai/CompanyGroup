using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace Szmyd.Orchard.Modules.Menu.Settings
{
    /// <summary>
    /// Content type settings for SubMenu part
    /// </summary>
    public class MenuWidgetPartTypePartSettings
    {
        private string _menuName = "main";

        /// <summary>
        /// Name of the corresponding menu.
        /// </summary>
        public string MenuName {
            get { return _menuName; }
            set { _menuName = value; }
        }
    }

    /// <summary>
    /// Overrides default editors to enable putting settings on SubMenu part.
    /// </summary>
    public class MenuWidgetPartSettingsHooks : ContentDefinitionEditorEventsBase
    {

        /// <summary>
        /// Overrides editor shown when part is attached to content type. Enables adding setting field to the content part
        /// attached.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "MenuWidgetPart")
                yield break;
            var model = definition.Settings.GetModel<MenuWidgetPartTypePartSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "MenuWidgetPart")
                yield break;

            var model = new MenuWidgetPartTypePartSettings();

            updateModel.TryUpdateModel(model, "MenuWidgetPartTypePartSettings", null, null);
            builder.WithSetting("MenuWidgetPartTypePartSettings.MenuName", model.MenuName);
            yield return DefinitionTemplate(model);
        }

    }
}
