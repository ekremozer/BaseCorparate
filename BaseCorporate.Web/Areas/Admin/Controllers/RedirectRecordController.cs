using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Areas.Admin.Infrastructure;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class RedirectRecordController : BaseAdminController
    {
        private readonly IRedirectRecordService _redirectRecordService;

        public RedirectRecordController(IRedirectRecordService redirectRecordService)
        {
            _redirectRecordService = redirectRecordService;
        }

        public IActionResult Index()
        {
            return View(_redirectRecordService.GetList());
        }

        public IActionResult Add(string oldUrl = "")
        {
            return View("AddOrUpdate", new RedirectRecordModel { OldUrl = oldUrl });
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _redirectRecordService.Get(id));
        }

        [HttpPost]
        public IActionResult AddOrUpdate(RedirectRecordModel model)
        {
            model = _redirectRecordService.AddOrUpdate(model);
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _redirectRecordService.Delete(model);
            return Json(result);
        }
    }
}
