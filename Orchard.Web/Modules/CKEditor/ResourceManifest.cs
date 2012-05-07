using Orchard.UI.Resources;

namespace CKEditor {
	public class ResourceManifest : IResourceManifestProvider{
		public void BuildManifests(ResourceManifestBuilder builder){
			builder.Add().DefineScript("CKEditor").SetUrl("ckeditor/ckeditor.js");
		}
	}
}
