using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CKEditor.Services;
using Orchard.FileSystems.VirtualPath;
using Orchard.Themes.Services;
using Orchard;
using CKEditor.Models;
using Orchard.ContentManagement;
using System.Web.Mvc;

namespace CKEditor.Providers
{
    public class DefaultConfigProvider : ICKEditorConfigProvider
    {
        private readonly ISiteThemeService _siteThemeService;
        private readonly IVirtualPathProvider _virtualPathProvider;
        public IOrchardServices Services { get; set; }

        public DefaultConfigProvider(
            IOrchardServices orchardServices,
            ISiteThemeService siteThemeService,
            IVirtualPathProvider virtualPathProvider
            )
        {

            _siteThemeService = siteThemeService;
            _virtualPathProvider = virtualPathProvider;
            Services = orchardServices;

        }

        public void PopulateConfiguration(CKEditorConfigContext config)
        {
            // Get CKEditor settings
            var ckSettings = Services.WorkContext.CurrentSite.As<CKEditorSettingsPart>();
            if (ckSettings != null)
            {
                if (!String.IsNullOrWhiteSpace(ckSettings.ContentsCss))
                {
                    // Push css link(s) into config
                    var currentTheme = _siteThemeService.GetSiteTheme();
                    var sheets =
                        // Get list of stylesheets
                        ckSettings.ContentsCss.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        // Transform to full URLs
                        .Select(sheet => string.Format("{0}/{1}/Styles/{2}", currentTheme.Location, currentTheme.Id, sheet))
                        .Where(path => _virtualPathProvider.FileExists(path)).Select(path => UrlHelper.GenerateContentUrl(path, Services.WorkContext.HttpContext)).ToList(); // Execute at this time to avoid trouble later
                    config.Add(new CKEditorConfigStringList("contentsCss",sheets));
                }
                if (!String.IsNullOrWhiteSpace(ckSettings.ExtraPlugins))
                {
                    // TODO: Plugins should add modularly from an ICKEditorPlugin interface?
                    var plugins =
                        ckSettings.ExtraPlugins;

                    config.Add(new CKEditorConfigString("extraPlugins",plugins));
                }
                // For removing standard styles if stylesheet parser is used
                if (ckSettings.ClearDefaultStyleSets)
                {
                    config.Add(new CKEditorConfigStringList("stylesSet", Enumerable.Empty<String>()));
                }
            }
        }
    }
}