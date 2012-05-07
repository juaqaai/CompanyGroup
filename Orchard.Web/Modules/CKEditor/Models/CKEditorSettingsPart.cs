using Orchard;
using Orchard.ContentManagement;
using System;

namespace CKEditor.Models {
	
    public class CKEditorSettingsPart : ContentPart<CKEditorSettingsPartRecord> {
        public String ContentsCss {
            get { return Record.ContentsCss; }
            set { Record.ContentsCss = value; }
        }

        public string ExtraPlugins {
            get { return Record.ExtraPlugins; }
            set { Record.ExtraPlugins = value; }
        }

        public bool ClearDefaultStyleSets
        {
            get { return Record.ClearDefaultStyleSets; }
            set { Record.ClearDefaultStyleSets = value; }
        }

    }
}
