using Orchard;
using Orchard.ContentManagement.Records;
using System;
using System.ComponentModel.DataAnnotations;

namespace CKEditor.Models {
	
    public class CKEditorSettingsPartRecord : ContentPartRecord {
        public const string DefaultContentsCss = "Site.css";
        private string _contentscss = DefaultContentsCss;
        public const string DefaultExtraPlugins = "stylesheetparser";
        private string _extraplugins = DefaultExtraPlugins;
        public const bool DefaultClearDefaultStyleSets = true;
        private bool _cleardefaultstylesets = DefaultClearDefaultStyleSets;

        [StringLength(255)]
        public virtual String ContentsCss
        {
            get { return _contentscss; }
            set { _contentscss = value; }
        }
        [StringLength(1024)]
        public virtual string ExtraPlugins
        {
            get { return _extraplugins; }
            set { _extraplugins = value; }
        }

        public virtual bool ClearDefaultStyleSets
        {
            get { return _cleardefaultstylesets; }
            set { _cleardefaultstylesets = value; }
        }
    }
}