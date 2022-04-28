using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Helper;
using BaseCorporate.Utility.Helper;

namespace BaseCorporate.Web.Infrastructure
{
    public static class TestData
    {
        public static void Builder()
        {
            #region LiveControl
            if (AppParameter.AppSettings.ConnectionString.Contains("78.135.105.9"))
            {
                return;
            }
            #endregion

            #region Context - DbReset
            using var context = new EfContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            #endregion

            #region User
            var user = new User
            {
                Name = "Ekrem",
                Surname = "ÖZER",
                Email = "ekrem.zr@gmail.com",
                Password = "123456".ToMd5(),
                Avatar = "default.jpg"
            };
            context.Users.Add(user);
            context.SaveChanges();
            user.CreatedByUserId = user.Id;
            context.SaveChanges();
            #endregion

            #region Category
            var category = new Category
            {
                Name = "Asp.Net MVC",
                Description = "Açıklama",
                MetaDescription = "Meta Açıklama",
                Slug = "Asp.Net MVC".ToUrl(),
                CreatedByUserId = user.Id
            };
            context.Categories.Add(category);
            context.SaveChanges();

            //category.DisplayName = category.ParentCategoryId == 0 ? category.Name : CategoryHelper.GetDisplayName(category.Id);
            //category.BreadCrumb = CategoryHelper.GetBreadCrumb(category.Id);
            //context.SaveChanges();

            //var subCategory = new Category
            //{
            //    Name = "Alt Kategori",
            //    Description = "Açıklama",
            //    MetaDescription = "Meta Açıklama",
            //    Slug = "alt-kategori",
            //    ParentCategoryId = category.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.Categories.Add(subCategory);
            //context.SaveChanges();

            //subCategory.DisplayName = subCategory.ParentCategoryId == 0 ? subCategory.Name : CategoryHelper.GetDisplayName(subCategory.Id);
            //subCategory.BreadCrumb = CategoryHelper.GetBreadCrumb(subCategory.Id);
            //context.SaveChanges();

            #endregion

            #region Article
            //var article = new Article
            //{
            //    PageTitle = "Sayfa Başlığı",
            //    Title = "İç Başlık",
            //    Image = "default.jpg",
            //    Body = "<p>İçerik</p>",
            //    MetaDescription = "Meta Description",
            //    MetaKeywords = "MetaKeywords1,MetaKeywords2",
            //    File = "TempFile.docx",
            //    CategoryId = subCategory.Id,
            //    AllowComment = false,
            //    IsFree = false,
            //    Price = 25,
            //    WritePrice = 350,
            //    Slug = "ilk-makale",
            //    SiteMapPriority = "1",
            //    CreatedByUserId = user.Id
            //};
            //context.Articles.Add(article);
            //context.SaveChanges();

            //var article2 = new Article
            //{
            //    PageTitle = "2.Sayfa Başlığı",
            //    Title = "2.İç Başlık",
            //    Image = "default.jpg",
            //    Body = "<p>İçerik</p>",
            //    MetaDescription = "Meta Description",
            //    MetaKeywords = "MetaKeywords1,MetaKeywords2",
            //    File = "TempFile.docx",
            //    CategoryId = subCategory.Id,
            //    AllowComment = false,
            //    IsFree = false,
            //    Price = 30,
            //    WritePrice = 300,
            //    Slug = "ikinci-makale",
            //    SiteMapPriority = "1",
            //    CreatedByUserId = user.Id
            //};
            //context.Articles.Add(article2);

            //var article3 = new Article
            //{
            //    PageTitle = "3. Makale",
            //    Title = "3.İç Başlık",
            //    Image = "default.jpg",
            //    Body = "<p>İçerik</p>",
            //    MetaDescription = "Meta Description",
            //    MetaKeywords = "MetaKeywords1,MetaKeywords2",
            //    File = "TempFile.docx",
            //    CategoryId = subCategory.Id,
            //    AllowComment = false,
            //    IsFree = false,
            //    Price = 30,
            //    WritePrice = 300,
            //    Slug = "ucuncu-makale",
            //    SiteMapPriority = "1",
            //    CreatedByUserId = user.Id
            //};
            //context.Articles.Add(article3);
            //context.SaveChanges();
            #endregion

            #region Tag
            //var tag1 = new Tag
            //{
            //    Name = "etiket-1",
            //    Description = "Açıklama",
            //    MetaDescription = "MetaDescription",
            //    Slug = "etiket-1",
            //    CreatedByUserId = user.Id
            //};
            //context.Tags.Add(tag1);

            //var tag2 = new Tag
            //{
            //    Name = "etiket-2",
            //    Description = "Açıklama",
            //    MetaDescription = "MetaDescription",
            //    Slug = "etiket-2",
            //    CreatedByUserId = user.Id
            //};
            //context.Tags.Add(tag2);

            //var tag3 = new Tag
            //{
            //    Name = "etiket-3",
            //    Description = "Açıklama",
            //    MetaDescription = "MetaDescription",
            //    Slug = "etiket-3",
            //    CreatedByUserId = user.Id
            //};
            //context.Tags.Add(tag3);
            //context.SaveChanges();
            #endregion

            #region TagMapping
            //var tagMapping1 = new TagMapping
            //{
            //    ArticleId = article.Id,
            //    TagId = tag1.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.TagMappings.Add(tagMapping1);

            //var tagMapping2 = new TagMapping
            //{
            //    ArticleId = article.Id,
            //    TagId = tag2.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.TagMappings.Add(tagMapping2);
            //context.SaveChanges();
            #endregion

            #region Topic
            //var topic = new Topic
            //{
            //    PageTitle = "Ön Bilgilendirme Metni",
            //    Title = "Ön Bilgilendirme Metni",
            //    Image = null,
            //    Body = "<p>İçerik</p>",
            //    MetaDescription = "Ön Bilgilendirme Metni",
            //    MetaKeywords = null,
            //    Slug = "on-bilgilendirme-metni",
            //    SiteMapPriority = "1",
            //    CreatedByUserId = user.Id
            //};
            //context.Topics.Add(topic);
            //context.SaveChanges();

            //var topic2 = new Topic
            //{
            //    PageTitle = "KVKK Bilgilendirme Metni",
            //    Title = "KVKK Bilgilendirme Metni",
            //    Image = null,
            //    Body = "<p>İçerik</p>",
            //    MetaDescription = "KVKK Bilgilendirme Metni",
            //    MetaKeywords = null,
            //    Slug = "kvkk-bilgilendirme-metni",
            //    SiteMapPriority = "1",
            //    CreatedByUserId = user.Id
            //};
            //context.Topics.Add(topic2);
            //context.SaveChanges();
            #endregion

            #region MenuGroup
            var headerMenuGroup = new MenuGroup
            {
                Name = "Üst Menü",
                Key = "header",
                CreatedByUserId = user.Id
            };
            context.MenuGroups.Add(headerMenuGroup);
            context.SaveChanges();

            //var sidebarMenuGroup = new MenuGroup
            //{
            //    Name = "Kategoriler",
            //    Key = "side-bar",
            //    CreatedByUserId = user.Id
            //};
            //context.MenuGroups.Add(sidebarMenuGroup);

            //var footer1MenuGroup = new MenuGroup
            //{
            //    Name = "Hakkımızda",
            //    Key = "footer-1",
            //    CreatedByUserId = user.Id
            //};
            //context.MenuGroups.Add(footer1MenuGroup);

            //var footer2MenuGroup = new MenuGroup
            //{
            //    Name = "Kategoriler",
            //    Key = "footer-2",
            //    CreatedByUserId = user.Id
            //};
            //context.MenuGroups.Add(footer2MenuGroup);

            //var footer3MenuGroup = new MenuGroup
            //{
            //    Name = "Yardım",
            //    Key = "footer-3",
            //    CreatedByUserId = user.Id
            //};
            //context.MenuGroups.Add(footer3MenuGroup);
            //context.SaveChanges();
            #endregion

            #region MenuItem
            var menuItem1 = new MenuItem
            {
                Name = "Ana Sayfa",
                Link = "/",
                External = false,
                GroupId = headerMenuGroup.Id,
                OrderBy = 1,
                CreatedByUserId = user.Id
            };
            context.MenuItems.Add(menuItem1);

            //var menuItem2 = new MenuItem
            //{
            //    Name = "Diğer",
            //    Link = "#",
            //    External = false,
            //    GroupId = headerMenuGroup.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem2);

            //var menuItem3 = new MenuItem
            //{
            //    Name = "İletişim",
            //    Link = "/iletisim",
            //    External = false,
            //    GroupId = headerMenuGroup.Id,
            //    OrderBy = 3,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem3);

            //var menuItem4 = new MenuItem
            //{
            //    Name = "Hakkımızda",
            //    Link = "/hakkimizda",
            //    External = false,
            //    GroupId = headerMenuGroup.Id,
            //    OrderBy = 4,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem4);

            //var menuItem5 = new MenuItem
            //{
            //    Name = "İcra Hukuku",
            //    Link = "/icra-hukuku",
            //    External = false,
            //    GroupId = sidebarMenuGroup.Id,
            //    OrderBy = 1,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem5);

            //var menuItem6 = new MenuItem
            //{
            //    Name = "Aile Hukuku",
            //    Link = "/aile-hukuku",
            //    External = false,
            //    GroupId = sidebarMenuGroup.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem6);

            //var menuItem7 = new MenuItem
            //{
            //    Name = "Menü 1",
            //    Link = "/menu-1",
            //    External = false,
            //    GroupId = footer1MenuGroup.Id,
            //    OrderBy = 1,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem7);

            //var menuItem8 = new MenuItem
            //{
            //    Name = "Menü 2",
            //    Link = "/menu-2",
            //    External = false,
            //    GroupId = footer1MenuGroup.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem8);

            //var menuItem9 = new MenuItem
            //{
            //    Name = "Menü 3",
            //    Link = "/menu-3",
            //    External = false,
            //    GroupId = footer1MenuGroup.Id,
            //    OrderBy = 3,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem9);

            //var menuItem10 = new MenuItem
            //{
            //    Name = "Menü 4",
            //    Link = "/menu-4",
            //    External = false,
            //    GroupId = footer1MenuGroup.Id,
            //    OrderBy = 4,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem10);

            //var menuItem11 = new MenuItem
            //{
            //    Name = "Menü 5",
            //    Link = "/menu-5",
            //    External = false,
            //    GroupId = footer1MenuGroup.Id,
            //    OrderBy = 5,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem11);


            //var menuItem12 = new MenuItem
            //{
            //    Name = "Menü 1",
            //    Link = "/menu-1",
            //    External = false,
            //    GroupId = footer2MenuGroup.Id,
            //    OrderBy = 1,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem12);

            //var menuItem13 = new MenuItem
            //{
            //    Name = "Menü 2",
            //    Link = "/menu-2",
            //    External = false,
            //    GroupId = footer2MenuGroup.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem13);

            //var menuItem14 = new MenuItem
            //{
            //    Name = "Menü 3",
            //    Link = "/menu-3",
            //    External = false,
            //    GroupId = footer2MenuGroup.Id,
            //    OrderBy = 3,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem14);

            //var menuItem15 = new MenuItem
            //{
            //    Name = "Menü 4",
            //    Link = "/menu-4",
            //    External = false,
            //    GroupId = footer2MenuGroup.Id,
            //    OrderBy = 4,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem15);

            //var menuItem16 = new MenuItem
            //{
            //    Name = "Menü 5",
            //    Link = "/menu-5",
            //    External = false,
            //    GroupId = footer2MenuGroup.Id,
            //    OrderBy = 5,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem16);

            //var menuItem17 = new MenuItem
            //{
            //    Name = "Menü 1",
            //    Link = "/menu-1",
            //    External = false,
            //    GroupId = footer3MenuGroup.Id,
            //    OrderBy = 1,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem17);

            //var menuItem18 = new MenuItem
            //{
            //    Name = "Menü 2",
            //    Link = "/menu-2",
            //    External = false,
            //    GroupId = footer3MenuGroup.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem18);

            //var menuItem19 = new MenuItem
            //{
            //    Name = "Menü 3",
            //    Link = "/menu-3",
            //    External = false,
            //    GroupId = footer3MenuGroup.Id,
            //    OrderBy = 3,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuItems.Add(menuItem19);

            context.SaveChanges();
            #endregion

            #region MenuSubItem
            //var menuSubItem1 = new MenuSubItem
            //{
            //    Name = "Vizyonumuz",
            //    Link = "/vizyonumuz",
            //    External = false,
            //    ItemId = menuItem2.Id,
            //    OrderBy = 1,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuSubItems.Add(menuSubItem1);

            //var menuSubItem2 = new MenuSubItem
            //{
            //    Name = "Misyonumuz",
            //    Link = "/misyonumuz",
            //    External = false,
            //    ItemId = menuItem2.Id,
            //    OrderBy = 2,
            //    CreatedByUserId = user.Id
            //};
            //context.MenuSubItems.Add(menuSubItem2);
            //context.SaveChanges();
            #endregion

            #region PageNotFoundLog
            //var pageNotFoundLog = new PageNotFoundLog
            //{
            //    PageUrl = "dilekceler/miras-dilekcesi.html",
            //    ReferrerUrl = "/miras",
            //    UserIp = "8.8.8.8",
            //    UserAgent = "Chrome",
            //    Count = 5
            //};
            //context.PageNotFoundLogs.Add(pageNotFoundLog);
            //context.SaveChanges();
            #endregion

            #region RedirectRecord
            //var redirectRecord = new RedirectRecord
            //{
            //    OldUrl = "/eski-url",
            //    NewUrl = "/yeni-url",
            //    RedirectCount = 4,
            //    CreatedByUserId = user.Id
            //};
            //context.RedirectRecords.Add(redirectRecord);
            //context.SaveChanges();
            #endregion

            #region Setting

            #region Meta
            var siteAddress = new Setting
            {
                Key = "SiteAddress",
                Value = "https://www.davadilekcesi.net",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(siteAddress);

            var title = new Setting
            {
                Key = "Title",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(title);

            var titleExtension = new Setting
            {
                Key = "TitleExtension",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(titleExtension);

            var metaDescription = new Setting
            {
                Key = "MetaDescription",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(metaDescription);

            var metaKeywords = new Setting
            {
                Key = "MetaKeywords",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(metaKeywords);

            var author = new Setting
            {
                Key = "Author",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(author);

            var ogDescription = new Setting
            {
                Key = "OgDescription",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(ogDescription);

            var ogImage = new Setting
            {
                Key = "OgImage",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(ogImage);

            var ogSiteName = new Setting
            {
                Key = "OgSiteName",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(ogSiteName);

            var ogTitle = new Setting
            {
                Key = "OgTitle",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(ogTitle);

            var ogType = new Setting
            {
                Key = "OgType",
                Value = "Dava Dilekçesi",
                GroupName = "meta",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(ogType);
            #endregion

            #region Email

            var mailSender = new Setting
            {
                Key = "MailSender",
                Value = "Dava Dilekçesi",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(mailSender);

            var smtpMail = new Setting
            {
                Key = "SmtpMail",
                Value = "noreply@davadilekcesi.net",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(smtpMail);

            var siteMail = new Setting
            {
                Key = "EditorMail",
                Value = "info@davadilekcesi.net",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(siteMail);

            var siteMailSenderName = new Setting
            {
                Key = "AdminMail",
                Value = "ekrem.zr@gmail.com",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(siteMailSenderName);

            var mailPass = new Setting
            {
                Key = "MailPass",
                Value = "AliH4mza**66",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(mailPass);

            var port = new Setting
            {
                Key = "Port",
                Value = "587",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(port);

            var host = new Setting
            {
                Key = "Host",
                Value = "78.135.105.9",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(host);

            var eEnableSsl = new Setting
            {
                Key = "EnableSsl",
                Value = "0",
                GroupName = "mail",
                CreatedByUserId = user.Id
            };
            context.Settings.Add(eEnableSsl);

            #endregion

            context.SaveChanges();
            #endregion

            #region UrlRecord
            var categoryUrl = new UrlRecord
            {
                Slug = category.Slug,
                CategoryId = category.Id,
                CreatedByUserId = user.Id
            };
            context.UrlRecords.Add(categoryUrl);
            context.SaveChanges();
            category.UrlRecordId = categoryUrl.Id;
            context.SaveChanges();

            //var subCategoryUrl = new UrlRecord
            //{
            //    Slug = subCategory.Slug,
            //    CategoryId = subCategory.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(subCategoryUrl);
            //context.SaveChanges();
            //subCategory.UrlRecordId = subCategoryUrl.Id;
            //context.SaveChanges();

            //var tag1Url = new UrlRecord
            //{
            //    Slug = tag1.Slug,
            //    TagId = tag1.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(tag1Url);
            //context.SaveChanges();
            //tag1.UrlRecordId = tag1Url.Id;
            //context.SaveChanges();

            //var tag2Url = new UrlRecord
            //{
            //    Slug = tag2.Slug,
            //    TagId = tag2.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(tag2Url);
            //context.SaveChanges();
            //tag2.UrlRecordId = tag2Url.Id;
            //context.SaveChanges();

            //var tag3Url = new UrlRecord
            //{
            //    Slug = tag3.Slug,
            //    TagId = tag3.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(tag3Url);
            //context.SaveChanges();
            //tag3.UrlRecordId = tag3Url.Id;
            //context.SaveChanges();

            //var articleUrl = new UrlRecord
            //{
            //    Slug = article.Slug,
            //    ArticleId = article.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(articleUrl);
            //context.SaveChanges();
            //article.UrlRecordId = articleUrl.Id;
            //context.SaveChanges();

            //var articleUr2 = new UrlRecord
            //{
            //    Slug = article2.Slug,
            //    ArticleId = article2.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(articleUr2);
            //context.SaveChanges();
            //article2.UrlRecordId = articleUr2.Id;
            //context.SaveChanges();

            //var articleUr3 = new UrlRecord
            //{
            //    Slug = article3.Slug,
            //    ArticleId = article3.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(articleUr3);
            //context.SaveChanges();
            //article3.UrlRecordId = articleUr3.Id;
            //context.SaveChanges();

            //var topicUrl = new UrlRecord
            //{
            //    Slug = topic.Slug,
            //    TopicId = topic.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(topicUrl);
            //context.SaveChanges();
            //topic.UrlRecordId = topicUrl.Id;
            //context.SaveChanges();

            //var topic2Url = new UrlRecord
            //{
            //    Slug = topic2.Slug,
            //    TopicId = topic2.Id,
            //    CreatedByUserId = user.Id
            //};
            //context.UrlRecords.Add(topic2Url);
            //context.SaveChanges();
            //topic.UrlRecordId = topic2Url.Id;
            //context.SaveChanges();
            #endregion
        }
    }
}
