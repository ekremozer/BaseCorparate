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
    public class TagController : BaseAdminController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index(int page)
        {
            return View(_tagService.GetList(page));
        }

        public IActionResult Update(int id)
        {
            var category = _tagService.Get(id);
            return View("AddOrUpdate", category);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _tagService.Delete(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(TagModel model)
        {
            model = _tagService.AddOrUpdate(model);
            return Json(model);
        }
    }
}
