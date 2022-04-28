using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Helper;
using BaseCorporate.Web.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace BaseCorporate.Web.Controllers
{
    public class SharedController : BaseWebController
    {
        private readonly IUrlRecordService _urlRecordService;
        private readonly IMemoryCache _memoryCache;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IArticleService _articleService;
        public SharedController(IUrlRecordService urlRecordService, IMemoryCache memoryCache, IHostEnvironment hostEnvironment, IArticleService articleService)
        {
            _urlRecordService = urlRecordService;
            _memoryCache = memoryCache;
            _hostEnvironment = hostEnvironment;
            _articleService = articleService;
        }

        public IActionResult PageNotFoundLog()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult SiteMap()
        {
            #region CacheControl
            var cacheName = AppParameter.SiteMapCacheName;
            var baseUrl = $"{AppParameter.MetaSetting.SiteAddress}/";
            var memoryCache = ServiceLocator.ServiceProvider.GetService<IMemoryCache>();
            var readFromCache = memoryCache.TryGetValue(cacheName, out string xmlString);
            if (readFromCache) return Content(xmlString, "text/xml");
            #endregion

            var slugs = _urlRecordService.GetUrlForSiteMap();
            var siteMapBuilder = new SitemapBuilder();

            siteMapBuilder.AddUrl(baseUrl, DateTime.UtcNow, ChangeFrequency.Weekly, "1.0");

            foreach (var post in slugs)
            {
                var priority = string.IsNullOrEmpty(post.Priority) ? "0.9" : post.Priority;
                siteMapBuilder.AddUrl($"{baseUrl}{post.Url}", post.Modified, null, priority);
            }

            xmlString = siteMapBuilder.ToString();
            memoryCache.Set(cacheName, xmlString, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
            return Content(xmlString, "text/xml");
        }

        public ActionResult Rss()
        {
            var articles = _articleService.GetListForRss();
            using var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("rss");
                xmlWriter.WriteAttributeString("version", "2.0");
                xmlWriter.WriteStartElement("channel");
                xmlWriter.WriteElementString("title", AppParameter.MetaSetting.Title);
                xmlWriter.WriteElementString("link", AppParameter.MetaSetting.SiteAddress);
                xmlWriter.WriteElementString("description", AppParameter.MetaSetting.MetaDescription);
                foreach (var article in articles)
                {
                    xmlWriter.WriteStartElement("item");
                    xmlWriter.WriteElementString("title", article.Title);
                    xmlWriter.WriteElementString("description", article.MetaDescription);
                    xmlWriter.WriteElementString("link", $"{AppParameter.MetaSetting.SiteAddress}/{article.Slug}");
                    xmlWriter.WriteElementString("pubDate", $"{article.CreatedAt.ToString("ddd, dd MMM yyyy HH:mm:ss", new CultureInfo("tr-TR"))} GMT");
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            var xmlString = stringWriter.ToString();
            return Content(xmlString, "text/xml");
        }

        public IActionResult RobotsTxt()
        {
            var txt = System.IO.File.ReadAllText(AppParameter.RobotsTxtPath);
            txt = txt.Replace("{SiteAddress}", AppParameter.MetaSetting.SiteAddress);
            return Content(txt);
        }

        public IActionResult CreatedDb()
        {
            TestData.Builder();
            return Redirect("/");
        }

        public IActionResult FaviconIco()
        {
            var favicon = System.IO.File.OpenRead($"{AppParameter.ContentRootPath}/favicon.ico");
            if (favicon == null)
            {
                return NotFound();
            }
            return File(favicon, "image/jpeg");
        }
    }
}
