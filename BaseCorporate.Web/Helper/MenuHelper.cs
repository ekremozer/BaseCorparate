using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace BaseCorporate.Web.Helper
{
    public static class MenuHelper
    {
        public static MenuGroupUIModel GetMenuGroup(string key)
        {
            #region CacheControl
            var cacheName = AppParameter.GetMenuGroupCacheName(key);
            var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            var readFromCache = memoryCache.TryGetValue(cacheName, out MenuGroupUIModel model);
            if (readFromCache) return model;
            #endregion

            #region GetData
            var menuGroupService = ServiceLocator.ServiceProvider.GetService<IMenuGroupService>();
            model = menuGroupService.Get(key);
            memoryCache.Set(cacheName, model, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
            return model;
            #endregion
        }

        public static void ClearCache()
        {
            var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            var menuGroupService = ServiceLocator.ServiceProvider.GetService<IMenuGroupService>();
            var menuGroups = menuGroupService.GetList();
            foreach (var cacheName in menuGroups.Select(menuGroup => AppParameter.GetMenuGroupCacheName(menuGroup.Key)))
            {
                memoryCache.Remove(cacheName);
            }
        }
    }
}
