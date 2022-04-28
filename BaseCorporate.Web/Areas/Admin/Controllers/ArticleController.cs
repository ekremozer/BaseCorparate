using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class ArticleController : BaseAdminController
    {
        private readonly IArticleService _articleService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMemoryCache _memoryCache;

        public ArticleController(IHostingEnvironment hostingEnvironment, IArticleService articleService, IMemoryCache memoryCache)
        {
            _hostingEnvironment = hostingEnvironment;
            _articleService = articleService;
            _memoryCache = memoryCache;
        }

        public IActionResult Index(int page = 1)
        {
            #region QueryParameters
            var title = (string)null;
            var categoryId = (int?)null;
            var isActive = true;

            var queryStrings = HttpContext.Request.Query;

            var titleKey = queryStrings.Keys.FirstOrDefault(x => x.ToLower().Contains("title"));
            if (!string.IsNullOrEmpty(titleKey))
            {
                title = queryStrings[titleKey];
            }

            var categoryKey = queryStrings.Keys.FirstOrDefault(x => x.ToLower().Contains("category"));
            if (!string.IsNullOrEmpty(categoryKey))
            {
                var intParse = int.TryParse(queryStrings[categoryKey], out var categoryIdParse);
                if (intParse)
                {
                    categoryId = categoryIdParse;
                }
            }

            var isActiveKey = queryStrings.Keys.FirstOrDefault(x => x.ToLower().Contains("active"));
            if (!string.IsNullOrEmpty(isActiveKey))
            {
                var boolParse = bool.TryParse(queryStrings[isActiveKey], out var isActiveKeyParse);
                if (boolParse)
                {
                    isActive = isActiveKeyParse;
                }
            }
            #endregion

            var model = _articleService.GetList(page, title, categoryId, isActive);
            var categories = _articleService.AddModel().Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Value
            });
            var selectListItems = categories as SelectListItem[] ?? categories.ToArray();
            selectListItems = selectListItems.Append(new SelectListItem()).ToArray();
            ViewBag.Categories = selectListItems.OrderByDescending(x => x.Value == null);
            return View(model);
        }

        public IActionResult Add()
        {
            var addModel = _articleService.AddModel();
            return View("AddOrUpdate", addModel);
        }

        public IActionResult Update(int id)
        {
            var addModel = _articleService.UpdateModel(id);
            return View("AddOrUpdate", addModel);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(ArticleModel model, IFormFile image)
        {
            var cacheName = AppParameter.ArticleLastAddedCacheName;
            _memoryCache.Remove(cacheName);


            if (image != null)
            {
                var fileName = model.Title.ToUrl();
                var fileExtension = Path.GetExtension(image.FileName);
                var fileNameWithExtension = $"{fileName}{fileExtension}";
                Uploader.Upload(image, AppParameter.ArticleImagePath, fileNameWithExtension);
                model.Image = fileNameWithExtension;
            }
            else
            {
                model.Image = string.Empty;
            }

            model.Slug = string.IsNullOrEmpty(model.Slug) ? model.Title.ToUrl() : model.Slug.ToUrl();

            model = _articleService.AddOrUpdate(model);
            return RedirectToAction("Update", new { id = model.Id });
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _articleService.Delete(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            var fileName = ExtensionMethods.GetUniqueFileName(upload.FileName);
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, AppParameter.ArticleImagePath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            var url = $"/{AppParameter.ArticleImagePath}/{fileName}";

            return Json(new { uploaded = true, url });
        }
    }
}
