﻿using System;
using System.Web.Mvc;
using Orchard.ContentManagement.MetaData;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Localization;

namespace Orchard.Core.Contents.Rules {
    public interface IFormProvider : IEventHandler {
        void Describe(dynamic context);
    }

    [OrchardFeature("Contents.Rules")]
    public class ContentForms : IFormProvider {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public ContentForms(
            IShapeFactory shapeFactory,
            IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
            Shape = shapeFactory;
        }

        public void Describe(dynamic context) {
            Func<IShapeFactory, dynamic> form =
                shape => {

                    var f = Shape.Form(
                        Id: "AnyOfContentTypes",
                        _Parts: Shape.SelectList(
                            Id: "contenttypes", Name: "contenttypes",
                            Title: T("Content types"),
                            Description: T("Select some content types."),
                            Size: 10,
                            Multiple: true
                            )
                        );

                    f._Parts.Add(new SelectListItem { Value = "", Text = T("Any").Text });

                    foreach (var contentType in _contentDefinitionManager.ListTypeDefinitions()) {
                        f._Parts.Add(new SelectListItem { Value = contentType.Name, Text = contentType.DisplayName });
                    }

                    return f;
                };

            context.Form("SelectContentTypes", form);

        }
    }
}