using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CKEditor.Services;
using System.Web.Helpers;
using System.Web;

namespace CKEditor.Providers
{
    /// <summary>
    /// Config pair for the contentsCss config setting
    /// </summary>
    public class CKEditorConfigStringList : ICKEditorConfigPair
    {
        public List<string> Items { get; protected set; }

        public CKEditorConfigStringList(string key, IEnumerable<string> strings)
        {
            Key = key;
            Items = strings.ToList();
        }

        public string Key { get; set; }

        public IHtmlString Value
        {
            get { return new HtmlString(Json.Encode(Items)); }
        }
    }
}
