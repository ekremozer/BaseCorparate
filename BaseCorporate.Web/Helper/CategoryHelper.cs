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
    public static class CategoryHelper
    {
        public static List<CategoryThreeModel> GetCategoriesThree(int categoryId)
        {
            #region CacheControl
            var cacheName = AppParameter.GetCategoriesThreeCacheName(categoryId);
            var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            var readFromCache = memoryCache.TryGetValue(cacheName, out List<CategoryThreeModel> model);
            if (readFromCache) return model;
            #endregion

            #region GetData
            var categoryService = ServiceLocator.ServiceProvider.GetService<ICategoryService>();
            model = categoryService.GetCategoriesThree(categoryId);
            memoryCache.Set(cacheName, model, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
            return model;
            #endregion
        }
    }
}
