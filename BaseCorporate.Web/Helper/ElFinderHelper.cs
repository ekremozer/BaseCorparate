using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace BaseCorporate.Web.Helper
{
    public static class ElFinderHelper
    {
        public static string RootPath = "Assets\\Images\\Article";
        public static string RootUrlPath = "Assets/Images/Article";
        public static string ThumbUrl = "/admin/elfinder/thumb/";

        public static Connector GetConnector(HttpRequest request)
        {
            var driver = new FileSystemDriver();

            var absoluteUrl = UriHelper.BuildAbsolute(request.Scheme, request.Host);
            var uri = new Uri(absoluteUrl);

            var appRoot = Directory.GetCurrentDirectory();
            var rootDirectory = Path.Combine(appRoot, RootPath);

            var url = $"{uri.Scheme}://{uri.Authority}/{RootUrlPath}/";
            var urlThumb = $"{uri.Scheme}://{uri.Authority}{ThumbUrl}";

            var root = new RootVolume(rootDirectory, url, urlThumb)
            {
                IsReadOnly = false,
                IsLocked = true,
                Alias = "Images",
                ThumbnailSize = 100
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
