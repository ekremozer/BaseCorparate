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
using BaseCorporate.Entities.Entities;
using BaseCorporate.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserController(IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(_userService.GetList());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate", new UserModel());
        }

        public IActionResult Update(int id)
        {
            var user = _userService.Get(id);
            return View("AddOrUpdate", user);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(UserModel userModel, IFormFile avatarFile)
        {
            if (avatarFile != null)
            {
                var uniqueFileName = ExtensionMethods.GetUniqueFileName(avatarFile.FileName);
                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Areas/Admin/Assets/Avatar");
                var filePath = Path.Combine(uploads, uniqueFileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    avatarFile.CopyToAsync(stream);
                }
                userModel.Avatar = uniqueFileName;
            }

            if (!string.IsNullOrEmpty(userModel.Password))
            {
                userModel.Password = userModel.Password.ToMd5();
            }

            userModel = _userService.AddOrUpdate(userModel);
            return Json(userModel);

            return RedirectToAction("Update", new { id = userModel.Id });
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _userService.Delete(model);
            return Json(result);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.RemoveSession("adminSession");
            Response.RemoveCookie("adminSession");
            return RedirectToAction("Index", "Login");
        }
    }
}
