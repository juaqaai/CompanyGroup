﻿using System;
using System.Collections.Generic;
using Orchard.UI.Resources;

namespace Webshop
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("CustomerService").SetUrl("customer.service.js", "customer.service.js").SetVersion("1.0.0").SetDependencies("jQuery");

            manifest.DefineScript("CustomerModel").SetUrl("customer.model.js", "customer.model.js").SetVersion("1.0.0").SetDependencies("jQuery");

         }
    }
}
