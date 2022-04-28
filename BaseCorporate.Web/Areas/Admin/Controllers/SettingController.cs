using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Infrastructure;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Utility.Model;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class SettingController : BaseAdminController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            return View(_settingService.GetList());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate", new SettingModel());
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _settingService.Get(id));
        }

        [HttpPost]
        public IActionResult AddOrUpdate(SettingModel model)
        {
            model = _settingService.AddOrUpdate(model);
            switch (model.GroupName)
            {
                case "mail": AppParameterBinding.MailSetting(_settingService); break;
                case "meta": AppParameterBinding.MetaSetting(_settingService); break;
                default: AppParameterBinding.AllSetting(_settingService); break;
            }
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _settingService.Delete(model);
            return Json(result);
        }
    }
}
