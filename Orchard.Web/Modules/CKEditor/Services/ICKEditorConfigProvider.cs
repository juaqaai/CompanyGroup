using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace CKEditor.Services
{
    public interface ICKEditorConfigProvider : IDependency
    {
        void PopulateConfiguration(CKEditorConfigContext config);
    }
}
