﻿using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Shapes;

namespace Orchard.ContentManagement.Drivers {
    public abstract class ContentFieldDriver<TField> : IContentFieldDriver where TField : ContentField, new() {
        protected virtual string Prefix { get { return ""; } }
        protected virtual string Zone { get { return "Content"; } }

        void IContentFieldDriver.GetContentItemMetadata(GetContentItemMetadataContext context) {            
            Process(context.ContentItem, (part, field) => GetContentItemMetadata(part, field, context.Metadata));
        }

        DriverResult IContentFieldDriver.BuildDisplayShape(BuildDisplayContext context) {
            return Process(context.ContentItem, (part, field) => Display(part, field, context.DisplayType, context.New));
        }

        DriverResult IContentFieldDriver.BuildEditorShape(BuildEditorContext context) {
            return Process(context.ContentItem, (part, field) => Editor(part, field, context.New));
        }

        DriverResult IContentFieldDriver.UpdateEditorShape(UpdateEditorContext context) {
            return Process(context.ContentItem, (part, field) => Editor(part, field, context.Updater, context.New));
        }

        void IContentFieldDriver.Importing(ImportContentContext context) {
            Process(context.ContentItem, (part, field) => Importing(part, field, context));
        }

        void IContentFieldDriver.Imported(ImportContentContext context) {
            Process(context.ContentItem, (part, field) => Imported(part, field, context));
        }

        void IContentFieldDriver.Exporting(ExportContentContext context) {
            Process(context.ContentItem, (part, field) => Exporting(part, field, context));
        }

        void IContentFieldDriver.Exported(ExportContentContext context) {
            Process(context.ContentItem, (part, field) => Exported(part, field, context));
        }

        void Process(ContentItem item, Action<ContentPart, TField> effort) {
            var occurences = item.Parts.SelectMany(part => part.Fields.OfType<TField>().Select(field => new { part, field }));
            foreach (var occurence in occurences) {
                effort(occurence.part, occurence.field);
            }
        }

        DriverResult Process(ContentItem item, Func<ContentPart, TField, DriverResult> effort) {
            var results = item.Parts
                    .SelectMany(part => part.Fields.OfType<TField>().Select(field => new { part, field }))
                    .Select(pf => effort(pf.part, pf.field));
            return Combined(results.ToArray());
        }

        public IEnumerable<ContentFieldInfo> GetFieldInfo() {
            var contentFieldInfo = new[] {
                new ContentFieldInfo {
                    FieldTypeName = typeof (TField).Name,
                    Factory = (partFieldDefinition, storage) => new TField {
                        PartFieldDefinition = partFieldDefinition,
                        Storage = storage,
                    }
                }
            };

            return contentFieldInfo;
        }

        protected virtual void GetContentItemMetadata(ContentPart part, TField field, ContentItemMetadata metadata) { return; }

        protected virtual DriverResult Display(ContentPart part, TField field, string displayType, dynamic shapeHelper) { return null; }
        protected virtual DriverResult Editor(ContentPart part, TField field, dynamic shapeHelper) { return null; }
        protected virtual DriverResult Editor(ContentPart part, TField field, IUpdateModel updater, dynamic shapeHelper) { return null; }
        
        protected virtual void Importing(ContentPart part, TField field, ImportContentContext context) { }
        protected virtual void Imported(ContentPart part, TField field, ImportContentContext context) { }
        protected virtual void Exporting(ContentPart part, TField field, ExportContentContext context) { }
        protected virtual void Exported(ContentPart part, TField field, ExportContentContext context) { }

        public ContentShapeResult ContentShape(string shapeType, Func<dynamic> factory) {
            return ContentShapeImplementation(shapeType, null, ctx => factory());
        }

        public ContentShapeResult ContentShape(string shapeType, string differentiator, Func<dynamic> factory) {
            return ContentShapeImplementation(shapeType, differentiator, ctx => factory());
        }

