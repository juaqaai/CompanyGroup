﻿using System;
using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Containers.Models;
using Orchard.Core.Routable.Models;
using Orchard.Core.Title.Models;

namespace Orchard.Core.Containers.Extensions
{
    public static class ContentQueryExtensions {
        public static IContentQuery<T> OrderBy<T>(this IContentQuery<T> query, string partAndProperty, bool descendingOrder) where T : IContent {
            //todo: (heskew) order by custom part properties
            switch (partAndProperty) {
                case "TitlePart.Title":
                    query = descendingOrder
                                ? query.OrderByDescending<TitlePartRecord, string>(record => record.Title)
                                : query.OrderBy<TitlePartRecord, string>(record => record.Title);
                    break;
                case "RoutePart.Title":
                    query = descendingOrder
                                ? query.OrderByDescending<RoutePartRecord, string>(record => record.Title)
                                : query.OrderBy<RoutePartRecord, string>(record => record.Title);
                    break;
                case "RoutePart.Slug":
                    query = descendingOrder
                                ? query.OrderByDescending<RoutePartRecord, string>(record => record.Slug)
                                : query.OrderBy<RoutePartRecord, string>(record => record.Slug);
                    break;
                case "CustomPropertiesPart.CustomOne":
                    query = descendingOrder
                                ? query.OrderByDescending<CustomPropertiesPartRecord, string>(record => record.CustomOne)
                                : query.OrderBy<CustomPropertiesPartRecord, string>(record => record.CustomOne);
                    break;
                case "CustomPropertiesPart.CustomTwo":
                    query = descendingOrder
                                ? query.OrderByDescending<CustomPropertiesPartRecord, string>(record => record.CustomTwo)
                                : query.OrderBy<CustomPropertiesPartRecord, string>(record => record.CustomTwo);
                    break;
                case "CustomPropertiesPart.CustomThree":
                    query = descendingOrder
                                ? query.OrderByDescending<CustomPropertiesPartRecord, string>(record => record.CustomThree)
                                : query.OrderBy<CustomPropertiesPartRecord, string>(record => record.CustomThree);
                    break;
                case "CommonPart.CreatedUtc":
                    query = descendingOrder
                                ? query.OrderByDescending<CommonPartRecord, DateTime?>(record => record.CreatedUtc)
                                : query.OrderBy<CommonPartRecord, DateTime?>(record => record.CreatedUtc);
                    break;
                default: // "CommonPart.PublishedUtc"
                    query = descendingOrder
                                ? query.OrderByDescending<CommonPartRecord, DateTime?>(record => record.PublishedUtc)
                                : query.OrderBy<CommonPartRecord, DateTime?>(record => record.PublishedUtc);
                    break;
            }

            return query;
        }

        public static IContentQuery<ContentItem> Where(this IContentQuery<ContentItem> query, string partAndProperty, string comparisonOperator, string comparisonValue) {
            var filterKey = string.Format("{0}|{1}", partAndProperty, comparisonOperator);
            if (!_filters.ContainsKey(filterKey))
                return query;

            return _filters[filterKey](query, comparisonValue);
        }

        // convoluted: yes; quick and works for now: yes; technical debt: not much
        private static readonly Dictionary<string, Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>> _filters = new Dictionary<string, Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>> {
            {"TitlePart.Title|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<TitlePartRecord>(r => true /* CompareTo is not implemented - r.Title.CompareTo(s) == -1*/))},
            {"TitlePart.Title|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<TitlePartRecord>(r => true /* CompareTo is not implemented - r.Title.CompareTo(s) == 1*/))},
            {"TitlePart.Title|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<TitlePartRecord>(r => r.Title.Equals(s, StringComparison.OrdinalIgnoreCase)))},
            {"TitlePart.Title|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<TitlePartRecord>(r => r.Title.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
            {"RoutePart.Title|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => true /* CompareTo is not implemented - r.Title.CompareTo(s) == -1*/))},
            {"RoutePart.Title|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => true /* CompareTo is not implemented - r.Title.CompareTo(s) == 1*/))},
            {"RoutePart.Title|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => r.Title.Equals(s, StringComparison.OrdinalIgnoreCase)))},
            {"RoutePart.Title|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => r.Title.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
            {"RoutePart.Slug|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => true /* CompareTo is not implemented - r.Slug.CompareTo(s) == -1*/))},
            {"RoutePart.Slug|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => true /* CompareTo is not implemented - r.Slug.CompareTo(s) == 1*/))},
            {"RoutePart.Slug|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => r.Slug.Equals(s.Trim(), StringComparison.OrdinalIgnoreCase)))},
            {"RoutePart.Slug|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<RoutePartRecord>(r => r.Slug.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
            {"CommonPart.PublishedUtc|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CommonPartRecord>(r => r.PublishedUtc < DateTime.Parse(s)))},
            {"CommonPart.PublishedUtc|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CommonPartRecord>(r => r.PublishedUtc > DateTime.Parse(s)))},
            {"CommonPart.PublishedUtc|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CommonPartRecord>(r => r.PublishedUtc == DateTime.Parse(s)))}, // todo: (heskew) not practical as is. needs some sense of precision....
            {"CommonPart.PublishedUtc|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CommonPartRecord>(r => true /* can't modified PublishedUtc for partial comparisons */))},
            // todo: (hesekw) this could benefit from a better filter implementation as this is currently very limited in functionality and I have no idea how the custom parts will be used by folks
            {"CustomPropertiesPart.CustomOne|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomOne.CompareTo(s) == -1*/))},
            {"CustomPropertiesPart.CustomOne|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomOne.CompareTo(s) == 1*/))},
            {"CustomPropertiesPart.CustomOne|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomOne.Equals(s, StringComparison.OrdinalIgnoreCase)))},
            {"CustomPropertiesPart.CustomOne|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomOne.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
            {"CustomPropertiesPart.CustomTwo|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomTwo.CompareTo(s) == -1*/))},
            {"CustomPropertiesPart.CustomTwo|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomTwo.CompareTo(s) == 1*/))},
            {"CustomPropertiesPart.CustomTwo|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomTwo.Equals(s, StringComparison.OrdinalIgnoreCase)))},
            {"CustomPropertiesPart.CustomTwo|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomTwo.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
            {"CustomPropertiesPart.CustomThree|<", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomThree.CompareTo(s) == -1*/))},
            {"CustomPropertiesPart.CustomThree|>", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => true /* CompareTo is not implemented - r.CustomThree.CompareTo(s) == 1*/))},
            {"CustomPropertiesPart.CustomThree|=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomThree.Equals(s, StringComparison.OrdinalIgnoreCase)))},
            {"CustomPropertiesPart.CustomThree|^=", new Func<IContentQuery<ContentItem>, string, IContentQuery<ContentItem>>((q, s) => q.Where<CustomPropertiesPartRecord>(r => r.CustomThree.StartsWith(s, StringComparison.OrdinalIgnoreCase)))},
        };
    }
}