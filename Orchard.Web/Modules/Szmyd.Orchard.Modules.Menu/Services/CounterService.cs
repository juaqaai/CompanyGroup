#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Mvc;
using Szmyd.Orchard.Modules.Menu.Entities;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;

#endregion

namespace Szmyd.Orchard.Modules.Menu.Services {
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class CounterService : ICounterService {
        private const string SessionKey = "Szmyd.Menu.Counters";
        private readonly IHttpContextAccessor _context;
        private readonly ConcurrentDictionary<string, object> _locks;
        private readonly IContentManager _manager;
        private readonly Lazy<IRepository<CounterRecord>> _repo;

        public CounterService(
            IContentManager manager,
            IHttpContextAccessor context,
            Lazy<IRepository<CounterRecord>> repo) {
            _locks = new ConcurrentDictionary<string, object>();
            _manager = manager;
            _context = context;
            _repo = repo;

            if (_context == null || _context.Current() == null || _context.Current().Session == null) {
                return;
            }
            // ReSharper disable PossibleNullReferenceException
            var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
            // ReSharper restore PossibleNullReferenceException
            if (dict == null) {
                // ReSharper disable PossibleNullReferenceException
                _context.Current().Session[SessionKey] = new ConcurrentDictionary<string, SerializableCounter>();
            }
            // ReSharper restore PossibleNullReferenceException
        }

        #region Implementation of ICounterService

        public int Increment(int itemId, int contextItemId, CounterType type = CounterType.Visits, CounterStoreType store = CounterStoreType.Database) {
            string key = String.Format("{0}.{1}.{2}", itemId, contextItemId, type);
            object counterLock = _locks.GetOrAdd(key, new object());
            lock (counterLock) {
                int counts = 0;
                switch (store) {
                    case CounterStoreType.Database:
                        CounterRecord counter = _repo.Value.Get(r => r.ForItemId == itemId &&
                                                                     r.InContextOfItemId == contextItemId &&
                                                                     r.Type == type);

                        if (counter == null) {
                            counter = new CounterRecord {
                                ForItemId = itemId,
                                InContextOfItemId = contextItemId,
                                Type = type,
                                Count = 0,
                                LastModified = DateTime.Now
                            };
                            _repo.Value.Create(counter);
                            _repo.Value.Flush();
                        }

                        counts = ++counter.Count;
                        counter.LastModified = DateTime.Now;
                        _repo.Value.Flush();
                        break;
                    case CounterStoreType.Session:
                        if (_context.Current() != null && _context.Current().Session != null) {
                            // ReSharper disable PossibleNullReferenceException
                            var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
                            // ReSharper restore PossibleNullReferenceException
                            SerializableCounter sessionCounter = dict.GetOrAdd(key, new SerializableCounter {
                                ForItemId = itemId,
                                InContextOfItemId = contextItemId,
                                Type = type,
                                Count = 0
                            });
                            counts = ++sessionCounter.Count;
                        }
                        break;
                    default:
                        break;
                }
                return counts;
            }
        }

        public int Decrement(int itemId, int contextItemId, CounterType type = CounterType.Visits, CounterStoreType store = CounterStoreType.Database) {
            string key = String.Format("{0}.{1}.{2}", itemId, contextItemId, type);
            object counterLock = _locks.GetOrAdd(key, new object());
            lock (counterLock) {
                int counts = 0;
                switch (store) {
                    case CounterStoreType.Database:

                        CounterRecord counter = _repo.Value.Get(r => r.ForItemId == itemId &&
                                                                     r.InContextOfItemId == contextItemId &&
                                                                     r.Type == type);

                        if (counter == null) {
                            counter = new CounterRecord {
                                ForItemId = itemId,
                                InContextOfItemId = contextItemId,
                                Type = type,
                                Count = 0,
                                LastModified = DateTime.Now
                            };
                            _repo.Value.Create(counter);
                        }

                        counts = --counter.Count;
                        counter.LastModified = DateTime.Now;
                        _repo.Value.Flush();
                        break;
                    case CounterStoreType.Session:
                        if (_context.Current() != null && _context.Current().Session != null) {
                            // ReSharper disable PossibleNullReferenceException
                            var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
                            // ReSharper restore PossibleNullReferenceException
                            SerializableCounter sessionCounter = dict.GetOrAdd(key, new SerializableCounter {
                                ForItemId = itemId,
                                InContextOfItemId = contextItemId,
                                Type = type,
                                Count = 0
                            });
                            counts = --sessionCounter.Count;
                        }
                        break;
                    default:
                        break;
                }
                return counts;
            }
        }

        /// <summary>
        ///     Gets items by the given predicate.
        /// </summary>
        /// <param name = "counterPredicate">Predicate</param>
        /// <returns>List of content items</returns>
        public IEnumerable<IContent> GetItems(Func<dynamic, bool> counterPredicate, Func<dynamic, dynamic> orderBy, CounterStoreType store) {
            IEnumerable<int> ids = Enumerable.Empty<int>();
            switch (store) {
                case CounterStoreType.Database:
                    ids = _repo.Value.Fetch(r => counterPredicate(r) && r.Count > 0)
                        .OrderBy(orderBy)
                        .Select(r => (int)r.ForItemId);
                    break;
                case CounterStoreType.Session:
                    if (_context.Current() != null) {
                        ids = (_context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>)
                            .Values
                            .Where(r => counterPredicate(r) && r.Count > 0)
                            .OrderBy(orderBy).Select(r => (int)r.ForItemId);
                    }
                    ;
                    break;
            }

            return _manager.Query().List().Where(i => ids.Contains(i.Id));
        }

        public int GetCounter(int itemId, int contextItemId, CounterType type, CounterStoreType store) {
            int counts = 0;
            switch (store) {
                case CounterStoreType.Database:

                    CounterRecord counter = _repo.Value.Get(r => r.ForItemId == itemId &&
                                                                 r.InContextOfItemId == contextItemId &&
                                                                 r.Type == type);

                    counts = counter == null ? 0 : counter.Count;
                    break;
                case CounterStoreType.Session:
                    if (_context.Current() != null) {
                        string key = String.Format("{0}.{1}.{2}", itemId, contextItemId, type);
                        // ReSharper disable PossibleNullReferenceException
                        var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
                        // ReSharper restore PossibleNullReferenceException
                        SerializableCounter c;
                        counts = dict.TryGetValue(key, out c) ? c.Count : 0;
                    }
                    break;
                default:
                    break;
            }
            return counts;
        }

        public void RemoveCounter(int itemId, int contextItemId, CounterType type, CounterStoreType store) {
            string key = String.Format("{0}.{1}.{2}", itemId, contextItemId, type);
            object counterLock = _locks.GetOrAdd(key, new object());
            lock (counterLock) {
                switch (store) {
                    case CounterStoreType.Database:

                        CounterRecord counter = _repo.Value.Get(r => r.ForItemId == itemId &&
                                                                     r.InContextOfItemId == contextItemId &&
                                                                     r.Type == type);

                        if (counter != null) {
                            _repo.Value.Delete(counter);
                            _repo.Value.Flush();
                        }


                        break;
                    case CounterStoreType.Session:
                        if (_context.Current() != null) {
                            // ReSharper disable PossibleNullReferenceException
                            var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
                            // ReSharper restore PossibleNullReferenceException
                            SerializableCounter c;
                            if (dict != null) {
                                dict.TryRemove(key, out c);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void RemoveAllCounters(int itemId) {
            if (_context.Current() != null) {
                // ReSharper disable PossibleNullReferenceException
                var dict = _context.Current().Session[SessionKey] as ConcurrentDictionary<string, SerializableCounter>;
                // ReSharper restore PossibleNullReferenceException
                if (dict != null) {
                    List<string> keys = dict.Where(kv => kv.Value.ForItemId == itemId)
                        .Select(kv => kv.Key)
                        .ToList();
                    foreach (string key in keys) {
                        SerializableCounter c;
                        dict.TryRemove(key, out c);
                    }
                }
            }
            List<CounterRecord> counters = _repo.Value.Fetch(r => r.ForItemId == itemId).ToList();
            foreach (CounterRecord c in counters) {
                _repo.Value.Delete(c);
            }
            _repo.Value.Flush();
        }

        #endregion
    }
}