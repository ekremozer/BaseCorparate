using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Areas.Admin.Models;
using BaseCorporate.Web.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BaseCorporate.Web.Infrastructure
{
    public class BaseWebController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region RequestControl
            var actionDescriptor = filterContext.ActionDescriptor as dynamic;
            var controllerName = actionDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;
            var method = filterContext.HttpContext.Request.Method;
            #endregion

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
