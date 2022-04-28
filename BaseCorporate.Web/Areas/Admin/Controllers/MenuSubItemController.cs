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
    public class MenuSubItemController : BaseAdminController
    {
        private readonly IMenuSubItemService _menuSubItemService;
        private readonly IMemoryCache _memoryCache;

        public MenuSubItemController(IMenuSubItemService menuSubItemService, IMemoryCache memoryCache)
        {
            _menuSubItemService = menuSubItemService;
            _memoryCache = memoryCache;
        }

        public IActionResult Index(int id = 0)
        {
            ViewBag.ItemId = id;
            return View(_menuSubItemService.GetList(id));
        }

        public IActionResult Add(int id)
        {
            return View("AddOrUpdate", _menuSubItemService.AddModel(id));
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _menuSubItemService.Get(id));
        }


        [HttpPost]
        public IActionResult AddOrUpdate(MenuSubItemModel model)
        {
            model = _menuSubItemService.AddOrUpdate(model);
            MenuHelper.ClearCache();
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _menuSubItemService.Delete(model);
            MenuHelper.ClearCache();
            return Json(result);
        }
    }
}
