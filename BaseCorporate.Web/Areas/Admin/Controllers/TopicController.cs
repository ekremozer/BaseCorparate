using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class TopicController : BaseAdminController
    {
        private readonly ITopicService _topicService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TopicController(ITopicService topicService, IHostingEnvironment hostingEnvironment)
        {
            _topicService = topicService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int page)
        {
            return View(_topicService.GetList(page));
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate", new TopicModel());
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", _topicService.Get(id));
        }

        [HttpPost]
        public IActionResult AddOrUpdate(TopicModel model, IFormFile image)
        {
            if (image != null)
            {
                var uniqueFileName = ExtensionMethods.GetUniqueFileName(image.FileName, false);
                Uploader.Upload(image, AppParameter.TopicImagePath, uniqueFileName);
                model.Image = uniqueFileName;
            }
            else
            {
                model.Image = string.Empty;
            }

            model.Slug = model.Slug.ToUrl();
            model = _topicService.AddOrUpdate(model);
            return RedirectToAction("Update", new { id = model.Id });
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _topicService.Delete(model);
            return Json(result);
        }
    }
}
