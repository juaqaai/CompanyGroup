using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CKEditor.Services
{
    public interface ICKEditorConfigPair
    {

        string Key { get; }
        IHtmlString Value { get; }

    }
}
