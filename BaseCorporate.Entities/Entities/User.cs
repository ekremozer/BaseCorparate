using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public ICollection<User> CreatedByUsers { get; set; }
        public ICollection<User> UpdatedByUsers { get; set; }
        public ICollection<User> DeletedByUsers { get; set; }

        public ICollection<Category> CreatedByCategories { get; set; }
        public ICollection<Category> UpdatedByCategories { get; set; }
        public ICollection<Category> DeletedByCategories { get; set; }

        public ICollection<Article> CreatedByArticles { get; set; }
        public ICollection<Article> UpdatedByArticles { get; set; }
        public ICollection<Article> DeletedByArticles { get; set; }

        public ICollection<Tag> CreatedByTags { get; set; }
        public ICollection<Tag> UpdatedByTags { get; set; }
        public ICollection<Tag> DeletedByTags { get; set; }

        public ICollection<TagMapping> CreatedByTagMappings { get; set; }
        public ICollection<TagMapping> UpdatedByTagMappings { get; set; }
        public ICollection<TagMapping> DeletedByTagMappings { get; set; }

        public ICollection<Topic> CreatedByTopics { get; set; }
        public ICollection<Topic> UpdatedByTopics { get; set; }
        public ICollection<Topic> DeletedByTopics { get; set; }

        public ICollection<MenuGroup> CreatedByMenuGroups { get; set; }
        public ICollection<MenuGroup> UpdatedByMenuGroups { get; set; }
        public ICollection<MenuGroup> DeletedByMenuGroups { get; set; }

        public ICollection<MenuItem> CreatedByMenuItems { get; set; }
        public ICollection<MenuItem> UpdatedByMenuItems { get; set; }
        public ICollection<MenuItem> DeletedByMenuItems { get; set; }

        public ICollection<MenuSubItem> CreatedByMenuSubItems { get; set; }
        public ICollection<MenuSubItem> UpdatedByMenuSubItems { get; set; }
        public ICollection<MenuSubItem> DeletedByMenuSubItems { get; set; }

        public ICollection<Setting> CreatedBySettings { get; set; }
        public ICollection<Setting> UpdatedBySettings { get; set; }
        public ICollection<Setting> DeletedBySettings { get; set; }

        public ICollection<RedirectRecord> CreatedByRedirectRecords { get; set; }
        public ICollection<RedirectRecord> UpdatedByRedirectRecords { get; set; }
        public ICollection<RedirectRecord> DeletedByRedirectRecords { get; set; }

        public ICollection<PageNotFoundLog> CreatedByPageNotFoundLogs { get; set; }
        public ICollection<PageNotFoundLog> UpdatedByPageNotFoundLogs { get; set; }
        public ICollection<PageNotFoundLog> DeletedByPageNotFoundLogs { get; set; }

        public ICollection<UrlRecord> CreatedByUrlRecords { get; set; }
        public ICollection<UrlRecord> UpdatedByUrlRecords { get; set; }
        public ICollection<UrlRecord> DeletedByUrlRecords { get; set; }

        public ICollection<ErrorLog> CreatedByErrorLogs { get; set; }
        public ICollection<ErrorLog> UpdatedByErrorLogs { get; set; }
        public ICollection<ErrorLog> DeletedByErrorLogs { get; set; }
        public User()
        {
            CreatedByUsers = new HashSet<User>();
            UpdatedByUsers = new HashSet<User>();
            DeletedByUsers = new HashSet<User>();

            CreatedByCategories = new HashSet<Category>();
            UpdatedByCategories = new HashSet<Category>();
            DeletedByCategories = new HashSet<Category>();

            CreatedByArticles = new HashSet<Article>();
            UpdatedByArticles = new HashSet<Article>();
            DeletedByArticles = new HashSet<Article>();

            CreatedByTags = new HashSet<Tag>();
            UpdatedByTags = new HashSet<Tag>();
            DeletedByTags = new HashSet<Tag>();

            CreatedByTagMappings = new HashSet<TagMapping>();
            UpdatedByTagMappings = new HashSet<TagMapping>();
            DeletedByTagMappings = new HashSet<TagMapping>();

            CreatedByTopics = new HashSet<Topic>();
            UpdatedByTopics = new HashSet<Topic>();
            DeletedByTopics = new HashSet<Topic>();

            CreatedByMenuGroups = new HashSet<MenuGroup>();
            UpdatedByMenuGroups = new HashSet<MenuGroup>();
            DeletedByMenuGroups = new HashSet<MenuGroup>();

            CreatedByMenuItems = new HashSet<MenuItem>();
            UpdatedByMenuItems = new HashSet<MenuItem>();
            DeletedByMenuItems = new HashSet<MenuItem>();

            CreatedByMenuSubItems = new HashSet<MenuSubItem>();
            UpdatedByMenuSubItems = new HashSet<MenuSubItem>();
            DeletedByMenuSubItems = new HashSet<MenuSubItem>();

            CreatedBySettings = new HashSet<Setting>();
            UpdatedBySettings = new HashSet<Setting>();
            DeletedBySettings = new HashSet<Setting>();

            CreatedByRedirectRecords = new HashSet<RedirectRecord>();
            UpdatedByRedirectRecords = new HashSet<RedirectRecord>();
            DeletedByRedirectRecords = new HashSet<RedirectRecord>();

            CreatedByPageNotFoundLogs = new HashSet<PageNotFoundLog>();
            UpdatedByPageNotFoundLogs = new HashSet<PageNotFoundLog>();
            DeletedByPageNotFoundLogs = new HashSet<PageNotFoundLog>();

            CreatedByUrlRecords = new HashSet<UrlRecord>();
            UpdatedByUrlRecords = new HashSet<UrlRecord>();
            DeletedByUrlRecords = new HashSet<UrlRecord>();

            CreatedByErrorLogs = new HashSet<ErrorLog>();
            UpdatedByErrorLogs = new HashSet<ErrorLog>();
            DeletedByErrorLogs = new HashSet<ErrorLog>();
        }
    }
}