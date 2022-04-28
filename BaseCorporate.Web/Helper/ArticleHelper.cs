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
    public static class ArticleHelper
    {
        public static List<ArticleListItem> GetLastAdded(int count = 3)
        {
            #region CacheControl
            var cacheName = AppParameter.ArticleLastAddedCacheName;
            var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            var readFromCache = memoryCache.TryGetValue(cacheName, out List<ArticleListItem> model);
            if (readFromCache) return model;
            #endregion

            #region GetData
            var articleService = ServiceLocator.ServiceProvider.GetService<IArticleService>();
            model = articleService.GetLastAdded(count);
            memoryCache.Set(cacheName, model, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
            return model;
            #endregion
        }

        public static List<ArticleListItem> GetByCategoryForDetail(int? categoryId, int articleId, int count = 3)
        {
            var articleService = ServiceLocator.ServiceProvider.GetService<IArticleService>();
            var model = articleService.GetByCategoryForDetail(categoryId, articleId, count);
            return model;

            //#region CacheControl
            //var cacheName = AppParameter.GetByCategoryForDetail(categoryId);
            //var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            //var readFromCache = memoryCache.TryGetValue(cacheName, out List<ArticleListItem> model);
            //if (readFromCache) return model;
            //#endregion

            //#region GetData
            //var articleService = ServiceLocator.ServiceProvider.GetService<IArticleService>();
            //model = articleService.GetByCategoryForDetail(categoryId, count);
            //memoryCache.Set(cacheName, model, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
            //return model;
            //#endregion
        }
    }
}
