﻿using System;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Localization;

namespace Orchard.Core.Contents.Rules {
    public interface IEventProvider : IEventHandler {
        void Describe(dynamic describe);
    }

    [OrchardFeature("Contents.Rules")]
    public class ContentEvents : IEventProvider {
        public Localizer T { get; set; }

        public void Describe(dynamic describe) {
            Func<dynamic, bool> contentHasPart = ContentHasPart;

            describe.For("Content", T("Content Items"), T("Content Items"))
                .Element("Created", T("Content Created"), T("Content is actually created."), contentHasPart, (Func<dynamic, LocalizedString>)(context => T("When content with types ({0}) is created.", FormatPartsList(context))), "SelectContentTypes")
                .Element("Versioned", T("Content Versioned"), T("Content is actually versioned."), contentHasPart, (Func<dynamic, LocalizedString>)(context => T("When content with types ({0}) is versioned.", FormatPartsList(context))), "SelectContentTypes")
                .Element("Published", T("Content Published"), T("Content is actually published."), contentHasPart, (Func<dynamic, LocalizedString>)(context => T("When content with types ({0}) is published.", FormatPartsList(context))), "SelectContentTypes")
                .Element("Removed", T("Content Removed"), T("Content is actually removed."), contentHasPart, (Func<dynamic, LocalizedString>)(context => T("When content with types ({0}) is removed.", FormatPartsList(context))), "SelectContentTypes");
        }

        private string FormatPartsList(dynamic context) {
            var contenttypes = context.Properties["contenttypes"];

            if (String.IsNullOrEmpty(contenttypes)) {
                return T("Any").Text;
            }

            return contenttypes;
        }

        private static bool ContentHasPart(dynamic context) {
            string contenttypes = context.Properties["contenttypes"];
            var content = context.Tokens["Content"] as IContent;

            // "" means 'any'
            if (String.IsNullOrEmpty(contenttypes)) {
                return true;
            }

            if (content == null) {
                return false;
            }

            var contentTypes = contenttypes.Split(new[] { ',' });

            return contentTypes.Any(contentType => content.ContentItem.TypeDefinition.Name == contentType);
        }
    }
}