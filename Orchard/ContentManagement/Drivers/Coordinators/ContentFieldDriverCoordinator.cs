﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement.FieldStorage;
using Orchard.ContentManagement.Handlers;
using Orchard.Logging;

namespace Orchard.ContentManagement.Drivers.Coordinators {
    [UsedImplicitly]
    public class ContentFieldDriverCoordinator : ContentHandlerBase {
        private readonly IEnumerable<IContentFieldDriver> _drivers;
        private readonly IFieldStorageProviderSelector _fieldStorageProviderSelector;

        public ContentFieldDriverCoordinator(
            IEnumerable<IContentFieldDriver> drivers,
            IFieldStorageProviderSelector fieldStorageProviderSelector) {
            _drivers = drivers;
            _fieldStorageProviderSelector = fieldStorageProviderSelector;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public override void Initializing(InitializingContentContext context) {
            var fieldInfos = _drivers.SelectMany(x => x.GetFieldInfo());
            var parts = context.ContentItem.Parts;
            foreach (var contentPart in parts) {
                foreach (var partFieldDefinition in contentPart.PartDefinition.Fields) {
                    var fieldTypeName = partFieldDefinition.FieldDefinition.Name;
                    var fieldInfo = fieldInfos.FirstOrDefault(x => x.FieldTypeName == fieldTypeName);
                    if (fieldInfo != null) {
                        var storage = _fieldStorageProviderSelector
                            .GetProvider(partFieldDefinition)
                            .BindStorage(contentPart, partFieldDefinition);
                        var field = fieldInfo.Factory(partFieldDefinition, storage);
                        contentPart.Weld(field);
                    }
                }
            }
        }

        public override void GetContentItemMetadata(GetContentItemMetadataContext context) {
            _drivers.Invoke(driver => driver.GetContentItemMetadata(context), Logger);
        }

        public override void BuildDisplay(BuildDisplayContext context) {
            _drivers.Invoke(driver => {
                var result = driver.BuildDisplayShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }

        public override void BuildEditor(BuildEditorContext context) {
            _drivers.Invoke(driver => {
                var result = driver.BuildEditorShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }

        public override void UpdateEditor(UpdateEditorContext context) {
            _drivers.Invoke(driver => {
                var result = driver.UpdateEditorShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }

        public override void Importing(ImportContentContext context) {
            foreach (var contentFieldDriver in _drivers) {
                contentFieldDriver.Importing(context);
            }
        }

        public override void Imported(ImportContentContext context) {
            foreach (var contentFieldDriver in _drivers) {
                contentFieldDriver.Imported(context);
            }
        }

        public override void Exporting(ExportContentContext context) {
            foreach (var contentFieldDriver in _drivers) {
                contentFieldDriver.Exporting(context);
            }
        }

        public override void Exported(ExportContentContext context) {
            foreach (var contentFieldDriver in _drivers) {
                contentFieldDriver.Exported(context);
            }
        }
    }
}