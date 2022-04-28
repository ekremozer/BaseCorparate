using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Areas.Admin.Infrastructure;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class UrlRecordController : BaseAdminController
    {
        private readonly IUrlRecordService _urlRecordService;

        public UrlRecordController(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }

        public IActionResult Index()
        {
            return View(_urlRecordService.GetList());
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _urlRecordService.Get(id));
        }

        [HttpPost]
        public IActionResult Update(UrlRecordModel model)
        {
            model.UpdateEntity = true;
            return View("AddOrUpdate", _urlRecordService.AddOrUpdate(model));
        }
    }
}
