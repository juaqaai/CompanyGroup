using Orchard.UI.Resources;

namespace Szmyd.Orchard.Modules.Menu.Resources {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("SuperfishNavbar").SetUrl("superfish-navbar.css").SetVersion("1.4.8");
            manifest.DefineStyle("SuperfishVertical").SetUrl("superfish-vertical.css").SetVersion("1.4.8");
            manifest.DefineStyle("Superfish").SetUrl("superfish.css").SetVersion("1.4.8");

            manifest.DefineScript("Bgiframe").SetUrl("jquery.bgiframe.min.js").SetVersion("2.1").SetDependencies("jQuery");
            manifest.DefineScript("HoverIntent").SetUrl("hoverIntent.js").SetDependencies("jQuery");
            manifest.DefineScript("Superfish").SetUrl("superfish.js").SetVersion("1.4.8").SetDependencies("jQuery", "HoverIntent", "Bgiframe");
            manifest.DefineScript("Supersubs").SetUrl("supersubs.js").SetVersion("0.2a").SetDependencies("jQuery", "Superfish");
        }
    }
}
