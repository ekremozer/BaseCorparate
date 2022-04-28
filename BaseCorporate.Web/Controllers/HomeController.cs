using System.Collections.Generic;
using System.Linq;
using BaseCorporate.Web.Infrastructure;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCorporate.Web.Controllers
{
    public class HomeController : BaseWebController
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IActionResult Index(int page = 1)
        {
            var list = _articleService.GetListByHomePage(page);
            //var context = new EfContext();

            //var articles = context.Articles.ToList();
            //foreach (var article in articles)
            //{
            //    article.Slug = article.Slug.ToUrl();
            //    article.CategoryId = 1;

            //    var urlRecord = new UrlRecord();
            //    urlRecord.ArticleId = article.Id;
            //    urlRecord.Slug = article.Slug;
            //    context.UrlRecords.Add(urlRecord);
            //    context.SaveChanges();
            //    article.UrlRecordId = urlRecord.Id;
            //    context.SaveChanges();
            //}
            return View(list);
        }
    }
}
