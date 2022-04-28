using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Web.Areas.Admin.Infrastructure;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class PageNotFoundLogController : BaseAdminController
    {
        private readonly IPageNotFoundLogService _pageNotFoundLogService;

        public PageNotFoundLogController(IPageNotFoundLogService pageNotFoundLogService)
        {
            _pageNotFoundLogService = pageNotFoundLogService;
        }

        public IActionResult Index(int page=1)
        {
            return View(_pageNotFoundLogService.GetList(page));
        }

        public IActionResult DeleteAll()
        {
            var result = _pageNotFoundLogService.DeleteAll();
            return Json(result);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _pageNotFoundLogService.Delete(model);
            return Json(result);
        }
    }
}
