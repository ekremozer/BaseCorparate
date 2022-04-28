using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Utility.Model;

namespace BaseCorporate.Utility.Helper
{
    public static class AppParameter
    {
        public static string ArticleImagePath = "Assets/Images/Article";
        public static string TopicImagePath = "Assets/Images/Topic";
        public static string ArticleFilePath = "Areas/Admin/Files/ArticleFiles";
        public static string GetMenuGroupCacheName(string key) { return $"GetMenuGroup-{key}"; }
        public static string ArticleLastAddedCacheName = "ArticleLastAdded";
        public static string ArticleBestSellerCacheName = "ArticleBestSeller";
        public static string GetCategoriesThreeCacheName(int categoryId) { return $"GetCategoriesThree-{categoryId}"; }
        public static AppSettings AppSettings { get; set; }
        public static MailSetting MailSetting { get; set; }
        public static MetaSetting MetaSetting { get; set; }

        public static string SiteMapCacheName = "SiteMap";
        public static string GetByCategoryForDetail(int? categoryId) { return $"GetByCategoryForDetail-{categoryId}"; }
        public static string RobotsTxtPath = "";
        public static string ContentRootPath = "";
    }
}
