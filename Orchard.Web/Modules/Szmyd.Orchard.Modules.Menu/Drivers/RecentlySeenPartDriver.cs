using System.Linq;
using System.Text.RegularExpressions;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Routable.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Notify;
using Szmyd.Orchard.Modules.Menu.Entities;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Services;

namespace Szmyd.Orchard.Modules.Menu.Drivers
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPartDriver : ContentPartDriver<RecentlySeenPart>
    {
        private readonly IAuthenticationService _auth;
        private readonly IOrchardServices _services;
        private readonly ICounterService _svc;
        private readonly INotifier _notifier;
        private const string TemplateName = "Parts/Menu.RecentlySeen";
        public Localizer T { get; set; }

        public RecentlySeenPartDriver(IAuthenticationService auth, IOrchardServices services, ICounterService svc, INotifier notifier)
        {
            _auth = auth;
            _services = services;
            _svc = svc;
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(RecentlySeenPart part, string displayType, dynamic shapeHelper)
        {
            //var settings = part.Settings.GetModel<RecentlySeenPartTypePartSettings>();
            var items = Enumerable.Empty<RecentlySeenItem>();

            // Setting Regexes
            var negRegex = part.NegativeFilterRegex ?? "";
            var hasNegRegex = !string.IsNullOrWhiteSpace(negRegex);
            var posRegex = part.PositiveFilterRegex ?? "";
            var hasPosRegex = !string.IsNullOrWhiteSpace(negRegex);

            // Setting data to fetch corresponding items
            var counterType = part.VisitCounter;
            var site = _services.WorkContext.CurrentSite;
            var user = _auth.GetAuthenticatedUser();

            // Correcting the displayed data if user is not authenticated
            if (user == null && counterType == VisitCounterType.PerUser)
                counterType = VisitCounterType.PerSession;

            switch (counterType)
            {
                case VisitCounterType.PerSession:
                    // Session info is stored per session, 
                    // so passing the site id doesn't have much sense here
                    items = _svc.GetItems(
                            r => r.InContextOfItemId == site.Id &&
                                 r.Type == (int)CounterType.Visits,
                            r => r.Count,
                            CounterStoreType.Session)
                        .Where(rp => rp.Is<RoutePart>())
                        .Select(rp => new RecentlySeenItem
                        {
                            Url = "/" + rp.As<RoutePart>().Path,
                            Text = new LocalizedString(rp.As<RoutePart>().Title),
                            Visits = _svc.GetCounter(
                                rp.ContentItem.Id,
                                site.Id,
                                CounterType.Visits,
                                CounterStoreType.Session)
                        });
                    break;
                case VisitCounterType.PerUser:
                    if (user != null)
                    {
                        items = _svc.GetItems(
                                r => r.InContextOfItemId == user.Id &&
                                     r.Type == (int)CounterType.Visits,
                                r => r.Count,
                                CounterStoreType.Database)
                            .Where(rp => rp.Is<RoutePart>())
                            .Select(rp => new RecentlySeenItem
                            {
                                Url = "/" + rp.As<RoutePart>().Path,
                                Text = new LocalizedString(rp.As<RoutePart>().Title),
                                Visits = _svc.GetCounter(
                                    rp.ContentItem.Id,
                                    user.Id,
                                    CounterType.Visits,
                                    CounterStoreType.Database)
                            });
                    }
                    break;
                case VisitCounterType.PerSite:
                    items = _svc.GetItems(
                                r => r.InContextOfItemId == site.Id &&
                                     r.Type == (int)CounterType.Visits,
                                r => r.Count,
                                CounterStoreType.Database)
                            .Where(rp => rp.Is<RoutePart>())
                            .Select(rp => new RecentlySeenItem
                            {
                                Url = "/" + rp.As<RoutePart>().Path,
                                Text = new LocalizedString(rp.As<RoutePart>().Title),
                                Visits = _svc.GetCounter(
                                    rp.ContentItem.Id,
                                    site.Id,
                                    CounterType.Visits,
                                    CounterStoreType.Database)
                            });
                    break;
                default:
                    break;
            }

            items = items.Where(i =>
                        !(hasNegRegex && Regex.IsMatch(i.Url, negRegex)) &&
                        !(hasPosRegex && !Regex.IsMatch(i.Url, posRegex)))
                    .OrderByDescending(e => e.Visits).Take(part.ItemCount);

            return ContentShape("Parts_Menu_RecentlySeen",
                () => shapeHelper.Parts_Menu_RecentlySeen(RecentItems: items, ShowCounts: part.ShowCounts));
        }

        protected override DriverResult Editor(RecentlySeenPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Menu_RecentlySeen",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(RecentlySeenPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _notifier.Information(T("Recently Seen Part edited successfully"));
            }
            else
            {
                _notifier.Error(T("Error during Recently Seen Part update!"));
            }
            return Editor(part, shapeHelper);
        }
    }
}