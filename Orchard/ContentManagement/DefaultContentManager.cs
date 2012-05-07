﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Autofac;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.Records;
using Orchard.Data;
using Orchard.Indexing;
using Orchard.Logging;
using Orchard.UI;

namespace Orchard.ContentManagement {
    public class DefaultContentManager : IContentManager {
        private readonly IComponentContext _context;
        private readonly IRepository<ContentTypeRecord> _contentTypeRepository;
        private readonly IRepository<ContentItemRecord> _contentItemRepository;
        private readonly IRepository<ContentItemVersionRecord> _contentItemVersionRepository;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly Func<IContentManagerSession> _contentManagerSession;
        private readonly Lazy<IContentDisplay> _contentDisplay;
        private readonly Lazy<ISessionLocator> _sessionLocator; 
        private const string Published = "Published";
        private const string Draft = "Draft";

        public DefaultContentManager(
            IComponentContext context,
            IRepository<ContentTypeRecord> contentTypeRepository,
            IRepository<ContentItemRecord> contentItemRepository,
            IRepository<ContentItemVersionRecord> contentItemVersionRepository,
            IContentDefinitionManager contentDefinitionManager,
            Func<IContentManagerSession> contentManagerSession,
            Lazy<IContentDisplay> contentDisplay,
            Lazy<ISessionLocator> sessionLocator) {
            _context = context;
            _contentTypeRepository = contentTypeRepository;
            _contentItemRepository = contentItemRepository;
            _contentItemVersionRepository = contentItemVersionRepository;
            _contentDefinitionManager = contentDefinitionManager;
            _contentManagerSession = contentManagerSession;
            _contentDisplay = contentDisplay;
            _sessionLocator = sessionLocator;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        private IEnumerable<IContentHandler> _handlers;
        public IEnumerable<IContentHandler> Handlers {
            get {
                if (_handlers == null) {
                    _handlers = _context.Resolve<IEnumerable<IContentHandler>>();
                }
                    
                return _handlers;
            }
        }

        public IEnumerable<ContentTypeDefinition> GetContentTypeDefinitions() {
            return _contentDefinitionManager.ListTypeDefinitions();
        }

        public virtual ContentItem New(string contentType) {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(contentType);
            if (contentTypeDefinition == null) {
                contentTypeDefinition = new ContentTypeDefinitionBuilder().Named(contentType).Build();
            }

            // create a new kernel for the model instance
            var context = new ActivatingContentContext {
                ContentType = contentTypeDefinition.Name,
                Definition = contentTypeDefinition,
                Builder = new ContentItemBuilder(contentTypeDefinition)
            };

            // invoke handlers to weld aspects onto kernel
            Handlers.Invoke(handler => handler.Activating(context), Logger);

            var context2 = new ActivatedContentContext {
                ContentType = contentType,
                ContentItem = context.Builder.Build()
            };

            // back-reference for convenience (e.g. getting metadata when in a view)
            context2.ContentItem.ContentManager = this;

            Handlers.Invoke(handler => handler.Activated(context2), Logger);

            var context3 = new InitializingContentContext {
                ContentType = context2.ContentType,
                ContentItem = context2.ContentItem,
            };

            Handlers.Invoke(handler => handler.Initializing(context3), Logger);

            // composite result is returned
            return context3.ContentItem;
        }

        public virtual ContentItem Get(int id) {
            return Get(id, VersionOptions.Published);
        }

        public virtual ContentItem Get(int id, VersionOptions options) {
            var session = _contentManagerSession();
            ContentItem contentItem;

            ContentItemVersionRecord versionRecord = null;

            // obtain the root records based on version options
            if (options.VersionRecordId != 0) {
                // short-circuit if item held in session
                if (session.RecallVersionRecordId(options.VersionRecordId, out contentItem)) {
                    return contentItem;
                }

                // locate explicit version record
                versionRecord = _contentItemVersionRepository.Get(options.VersionRecordId);
            }
            else {
                var record = _contentItemRepository.Get(id);
                if (record != null)
                    versionRecord = GetVersionRecord(options, record);
            }

            // no record means content item doesn't exist
            if (versionRecord == null) {
                return null;
            }

            // return item if obtained earlier in session
            if (session.RecallVersionRecordId(versionRecord.Id, out contentItem)) {
                if (options.IsDraftRequired && versionRecord.Published) {
                    return BuildNewVersion(contentItem);
                }
                return contentItem;
            }

            // allocate instance and set record property
            contentItem = New(versionRecord.ContentItemRecord.ContentType.Name);
            contentItem.VersionRecord = versionRecord;

            // store in session prior to loading to avoid some problems with simple circular dependencies
            session.Store(contentItem);

            // create a context with a new instance to load            
            var context = new LoadContentContext(contentItem);

            // invoke handlers to acquire state, or at least establish lazy loading callbacks
            Handlers.Invoke(handler => handler.Loading(context), Logger);
            Handlers.Invoke(handler => handler.Loaded(context), Logger);

            // when draft is required and latest is published a new version is appended 
            if (options.IsDraftRequired && versionRecord.Published) {
                return BuildNewVersion(context.ContentItem);
            }

            return context.ContentItem;
        }

        private ContentItemVersionRecord GetVersionRecord(VersionOptions options, ContentItemRecord itemRecord) {
            if (options.IsPublished) {
                return itemRecord.Versions.FirstOrDefault(
                    x => x.Published) ??
                       _contentItemVersionRepository.Get(
                           x => x.ContentItemRecord == itemRecord && x.Published);
            }
            if (options.IsLatest || options.IsDraftRequired) {
                return itemRecord.Versions.FirstOrDefault(
                    x => x.Latest) ??
                       _contentItemVersionRepository.Get(
                           x => x.ContentItemRecord == itemRecord && x.Latest);
            }
            if (options.IsDraft) {
                return itemRecord.Versions.FirstOrDefault(
                    x => x.Latest && !x.Published) ??
                       _contentItemVersionRepository.Get(
                           x => x.ContentItemRecord == itemRecord && x.Latest && !x.Published);
            }
            if (options.VersionNumber != 0) {
                return itemRecord.Versions.FirstOrDefault(
                    x => x.Number == options.VersionNumber) ??
                       _contentItemVersionRepository.Get(
                           x => x.ContentItemRecord == itemRecord && x.Number == options.VersionNumber);
            }
            return null;
        }

        public virtual IEnumerable<ContentItem> GetAllVersions(int id) {
            return _contentItemVersionRepository
                .Fetch(x => x.ContentItemRecord.Id == id)
                .OrderBy(x => x.Number)
                .Select(x => Get(x.ContentItemRecord.Id, VersionOptions.VersionRecord(x.Id)));
        }

        public IEnumerable<T> GetMany<T>(IEnumerable<int> ids, VersionOptions options, QueryHints hints) where T : class, IContent {
            var contentItemVersionRecords = GetManyImplementation(hints, (contentItemCriteria, contentItemVersionCriteria) => {
                contentItemCriteria.Add(Restrictions.In("Id", ids.ToArray()));
                if (options.IsPublished) {
                    contentItemVersionCriteria.Add(Restrictions.Eq("Published", true));
                }
                else if (options.IsLatest) {
                    contentItemVersionCriteria.Add(Restrictions.Eq("Latest", true));
                }
                else if (options.IsDraft) {
                    contentItemVersionCriteria.Add(
                        Restrictions.And(Restrictions.Eq("Published", false),
                                        Restrictions.Eq("Latest", true)));
                }
            });
            var itemsById = contentItemVersionRecords
                .Select(r => Get(r.ContentItemRecord.Id, VersionOptions.VersionRecord(r.Id)))
                .GroupBy(ci => ci.Id)
                .ToDictionary(g => g.Key);

            return ids.SelectMany(id => {
                                      IGrouping<int, ContentItem> values;
                                      return itemsById.TryGetValue(id, out values) ? values : Enumerable.Empty<ContentItem>();
                                  }).AsPart<T>().ToArray();
        }
        
        public IEnumerable<T> GetManyByVersionId<T>(IEnumerable<int> versionRecordIds, QueryHints hints) where T : class, IContent {
            var contentItemVersionRecords = GetManyImplementation(hints, (contentItemCriteria, contentItemVersionCriteria) => 
                contentItemVersionCriteria.Add(Restrictions.In("Id", versionRecordIds.ToArray())));

            var itemsById = contentItemVersionRecords
                .Select(r => Get(r.ContentItemRecord.Id, VersionOptions.VersionRecord(r.Id)))
                .GroupBy(ci => ci.VersionRecord.Id)
                .ToDictionary(g => g.Key);

            return versionRecordIds.SelectMany(id => {
                IGrouping<int, ContentItem> values;
                return itemsById.TryGetValue(id, out values) ? values : Enumerable.Empty<ContentItem>();
            }).AsPart<T>().ToArray();
        }

        private IEnumerable<ContentItemVersionRecord> GetManyImplementation(QueryHints hints, Action<ICriteria, ICriteria> predicate) {
            var session = _sessionLocator.Value.For(typeof (ContentItemRecord));
            var contentItemVersionCriteria = session.CreateCriteria(typeof (ContentItemVersionRecord));
            var contentItemCriteria = contentItemVersionCriteria.CreateCriteria("ContentItemRecord");
            predicate(contentItemCriteria, contentItemVersionCriteria);
            
            var contentItemMetadata = session.SessionFactory.GetClassMetadata(typeof (ContentItemRecord));
            var contentItemVersionMetadata = session.SessionFactory.GetClassMetadata(typeof (ContentItemVersionRecord));

            // break apart and group hints by their first segment
            var hintDictionary = hints.Records
                .Select(hint=>new {Hint=hint, Segments=hint.Split('.')})
                .GroupBy(item=>item.Segments.FirstOrDefault())
                .ToDictionary(grouping=>grouping.Key, StringComparer.InvariantCultureIgnoreCase);

            // locate hints that match properties in the ContentItemVersionRecord
            foreach (var hit in contentItemVersionMetadata.PropertyNames.Where(hintDictionary.ContainsKey).SelectMany(key=>hintDictionary[key])) {
                contentItemVersionCriteria.SetFetchMode(hit.Hint, FetchMode.Eager);
                hit.Segments.Take(hit.Segments.Count() - 1).Aggregate(contentItemVersionCriteria, ExtendCriteria);
            }

            // locate hints that match properties in the ContentItemRecord
            foreach (var hit in contentItemMetadata.PropertyNames.Where(hintDictionary.ContainsKey).SelectMany(key=>hintDictionary[key])) {
                contentItemVersionCriteria.SetFetchMode("ContentItemRecord." + hit.Hint, FetchMode.Eager);
                hit.Segments.Take(hit.Segments.Count() - 1).Aggregate(contentItemCriteria, ExtendCriteria);
            }

            if (hintDictionary.SelectMany(x=>x.Value).Any(x=>x.Segments.Count() > 1))
                contentItemVersionCriteria.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return contentItemVersionCriteria.List<ContentItemVersionRecord>();
        }

        private static ICriteria ExtendCriteria(ICriteria criteria, string segment) {
            return criteria.GetCriteriaByPath(segment) ?? criteria.CreateCriteria(segment, JoinType.LeftOuterJoin);
        }

        public virtual void Publish(ContentItem contentItem) {
            if (contentItem.VersionRecord.Published) {
                return;
            }
            // create a context for the item and it's previous published record
            var previous = contentItem.Record.Versions.SingleOrDefault(x => x.Published);
            var context = new PublishContentContext(contentItem, previous);

            // invoke handlers to acquire state, or at least establish lazy loading callbacks
            Handlers.Invoke(handler => handler.Publishing(context), Logger);

            if (previous != null) {
                previous.Published = false;
            }
            contentItem.VersionRecord.Published = true;

            Handlers.Invoke(handler => handler.Published(context), Logger);
        }

        public virtual void Unpublish(ContentItem contentItem) {
            ContentItem publishedItem;
            if (contentItem.VersionRecord.Published) {
                // the version passed in is the published one
                publishedItem = contentItem;
            }
            else {
                // try to locate the published version of this item
                publishedItem = Get(contentItem.Id, VersionOptions.Published);
            }

            if (publishedItem == null) {
                // no published version exists. no work to perform.
                return;
            }

            // create a context for the item. the publishing version is null in this case
            // and the previous version is the one active prior to unpublishing. handlers
            // should take this null check into account
            var context = new PublishContentContext(contentItem, publishedItem.VersionRecord) {
                PublishingItemVersionRecord = null
            };

            Handlers.Invoke(handler => handler.Unpublishing(context), Logger);

            publishedItem.VersionRecord.Published = false;

            Handlers.Invoke(handler => handler.Unpublished(context), Logger);
        }

        public virtual void Remove(ContentItem contentItem) {
            var activeVersions = _contentItemVersionRepository.Fetch(x => x.ContentItemRecord == contentItem.Record && (x.Published || x.Latest));
            var context = new RemoveContentContext(contentItem);

            Handlers.Invoke(handler => handler.Removing(context), Logger);

            foreach (var version in activeVersions) {
                if (version.Published) {
                    version.Published = false;
                }
                if (version.Latest) {
                    version.Latest = false;
                }
            }

            Handlers.Invoke(handler => handler.Removed(context), Logger);
        }

        protected virtual ContentItem BuildNewVersion(ContentItem existingContentItem) {
            var contentItemRecord = existingContentItem.Record;

            // locate the existing and the current latest versions, allocate building version
            var existingItemVersionRecord = existingContentItem.VersionRecord;
            var buildingItemVersionRecord = new ContentItemVersionRecord {
                ContentItemRecord = contentItemRecord,
                Latest = true,
                Published = false,
                Data = existingItemVersionRecord.Data,
            };


            var latestVersion = contentItemRecord.Versions.SingleOrDefault(x => x.Latest);

            if (latestVersion != null) {
                latestVersion.Latest = false;
                buildingItemVersionRecord.Number = latestVersion.Number + 1;
            }
            else {
                buildingItemVersionRecord.Number = contentItemRecord.Versions.Max(x => x.Number) + 1;
            }

            contentItemRecord.Versions.Add(buildingItemVersionRecord);
            _contentItemVersionRepository.Create(buildingItemVersionRecord);

            var buildingContentItem = New(existingContentItem.ContentType);
            buildingContentItem.VersionRecord = buildingItemVersionRecord;

            var context = new VersionContentContext {
                Id = existingContentItem.Id,
                ContentType = existingContentItem.ContentType,
                ContentItemRecord = contentItemRecord,
                ExistingContentItem = existingContentItem,
                BuildingContentItem = buildingContentItem,
                ExistingItemVersionRecord = existingItemVersionRecord,
                BuildingItemVersionRecord = buildingItemVersionRecord,
            };
            Handlers.Invoke(handler => handler.Versioning(context), Logger);
            Handlers.Invoke(handler => handler.Versioned(context), Logger);

            return context.BuildingContentItem;
        }

        public virtual void Create(ContentItem contentItem) {
            Create(contentItem, VersionOptions.Published);
        }

        public virtual void Create(ContentItem contentItem, VersionOptions options) {
            // produce root record to determine the model id
            contentItem.VersionRecord = new ContentItemVersionRecord {
                ContentItemRecord = new ContentItemRecord {
                    ContentType = AcquireContentTypeRecord(contentItem.ContentType)
                },
                Number = 1,
                Latest = true,
                Published = true
            };
            // add to the collection manually for the created case
            contentItem.VersionRecord.ContentItemRecord.Versions.Add(contentItem.VersionRecord);

            // version may be specified
            if (options.VersionNumber != 0) {
                contentItem.VersionRecord.Number = options.VersionNumber;
            }

            // draft flag on create is required for explicitly-published content items
            if (options.IsDraft) {
                contentItem.VersionRecord.Published = false;
            }

            _contentItemRepository.Create(contentItem.Record);
            _contentItemVersionRepository.Create(contentItem.VersionRecord);


            // build a context with the initialized instance to create
            var context = new CreateContentContext(contentItem);


            // invoke handlers to add information to persistent stores
            Handlers.Invoke(handler => handler.Creating(context), Logger);
            Handlers.Invoke(handler => handler.Created(context), Logger);

            if (options.IsPublished) {
                var publishContext = new PublishContentContext(contentItem, null);

                // invoke handlers to acquire state, or at least establish lazy loading callbacks
                Handlers.Invoke(handler => handler.Publishing(publishContext), Logger);

                // invoke handlers to acquire state, or at least establish lazy loading callbacks
                Handlers.Invoke(handler => handler.Published(publishContext), Logger);
            }
        }

        public ContentItemMetadata GetItemMetadata(IContent content) {
            var context = new GetContentItemMetadataContext {
                ContentItem = content.ContentItem,
                Metadata = new ContentItemMetadata()
            };

            Handlers.Invoke(handler => handler.GetContentItemMetadata(context), Logger);

            return context.Metadata;
        }

        public IEnumerable<GroupInfo> GetEditorGroupInfos(IContent content) {
            var metadata = GetItemMetadata(content);
            return metadata.EditorGroupInfo
                .GroupBy(groupInfo => groupInfo.Id)
                .Select(grouping => grouping.OrderBy(groupInfo => groupInfo.Position, new FlatPositionComparer()).FirstOrDefault());
        }

        public IEnumerable<GroupInfo> GetDisplayGroupInfos(IContent content) {
            var metadata = GetItemMetadata(content);
            return metadata.DisplayGroupInfo
                .GroupBy(groupInfo => groupInfo.Id)
                .Select(grouping => grouping.OrderBy(groupInfo => groupInfo.Position, new FlatPositionComparer()).FirstOrDefault());
        }

        public GroupInfo GetEditorGroupInfo(IContent content, string groupInfoId) {
            return GetEditorGroupInfos(content).FirstOrDefault(gi => string.Equals(gi.Id, groupInfoId, StringComparison.OrdinalIgnoreCase));
        }

        public GroupInfo GetDisplayGroupInfo(IContent content, string groupInfoId) {
            return GetDisplayGroupInfos(content).FirstOrDefault(gi => string.Equals(gi.Id, groupInfoId, StringComparison.OrdinalIgnoreCase));
        }

        public dynamic BuildDisplay(IContent content, string displayType = "", string groupId = "") {
            return _contentDisplay.Value.BuildDisplay(content, displayType, groupId);
        }

        public dynamic BuildEditor(IContent content, string groupId = "") {
            return _contentDisplay.Value.BuildEditor(content, groupId);
        }

        public dynamic UpdateEditor(IContent content, IUpdateModel updater, string groupId = "") {
            return _contentDisplay.Value.UpdateEditor(content, updater, groupId);
        }

        public IContentQuery<ContentItem> Query() {
            var query = _context.Resolve<IContentQuery>(TypedParameter.From<IContentManager>(this));
            return query.ForPart<ContentItem>();
        }

        // Insert or Update imported data into the content manager.
        // Call content item handlers.
        public void Import(XElement element, ImportContentSession importContentSession) {
            var elementId = element.Attribute("Id");
            if (elementId == null) {
                return;
            }

            var identity = elementId.Value;
            var status = element.Attribute("Status");

            var item = importContentSession.Get(identity);
            if (item == null) {
                item = New(element.Name.LocalName);
                if (status != null && status.Value == "Draft") {
                    Create(item, VersionOptions.Draft);
                }
                else {
                    Create(item);
                }
            }
            else {
                item = Get(item.Id, VersionOptions.DraftRequired);
            }
            importContentSession.Store(identity, item);

            var context = new ImportContentContext(item, element, importContentSession);
            foreach (var contentHandler in Handlers) {
                contentHandler.Importing(context);
            }
            foreach (var contentHandler in Handlers) {
                contentHandler.Imported(context);
            }

            if (status == null || status.Value == Published) {
                Publish(item);
            }
        }

        public XElement Export(ContentItem contentItem) {
            var context = new ExportContentContext(contentItem, new XElement(contentItem.ContentType));

            foreach (var contentHandler in Handlers) {
                contentHandler.Exporting(context);
            }

            foreach (var contentHandler in Handlers) {
                contentHandler.Exported(context);
            }

            context.Data.SetAttributeValue("Id", GetItemMetadata(contentItem).Identity.ToString());
            if (contentItem.IsPublished()) {
                context.Data.SetAttributeValue("Status", Published);
            }
            else {
                context.Data.SetAttributeValue("Status", Draft);
            }

            return context.Data;
        }

        public void Flush() {
            _contentItemRepository.Flush();
        }

        private ContentTypeRecord AcquireContentTypeRecord(string contentType) {
            var contentTypeRecord = _contentTypeRepository.Get(x => x.Name == contentType);
            if (contentTypeRecord == null) {
                //TEMP: this is not safe... ContentItem types could be created concurrently?
                contentTypeRecord = new ContentTypeRecord { Name = contentType };
                _contentTypeRepository.Create(contentTypeRecord);
            }
            return contentTypeRecord;
        }

        public void Index(ContentItem contentItem, IDocumentIndex documentIndex) {
            var indexContentContext = new IndexContentContext(contentItem, documentIndex);

            // dispatch to handlers to retrieve index information
            Handlers.Invoke(handler => handler.Indexing(indexContentContext), Logger);

            Handlers.Invoke(handler => handler.Indexed(indexContentContext), Logger);
        }
    }

    internal class CallSiteCollection : ConcurrentDictionary<string, CallSite<Func<CallSite, object, object>>> {
        private readonly Func<string, CallSite<Func<CallSite, object, object>>> _valueFactory;

        public CallSiteCollection(Func<string, CallSite<Func<CallSite, object, object>>> callSiteFactory) {
            _valueFactory = callSiteFactory;
        }

        public CallSiteCollection(Func<string, CallSiteBinder> callSiteBinderFactory) {
            _valueFactory = key => CallSite<Func<CallSite, object, object>>.Create(callSiteBinderFactory(key));
        }

        public object Invoke(object callee, string key) {
            var callSite = GetOrAdd(key, _valueFactory);
            return callSite.Target(callSite, callee);
        }
    }
}
