using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Helper;
using BaseCorporate.Utility.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class MenuGroupController : BaseAdminController
    {
        private readonly IMenuGroupService _menuGroupService;
        private readonly IMemoryCache _memoryCache;

        public MenuGroupController(IMenuGroupService menuGroupService, IMemoryCache memoryCache)
        {
            _menuGroupService = menuGroupService;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View(_menuGroupService.GetList());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate", new MenuGroupModel());
        }

        public IActionResult Update(int id)
        {
            var category = _menuGroupService.Get(id);
            return View("AddOrUpdate", category);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(MenuGroupModel model)
        {
            model = _menuGroupService.AddOrUpdate(model);
            MenuHelper.ClearCache();
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _menuGroupService.Delete(model);
            MenuHelper.ClearCache();
            return Json(result);
        }
    }
}