        public ContentShapeResult ContentShape(string shapeType, Func<dynamic, dynamic> factory) {
            return ContentShapeImplementation(shapeType, null, ctx => factory(CreateShape(ctx, shapeType)));
        }

        public ContentShapeResult ContentShape(string shapeType, string differentiator, Func<dynamic, dynamic> factory) {
            return ContentShapeImplementation(shapeType, differentiator, ctx => factory(CreateShape(ctx, shapeType)));
        }

        private ContentShapeResult ContentShapeImplementation(string shapeType, string differentiator, Func<BuildShapeContext, object> shapeBuilder) {
            return new ContentShapeResult(shapeType, Prefix, ctx => AddAlternates(shapeBuilder(ctx), differentiator)).Differentiator(differentiator);
        }

        private static object AddAlternates(dynamic shape, string differentiator) {
            // automatically add shape alternates for shapes added by fields
            // for fields on dynamic parts the part name is the same as the content type name

            ShapeMetadata metadata = shape.Metadata;
            ContentPart part = shape.ContentPart;
            var shapeType = metadata.Type;
            var fieldName = differentiator ?? String.Empty;
            var partName = part != null ? part.PartDefinition.Name : String.Empty;
            var contentType = part != null ? part.ContentItem.ContentType : String.Empty;
            var dynamicType = string.Equals(partName, contentType, StringComparison.Ordinal);

            // [ShapeType__FieldName] e.g. Fields/Common.Text-Teaser
            if ( !string.IsNullOrEmpty(fieldName) )
                metadata.Alternates.Add(shapeType + "__" + fieldName);

            // [ShapeType__PartName] e.g. Fields/Common.Text-TeaserPart
            if ( !string.IsNullOrEmpty(partName) ) {
                metadata.Alternates.Add(shapeType + "__" + partName);
            }

            // [ShapeType]__[ContentType]__[PartName] e.g. Fields/Common.Text-Blog-TeaserPart
            if ( !string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(contentType) && !dynamicType ) {
                metadata.Alternates.Add(shapeType + "__" + contentType + "__" + partName);
            }

            // [ShapeType]__[PartName]__[FieldName] e.g. Fields/Common.Text-TeaserPart-Teaser
            if ( !string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(fieldName) ) {
                metadata.Alternates.Add(shapeType + "__" + partName + "__" + fieldName);
            }

            // [ShapeType]__[ContentType]__[FieldName] e.g. Fields/Common.Text-Blog-Teaser
            if ( !string.IsNullOrEmpty(contentType) && !string.IsNullOrEmpty(fieldName) ) {
                metadata.Alternates.Add(shapeType + "__" + contentType + "__" + fieldName);
            }

            // [ShapeType]__[ContentType]__[PartName]__[FieldName] e.g. Fields/Common.Text-Blog-TeaserPart-Teaser
            if ( !string.IsNullOrEmpty(contentType) && !string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(fieldName) && !dynamicType ) {
                metadata.Alternates.Add(shapeType + "__" + contentType + "__" + partName );
            }
            
            return shape;
        }

        private static object CreateShape(BuildShapeContext context, string shapeType) {
            IShapeFactory shapeFactory = context.New;
            return shapeFactory.Create(shapeType);
        }

        [Obsolete]
        public ContentTemplateResult ContentFieldTemplate(object model) {
            return new ContentTemplateResult(model, null, Prefix).Location(Zone);
        }
        [Obsolete]
        public ContentTemplateResult ContentFieldTemplate(object model, string template) {
            return new ContentTemplateResult(model, template, Prefix).Location(Zone);
        }
        [Obsolete]
        public ContentTemplateResult ContentFieldTemplate(object model, string template, string prefix) {
            return new ContentTemplateResult(model, template, prefix).Location(Zone);
        }

        public CombinedResult Combined(params DriverResult[] results) {
            return new CombinedResult(results);
        }
    }
}