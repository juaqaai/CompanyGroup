using JetBrains.Annotations;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using CKEditor.Models;
using Orchard.Localization;
using Orchard.ContentManagement;

namespace CKEditor.Handlers
{
    [UsedImplicitly]
    public class CKEditorSettingsPartHandler : ContentHandler
    {
        public CKEditorSettingsPartHandler(IRepository<CKEditorSettingsPartRecord> repository)
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<CKEditorSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new TemplateFilterForRecord<CKEditorSettingsPartRecord>("CKEditorSettings", "Parts.CKEditor.CKEditorSettings", "ckeditor"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("CKEditor")));
        }

    }
}
