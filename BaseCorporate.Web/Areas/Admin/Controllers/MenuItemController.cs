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
    public class MenuItemController : BaseAdminController
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMemoryCache _memoryCache;

        public MenuItemController(IMenuItemService menuItemService, IMemoryCache memoryCache)
        {
            _menuItemService = menuItemService;
            _memoryCache = memoryCache;
        }

        public IActionResult Index(int id = 0)
        {
            ViewBag.GroupId = id;
            return View(_menuItemService.GetList(id));
        }

        public IActionResult Add(int id)
        {
            return View("AddOrUpdate",_menuItemService.AddModel(id));
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _menuItemService.Get(id));
        }


        [HttpPost]
        public IActionResult AddOrUpdate(MenuItemModel model)
        {
            model = _menuItemService.AddOrUpdate(model);
            MenuHelper.ClearCache();
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _menuItemService.Delete(model);
            MenuHelper.ClearCache();
            return Json(result);
        }
    }
}
