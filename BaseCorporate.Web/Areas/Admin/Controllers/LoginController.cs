using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Web.Areas.Admin.Infrastructure;
using BaseCorporate.Web.Areas.Admin.Models;
using BaseCorporate.Web.Helper;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
namespace BaseCorporate.Web.Areas.Admin.Controllers
{
    public class LoginController : BaseAdminController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {
            //var userService = HttpContext.RequestServices.GetService<IUserService>();

            loginViewModel.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            loginViewModel.Password = loginViewModel.Password.ToMd5();
            var user = _userService.Get(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                loginViewModel.ErrorMessage = "Email veya şifre hatalı";
                return View(loginViewModel);
            }

            if (!user.IsActive)
            {
                loginViewModel.ErrorMessage = "Kullanıcı aktif değil";
            }
            HttpContext.Session.SetSession("adminSession", user);

            if (loginViewModel.RememberMe)
            {
                var jsonLoginViewModel = JsonConvert.SerializeObject(loginViewModel);
                var cryptoModel = CryptoHelper.ToTripleDesMd5Encryption(jsonLoginViewModel);
                Response.SetCookie("adminSession", cryptoModel, TimeSpan.FromDays(1));
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }
    }
}
