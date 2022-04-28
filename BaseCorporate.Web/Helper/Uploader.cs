using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Utility.Helper;
using Microsoft.AspNetCore.Http;

namespace BaseCorporate.Web.Helper
{
    public static class Uploader
    {
        public static async void Upload(IFormFile file, string path, string uniqueFileName)
        {
            if (file != null)
            {
                var uploads = Path.Combine(AppParameter.ContentRootPath, path);
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var fullPath = Path.Combine(uploads, uniqueFileName);
                //if (File.Exists(fullPath))
                //{
                //    uniqueFileName = Guid.NewGuid().ToString() + uniqueFileName;
                //    fullPath = Path.Combine(uploads, uniqueFileName);
                //}
                await using var stream = File.Create(fullPath);
                await file.CopyToAsync(stream);
            }
        }
    }
}
