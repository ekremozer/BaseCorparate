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

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categoryList = _categoryService.GetList();
            return View(categoryList);
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate", _categoryService.AddModel());
        }

        public IActionResult Update(int id)
        {
            var category = _categoryService.Get(id);
            return View("AddOrUpdate", category);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(CategoryModel model)
        {
            model = _categoryService.AddOrUpdate(model);
            return Json(model);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _categoryService.Delete(model);
            return Json(result);
        }
    }
}
