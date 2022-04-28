using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class RobotsTxtController : BaseAdminController
    {
        public IActionResult Index()
        {
            var txt = System.IO.File.ReadAllText(AppParameter.RobotsTxtPath);
            return View((object)txt);
        }

        [HttpPost]
        public IActionResult Update([FromForm] string robotsTxt)
        {
            System.IO.File.WriteAllTextAsync(AppParameter.RobotsTxtPath, robotsTxt);
            return Redirect("/admin/RobotsTxt");
        }
    }
}
