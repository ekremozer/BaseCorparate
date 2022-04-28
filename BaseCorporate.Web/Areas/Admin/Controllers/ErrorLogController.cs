using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Web.Areas.Admin.Infrastructure;

namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class ErrorLogController : BaseAdminController
    {
        private readonly IErrorLogService _errorLogService;

        public ErrorLogController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        public IActionResult Index()
        {
            return View(_errorLogService.GetList());
        }

        public IActionResult DeleteAll()
        {
            var result = _errorLogService.DeleteAll();
            return Json(result);
        }

        public IActionResult Delete(BaseModel model)
        {
            var result = _errorLogService.Delete(model);
            return Json(result);
        }
    }
}
