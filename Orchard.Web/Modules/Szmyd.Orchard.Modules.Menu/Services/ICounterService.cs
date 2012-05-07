using System;
using System.Collections.Generic;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Services
{

    public interface ICounterService : IDependency {
        /// <summary>
        /// Increments the given counter type for the item in the context of other item.
        /// </summary>
        /// <param name="itemId">Item the counter counts for.</param>
        /// <param name="contextItemId">Item, in context of which the counter runs.</param>
        /// <param name="type">Type of the counter</param>
        /// <param name="store">Storage type</param>
        /// <returns>Current count</returns>
        int Increment(int itemId, int contextItemId, CounterType type, CounterStoreType store);

        /// <summary>
        /// Decrements the given counter type for the item in the context of other item.
        /// </summary>
        /// <param name="itemId">Item the counter counts for.</param>
        /// <param name="contextItemId">Item, in context of which the counter runs.</param>
        /// <param name="type">Type of the counter</param>
        /// <param name="store">Storage type</param>
        /// <returns>Current count</returns>
        int Decrement(int itemId, int contextItemId, CounterType type, CounterStoreType store);

        /// <summary>
        /// Gets items by the given predicate.
        /// </summary>
        /// <param name="counterPredicate">Predicate</param>
        /// <returns>List of content items</returns>
        IEnumerable<IContent> GetItems(Func<dynamic, bool> counterPredicate, Func<dynamic, dynamic> orderBy, CounterStoreType store);

        int GetCounter(int itemId, int contextItemId, CounterType type, CounterStoreType store);

        void RemoveCounter(int itemId, int contextItemId, CounterType type, CounterStoreType store);

        void RemoveAllCounters(int itemId);
    }
}