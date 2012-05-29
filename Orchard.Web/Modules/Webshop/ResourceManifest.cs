using System;
using System.Collections.Generic;
using Orchard.UI.Resources;

namespace Webshop
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("WebshopModel").SetUrl("webshop.model.js", "webshop.model.js").SetVersion("1.0.0");

            manifest.DefineScript("CatalogueService").SetUrl("catalogue.service.js", "catalogue.service.js").SetVersion("1.0.0").SetDependencies("jQuery");

            //chosen from Harvest
            manifest.DefineScript("Chosen").SetUrl("chosen.js", "chosen.js").SetVersion("0.9.1").SetDependencies("jQuery");

            manifest.DefineScript("JTemplate").SetUrl("jquery.tmpl.min.js", "jquery.tmpl.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JSon").SetUrl("jquery.json-2.2.min.js", "jquery.json-2.2.min.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_EasyPaginate").SetUrl("easypaginate.min.js", "easypaginate.min.js").SetVersion("1.0.0").SetDependencies("jQuery");

            //manifest.DefineScript("JQuery_StickyBar").SetUrl("expstickybar.js", "expstickybar.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_FlexCroll").SetUrl("flexcroll.js", "flexcroll.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_HoverIntent").SetUrl("hoverIntent.js", "hoverIntent.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_AsmSelect").SetUrl("jquery.asmselect.js", "jquery.asmselect.js").SetVersion("1.0.4").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Easing").SetUrl("jquery.easing-1.3.pack.js", "jquery.easing-1.3.pack.js").SetVersion("1.3.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_FancyBox").SetUrl("jquery.fancybox-1.3.4.pack.js", "jquery.fancybox-1.3.4.pack.js").SetVersion("1.3.4").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_FloatingMessage").SetUrl("jquery.floatingmessage.js", "jquery.floatingmessage.js").SetVersion("1.0.1").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_JqEasyPanel").SetUrl("jquery.jqEasyPanel.js", "jquery.jqEasyPanel.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_MouseWheel").SetUrl("jquery.mousewheel-3.0.4.pack.js", "jquery.mousewheel-3.0.4.pack.js").SetVersion("3.0.4").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_MultiSelect").SetUrl("jquery.multiselect.js", "jquery.multiselect.js").SetVersion("1.12.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Tabify").SetUrl("jquery.tabify.js", "jquery.tabify.js").SetVersion("1.12.0").SetDependencies("jQuery");

                //manifest.DefineScript("JQuery_Ui").SetUrl("jquery.ui.js", "jquery.ui.js").SetVersion("1.5.1").SetDependencies("jQuery");
                //manifest.DefineScript("JQuery_Ui").SetUrl("jquery-ui-1.8.10.custom.min.js", "jquery-ui-1.8.10.custom.min.js").SetVersion("1.5.1").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Uniform").SetUrl("jquery.uniform.js", "jquery.uniform.min.js").SetVersion("1.5.1").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Vegas").SetUrl("jquery.vegas.js", "jquery.vegas.js").SetVersion("1.2.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Prettify").SetUrl("prettify.js", "prettify.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_Flash").SetUrl("jquery.flash.js", "jquery.flash.js").SetVersion("1.0.1").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_MaskedInput").SetUrl("jquery.maskedinput-1.3.min.js", "jquery.maskedinput-1.3.min.js").SetVersion("1.3.0").SetDependencies("jQuery");

            manifest.DefineScript("JQuery_PrintElement").SetUrl("jquery.printElement.min.js", "jquery.printElement.min.js").SetVersion("1.2.0").SetDependencies("jQuery");

            manifest.DefineScript("jQuery_Validate").SetUrl("jquery.validate.min.js").SetVersion("1.8.0").SetDependencies("jQuery");

            manifest.DefineScript("jQuery_UICore").SetUrl("jquery.ui.core.js").SetVersion("1.0.0").SetDependencies("jQuery");
            manifest.DefineScript("jQuery_UIDatePicker").SetUrl("jquery.ui.datepicker.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("Knockout").SetUrl("knockout-2.0.0.js").SetVersion("2.0.0");

            manifest.DefineScript("Constants").SetUrl("application.constants.js", "application.constants.js").SetVersion("1.0.0");

            manifest.DefineScript("ShoppingCart").SetUrl("shoppingcart.model.js", "shoppingcart.model.js").SetVersion("1.0.0").SetDependencies("Knockout");

            manifest.DefineScript("Catalogue").SetUrl("catalogue.model.js", "catalogue.model.js").SetVersion("1.0.0").SetDependencies("Knockout");

            manifest.DefineScript("Requests").SetUrl("catalogue.model.js", "application.requests.js").SetVersion("1.0.0");
        }
    }
}
