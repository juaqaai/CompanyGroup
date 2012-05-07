using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.UI;
using Szmyd.Orchard.Modules.Menu.Models;

namespace Szmyd.Orchard.Modules.Menu.Services
{
    public class MenuService : IAdvancedMenuService
    {
        private readonly Work<IContentManager> _contentManager;
        private readonly ISignals _signals;
        private readonly ICacheManager _cacheManager;
        private const string CachePrefix = "Szmyd.Menu.Menus.";
        private const string MenuSignal = "Szmyd.Menu.Menus";

        public MenuService(Work<IContentManager> contentManager, ISignals signals, ICacheManager cacheManager)
        {
            _contentManager = contentManager;
            _signals = signals;
            _cacheManager = cacheManager;
        }

        public IEnumerable<AdvancedMenuPart> GetMenus()
        {
            //return _cacheManager.Get(CachePrefix,
            //    (ctx) =>
            //    {
            //        MonitorSignal(ctx);
                    return _contentManager.Value.Query<AdvancedMenuPart, AdvancedMenuPartRecord>().List();
            //    });
        }

        public AdvancedMenuPart GetMenu(string menuName)
        {
            //return _cacheManager.Get(CachePrefix + menuName,
            //    (ctx) =>
            //    {
            //        MonitorSignal(ctx);
                    return _contentManager.Value
                        .Query<AdvancedMenuPart, AdvancedMenuPartRecord>()
                        .Where(c => c.Name == menuName)
                        .List().FirstOrDefault();
            //    });
        }

        public IEnumerable<AdvancedMenuItemPart> GetMenuItems(string menuName)
        {
            //return _cacheManager.Get(CachePrefix + menuName,
            //    (ctx) =>
            //    {
            //        MonitorSignal(ctx);
                    return _contentManager.Value
                        .Query<AdvancedMenuItemPart, AdvancedMenuItemPartRecord>()
                        .Where(i => i.MenuName == menuName)
                        .List().OrderBy(i => i.Position, new FlatPositionComparer());
            //    });
        }

        public IEnumerable<AdvancedMenuItemPart> GetMenuItemsForContent(IContent contentItem)
        {
            //return _cacheManager.Get(CachePrefix + "Content." + contentItem.Id,
            //    (ctx) =>
            //    {
            //        MonitorSignal(ctx);
                    return _contentManager.Value
                        .Query<AdvancedMenuItemPart, AdvancedMenuItemPartRecord>()
                        .Where(i => i.RelatedContentId == contentItem.Id)
                        .List();
            //    });
        }

        public AdvancedMenuItemPart GetMenuItem(int itemId)
        {
            return _contentManager.Value.Get(itemId).As<AdvancedMenuItemPart>();
        }

        public void DeleteMenu(string menuName)
        {
            var item = GetMenu(menuName);
            if (item != null && item.ContentItem != null)
            {
                TriggerSignal();
                // Remove menu items
                foreach (var i in GetMenuItems(menuName))
                {
                    _contentManager.Value.Remove(i.ContentItem);
                }

                // Remove the menu
                _contentManager.Value.Remove(item.ContentItem);
                _contentManager.Value.Flush();
            }
        }

        public void DeleteMenuItem(int itemId)
        {
            var item = GetMenuItem(itemId);
            if (item != null && item.ContentItem != null) {
                TriggerSignal();

                _contentManager.Value.Remove(item.ContentItem);
                _contentManager.Value.Flush();
            }
        }

        private void MonitorSignal(AcquireContext<string> ctx)
        {
            ctx.Monitor(_signals.When(MenuSignal));
        }

        public void TriggerSignal()
        {
            _signals.Trigger(MenuSignal);
        }
    }
}