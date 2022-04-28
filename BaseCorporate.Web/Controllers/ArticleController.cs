using System.Linq;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Utility.Model;
using BaseCorporate.Web.Helper;
using BaseCorporate.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BaseCorporate.Web.Controllers
{
    public class ArticleController : BaseWebController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category(int id, int page)
        {
            _articleService.GetArchivePeriods();
            var list = _articleService.GetListByCategory(id, page);
            return View("Index", list);
        }

        public IActionResult Tag(int id, int page)
        {
            var list = _articleService.GetListByTag(id, page);
            return View("Index", list);
        }

        public ActionResult Search(string q, int c, int page)
        {
            var list = _articleService.GetListBySearch(q, c, page);
            return View("Index", list);
        }

        public ActionResult Detail(int id)
        {
            var article = _articleService.GetDetail(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        public IActionResult Archive()
        {
            var archivePeriods = _articleService.GetArchivePeriods();
            return View(archivePeriods);
        }

        public IActionResult ArchiveList(int year, int month, int page)
        {
            var list = _articleService.GeArchiveList(year, month, page);
            return View("Index", list);
        }
    }
}
