﻿using Orchard.ContentManagement;
using Orchard.DisplayManagement.Descriptors;

namespace Orchard.Core.Contents {
    public class Shapes : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Content")
                .OnCreated(created => {
                    var content = created.Shape;
                    content.Child.Add(created.New.PlaceChildContent(Source: content));
                })
                .OnDisplaying(displaying => {
                    ContentItem contentItem = displaying.Shape.ContentItem;
                    if (contentItem != null) {
                        // Alternates in order of specificity. 
                        // Display type > content type > specific content > display type for a content type > display type for specific content

                        // Content__[DisplayType] e.g. Content.Summary
                        displaying.ShapeMetadata.Alternates.Add("Content_" + displaying.ShapeMetadata.DisplayType);

                        // Content__[ContentType] e.g. Content-BlogPost
                        displaying.ShapeMetadata.Alternates.Add("Content__" + contentItem.ContentType);

                        // Content__[Id] e.g. Content-42
                        displaying.ShapeMetadata.Alternates.Add("Content__" + contentItem.Id);

                        // Content_[DisplayType]__[ContentType] e.g. Content-BlogPost.Summary
                        displaying.ShapeMetadata.Alternates.Add("Content_" + displaying.ShapeMetadata.DisplayType + "__" + contentItem.ContentType);

                        // Content_[DisplayType]__[Id] e.g. Content-42.Summary
                        displaying.ShapeMetadata.Alternates.Add("Content_" +  displaying.ShapeMetadata.DisplayType + "__" + contentItem.Id);

                        if ( !displaying.ShapeMetadata.DisplayType.Contains("Admin") )
                            displaying.ShapeMetadata.Wrappers.Add("Content_ControlWrapper");
                    }
                });
        }
    }
}
