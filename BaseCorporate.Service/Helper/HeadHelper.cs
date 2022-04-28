using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;

namespace BaseCorporate.Service.Helper
{
    public static class HeadHelper
    {
        public static HeadModel GetDefaultHeadModel(int page = 1)
        {
            var siteAddress = AppParameter.MetaSetting.SiteAddress;
            var title = AppParameter.MetaSetting.Title;
            var titleExtension = AppParameter.MetaSetting.TitleExtension;
            var metaDescription = AppParameter.MetaSetting.MetaDescription;
            var metaKeywords = AppParameter.MetaSetting.MetaKeywords;
            var author = AppParameter.MetaSetting.Author;
            var ogDescription = AppParameter.MetaSetting.OgDescription;
            var ogImage = AppParameter.MetaSetting.OgImage;
            var ogSiteName = AppParameter.MetaSetting.OgSiteName;
            var ogTitle = AppParameter.MetaSetting.OgTitle;
            var ogType = AppParameter.MetaSetting.OgType;
            var pageExtension = page <= 1 ? string.Empty : $" Sayfa - {page} ";

            var currentUrl = AppParameter.MetaSetting.SiteAddress;
            var defaultHead = new HeadModel
            {
                Title = title + pageExtension + titleExtension,
                Author = author,
                Canonical = currentUrl,
                Description = metaDescription,
                Keywords = metaKeywords,
                OgDescription = ogDescription,
                OgImage = siteAddress + ogImage,
                OgSiteName = ogSiteName,
                OgTitle = ogTitle,
                OgType = ogType,
                OgUrl = currentUrl,
                SiteAddress = siteAddress,
                TitleExtension = titleExtension
            };
            return defaultHead;
        }

        public static HeadModel GetCustomHeadModel(string title, string canonicalUrl = null)
        {
            var defaultHeadModel = GetDefaultHeadModel();

            var currentUrl = !string.IsNullOrEmpty(canonicalUrl) ? $"{defaultHeadModel.SiteAddress}/{canonicalUrl}" : defaultHeadModel.SiteAddress;
            var contactHead = new HeadModel
            {
                Title = title + defaultHeadModel.TitleExtension,
                Author = defaultHeadModel.Author,
                Canonical = currentUrl,
                Description = defaultHeadModel.Description,
                Keywords = defaultHeadModel.Keywords,
                OgDescription = defaultHeadModel.OgDescription,
                OgImage = defaultHeadModel.SiteAddress + defaultHeadModel.OgImage,
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = title + defaultHeadModel.TitleExtension,
                OgType = defaultHeadModel.OgType,
                OgUrl = currentUrl
            };
            return contactHead;
        }

        public static HeadModel GetArticleDetailHeadModel(ArticleViewModel article)
        {

            var defaultHeadModel = GetDefaultHeadModel();
            var articleHead = new HeadModel
            {
                Title = $"{article.Title} {defaultHeadModel.TitleExtension}",
                Author = defaultHeadModel.Author,
                Canonical = $"{defaultHeadModel.SiteAddress}/{article.Slug}",
                Description = string.IsNullOrEmpty(article.MetaDescription) ? defaultHeadModel.Description : article.MetaDescription,
                Keywords = string.IsNullOrEmpty(article.MetaKeywords) ? defaultHeadModel.Keywords : article.MetaKeywords,
                OgDescription = string.IsNullOrEmpty(article.MetaDescription) ? defaultHeadModel.OgDescription : article.MetaDescription,
                OgImage = string.IsNullOrEmpty(article.Image) ? defaultHeadModel.OgImage : $"{defaultHeadModel.SiteAddress}/{AppParameter.ArticleImagePath}/{article.Image}",
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = $"{article.Title} {defaultHeadModel.TitleExtension}",
                OgType = "article",
                OgUrl = defaultHeadModel.OgUrl
            };
            return articleHead;
        }

        public static HeadModel GetCategoryListHeadModel(CategoryModel category, int page)
        {
            var defaultHeadModel = GetDefaultHeadModel();
            var canonical = $"{defaultHeadModel.SiteAddress}/{category.Slug}";
            canonical = page > 1 ? $"{canonical}/{page}" : canonical;

            var tagListHead = new HeadModel
            {
                Title = $"{category.Name} {defaultHeadModel.TitleExtension}",
                Author = defaultHeadModel.Author,
                Canonical = canonical,
                Description = string.IsNullOrEmpty(category.MetaDescription) ? defaultHeadModel.Description : category.MetaDescription,
                Keywords = defaultHeadModel.Keywords,
                OgDescription = string.IsNullOrEmpty(category.MetaDescription) ? defaultHeadModel.OgDescription : category.MetaDescription,
                OgImage = defaultHeadModel.OgImage,
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = $"{category.Name} {defaultHeadModel.TitleExtension}",
                OgType = "website",
                OgUrl = defaultHeadModel.OgUrl
            };
            return tagListHead;
        }

        public static HeadModel GetTagListHeadModel(TagModel tag, int page)
        {
            var defaultHeadModel = GetDefaultHeadModel();
            var canonical = $"{defaultHeadModel.SiteAddress}/{tag.Slug}";
            canonical = page > 1 ? $"{canonical}/{page}" : canonical;
            var categoryListHead = new HeadModel
            {
                Title = $"{tag.Name} {defaultHeadModel.TitleExtension}",
                Author = defaultHeadModel.Author,
                Canonical = canonical,
                Description = defaultHeadModel.Description,
                Keywords = defaultHeadModel.Keywords,
                OgDescription = defaultHeadModel.OgDescription,
                OgImage = defaultHeadModel.OgImage,
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = $"{tag.Name} {defaultHeadModel.TitleExtension}",
                OgType = "website",
                OgUrl = defaultHeadModel.OgUrl
            };
            return categoryListHead;
        }

        public static HeadModel GetTopicHeadModel(TopicModel topic)
        {
            var defaultHeadModel = GetDefaultHeadModel();
            var canonical = $"{defaultHeadModel.SiteAddress}/{topic.Slug}";
            var categoryListHead = new HeadModel
            {
                Title = $"{topic.Title} {defaultHeadModel.TitleExtension}",
                Author = defaultHeadModel.Author,
                Canonical = canonical,
                Description = defaultHeadModel.Description,
                Keywords = defaultHeadModel.Keywords,
                OgDescription = defaultHeadModel.OgDescription,
                OgImage = defaultHeadModel.OgImage ?? AppParameter.MetaSetting.OgImage,
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = $"{topic.Title} {defaultHeadModel.TitleExtension}",
                OgType = "website",
                OgUrl = defaultHeadModel.OgUrl
            };
            return categoryListHead;
        }

        public static HeadModel GetSearchListHeadModel(string q, int page)
        {
            var defaultHeadModel = GetDefaultHeadModel();
            var canonical = $"{defaultHeadModel.SiteAddress}/ara?q={q}";
            canonical = page > 1 ? $"{canonical}/{page}" : canonical;
            var categoryListHead = new HeadModel
            {
                Title = $"{q} {defaultHeadModel.TitleExtension}",
                Author = defaultHeadModel.Author,
                Canonical = canonical,
                Description = defaultHeadModel.Description,
                Keywords = defaultHeadModel.Keywords,
                OgDescription = defaultHeadModel.OgDescription,
                OgImage = defaultHeadModel.OgImage,
                OgSiteName = defaultHeadModel.OgSiteName,
                OgTitle = $"{q} {defaultHeadModel.TitleExtension}",
                OgType = "website",
                OgUrl = defaultHeadModel.OgUrl
            };
            return categoryListHead;
        }
    }
}