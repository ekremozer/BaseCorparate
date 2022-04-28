using System;
using System.Linq;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Areas.Admin.Models;
using BaseCorporate.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BaseCorporate.Web.Areas.Admin.Infrastructure
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        #region Fields
        internal int SessionUserId
        {
            get => ViewBag.SessionUserId;
            set => ViewBag.SessionUserId = value;
        }

        internal UserModel SessionUserModel
        {
            get => ViewBag.AdminSession;
            set => ViewBag.AdminSession = value;
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region RequestControl
            var actionDescriptor = filterContext.ActionDescriptor as dynamic;
            var controllerName = actionDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;
            var method = filterContext.HttpContext.Request.Method;
            var excludeControllers = new[] { "Login", "ForgotPassword" };
            if (Array.IndexOf(excludeControllers, controllerName) >= 0)
            {
                return;
            }
            #endregion

            #region SessionControl
            var adminSession = HttpContext.Session.GetSession<UserModel>("adminSession");
            if (adminSession == null)
            {
                #region CookieControl
                var adminSessionCookie = HttpContext.GetCookie("adminSession");
                if (adminSessionCookie != null)
                {
                    adminSessionCookie = CryptoHelper.ToTripleDesMd5Decrypt(adminSessionCookie);
                    var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(adminSessionCookie);
                    if (loginViewModel.IpAddress == Request.HttpContext.Connection.RemoteIpAddress.ToString())
                    {
                        var userService = filterContext.HttpContext.RequestServices.GetService<IUserService>();
                        var user = userService?.Get(loginViewModel.Email, loginViewModel.Password);
                        if (user != null)
                        {
                            HttpContext.Session.SetSession("adminSession", user);

                            if (loginViewModel.RememberMe)
                            {
                                var jsonLoginViewModel = JsonConvert.SerializeObject(loginViewModel);
                                var cryptoModel = CryptoHelper.ToTripleDesMd5Encryption(jsonLoginViewModel);
                                Response.SetCookie("adminSession", cryptoModel, TimeSpan.FromDays(1));
                            }

                            var returnUrl = HttpContext.Request.Path.Value ?? "/Admin";
                            filterContext.Result = new RedirectResult(returnUrl);
                            return;
                        }
                    }
                }
                #endregion

                #region  ReturnLoginPage
                var requestUrl = HttpContext.Request.Path;
                if (requestUrl != null)
                {
                    var returnUrl = requestUrl.Value;
                    filterContext.Result = new RedirectResult($"~/Admin/Login?returnUrl={returnUrl}");
                    return;
                }
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
                #endregion
            }
            #endregion

            #region SetSessionUserId
            if (filterContext.Controller is Controller controller)
            {
                //GET: var sessionUserId = ViewData["SessionUserId"].GetObjectValue<int>();
                controller.ViewData["SessionUserId"] = adminSession.Id;
                controller.ViewData["SessionUser"] = adminSession;
            }

            if (filterContext.ActionArguments.FirstOrDefault().Value is BaseModel model)
            {
                model.SessionUserId = adminSession.Id;
                model.SessionUser = adminSession;
            }
            #endregion

            #region UpdateSessionAndViewBag
            ViewBag.AdminSession = adminSession;
            SessionUserId = adminSession.Id;
            SessionUserModel = adminSession;
            HttpContext.Session.SetSession("adminSession", adminSession);
            #endregion

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
