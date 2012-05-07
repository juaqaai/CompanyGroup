using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKEditor.Services
{
    public class CKEditorConfigContext
    {

        public Dictionary<String, ICKEditorConfigPair> Config = new Dictionary<string, ICKEditorConfigPair>();

        public void Add(ICKEditorConfigPair pair)
        {
            var key = pair.Key;
            if (!String.IsNullOrWhiteSpace(key))
            {
                Config[pair.Key] = pair;
            }
        }
    }
}
