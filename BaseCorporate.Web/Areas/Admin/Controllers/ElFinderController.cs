using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Helper;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class ElFinderController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Connector()
        {
            var connector = ElFinderHelper.GetConnector(Request);
            return await connector.ProcessAsync(Request);
        }

        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = ElFinderHelper.GetConnector(Request);
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }
    }
}
