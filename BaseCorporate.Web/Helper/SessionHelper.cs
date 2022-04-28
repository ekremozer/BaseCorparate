using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BaseCorporate.Web.Helper
{
    public static class SessionHelper
    {
        public static void SetSession(this ISession iSession, string key, string sessionString)
        {
            iSession.SetString(key, sessionString);
        }

        public static void SetSession<T>(this ISession iSession, string key, T model)
        {
            var modelJson = JsonConvert.SerializeObject(model);
            iSession.SetString(key, modelJson);
        }

        public static string GetSession(this ISession iSession, string key)
        {
            var sessionString = iSession.GetString(key);
            return sessionString;
        }

        public static T GetSession<T>(this ISession iSession, string key)
        {
            var sessionString = iSession.GetString(key);

            return string.IsNullOrEmpty(sessionString) ?
                default :
                JsonConvert.DeserializeObject<T>(sessionString);
        }

        public static void RemoveSession(this ISession iSession, string key)
        {
            iSession.Remove(key);
        }
    }
}
