using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Themes.Services;
using Orchard.FileSystems.VirtualPath;
using Orchard;
using CKEditor.Models;
using Orchard.ContentManagement;
using Orchard.DisplayManagement.Implementation;
using CKEditor.Services;

namespace CKEditor
{
    public class Shapes : IShapeTableProvider
    {
        private readonly ISiteThemeService _siteThemeService;
        private readonly IVirtualPathProvider _virtualPathProvider;
        private readonly Lazy<IEnumerable<ICKEditorConfigProvider>> _ckEditorConfigProviders;
        public IOrchardServices Services { get; set; }
        
        public Shapes(
            IOrchardServices orchardServices,
            ISiteThemeService siteThemeService,
            IVirtualPathProvider virtualPathProvider,
            Lazy<IEnumerable<ICKEditorConfigProvider>> configProviders
            ) {

            _siteThemeService = siteThemeService;
            _virtualPathProvider = virtualPathProvider;
            _ckEditorConfigProviders = configProviders;

            Services = orchardServices;

        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("CKEditor_Config")
                .OnDisplaying(CKEditor_Config_Displaying);
        }

        public void CKEditor_Config_Displaying(ShapeDisplayingContext displaying)
        {
            // Build other key/value pairs
            var context = new CKEditorConfigContext();
            foreach (var provider in _ckEditorConfigProviders.Value) {
                provider.PopulateConfiguration(context);
            }
            displaying.Shape.ConfigPairs = context.Config.Values;
        }
    }
}