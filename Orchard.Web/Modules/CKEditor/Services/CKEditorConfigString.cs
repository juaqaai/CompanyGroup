using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CKEditor.Services
{
    public class CKEditorConfigString : ICKEditorConfigPair
    {
        public CKEditorConfigString(string key, string value)
        {
            Key = key;
            _unquotedValue = value;
        }

        public string Key
        {
            get;
            set;
        }

        protected string _unquotedValue;

        public IHtmlString Value
        {
            get { 
                // JSON string must be quoted
                // TODO: Needs any further encoding?
                return new HtmlString(Json.Encode(_unquotedValue));
            }
        }
    }
}