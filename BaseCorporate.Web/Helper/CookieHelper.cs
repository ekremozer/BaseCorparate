using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BaseCorporate.Web.Helper
{
    public static class CookieHelper
    {
        public static void SetCookie(this HttpResponse httpResponse, string key, string value, TimeSpan timeSpan)
        {
            var cookie = new CookieOptions { Expires = DateTime.Now.Add(timeSpan) };
            httpResponse.Cookies.Append(key, value, cookie);
        }

        public static void SetCookie<T>(this HttpResponse httpResponse, string key, T model, TimeSpan timeSpan)
        {
            var modelJson = JsonConvert.SerializeObject(model);
            var cookie = new CookieOptions { Expires = DateTime.Now.Add(timeSpan) };
            httpResponse.Cookies.Append(key, modelJson, cookie);
        }
        public static string GetCookie(this HttpContext httpContext, string key)
        {
            var cookieValue = httpContext.Request.Cookies[key];
            return cookieValue;
        }
        public static T GetCookie<T>(this HttpContext httpContext, string key)
        {
            var cookieString = httpContext.GetCookie(key);
            return string.IsNullOrEmpty(cookieString) ?
                default :
                JsonConvert.DeserializeObject<T>(cookieString);
        }

        public static void RemoveCookie(this HttpResponse httpResponse, string key)
        {
            httpResponse.Cookies.Delete(key);
        }

        public static void RemoveAllCookies(this HttpContext httpContext, HttpResponse httpResponse)
        {
            foreach (var cookie in httpContext.Request.Cookies)
            {
                httpResponse.Cookies.Delete(cookie.Key);
            }
        }
    }
}
