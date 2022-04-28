using BaseCorporate.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BaseCorporate.Dal.EntityFramework
{
    public class ModelBuilderHelper
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(x => x.Id, "user_id").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(50);
                entity.Property(a => a.Surname).HasMaxLength(50);
                entity.Property(a => a.Email).HasMaxLength(50);
                entity.Property(a => a.Password).HasMaxLength(36);
                entity.Property(a => a.Password).HasMaxLength(50);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByUsers).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByUsers).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByUsers).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(x => x.Id, "category_id").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.Description).HasMaxLength(250);
                entity.Property(a => a.MetaDescription).HasMaxLength(250);
                entity.Property(a => a.Slug).HasMaxLength(100);
                entity.Property(a => a.DisplayName).HasMaxLength(350);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByCategories).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByCategories).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByCategories).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.ParentCategory).WithMany(x => x.ChildCategories).HasForeignKey(x => x.ParentCategoryId);
                entity.HasOne(x => x.UrlRecord).WithOne(x => x.Category).HasForeignKey<UrlRecord>(x => x.CategoryId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasIndex(x => x.Id, "article_id").IsUnique();

                entity.Property(a => a.PageTitle).HasMaxLength(100);
                entity.Property(a => a.Title).HasMaxLength(100);
                entity.Property(a => a.Image).HasMaxLength(100);
                entity.Property(a => a.MetaDescription).HasMaxLength(250);
                entity.Property(a => a.MetaKeywords).HasMaxLength(250);
                entity.Property(a => a.MetaKeywords).HasMaxLength(80);
                entity.Property(a => a.Slug).HasMaxLength(100);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByArticles).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByArticles).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByArticles).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId);
                entity.HasOne(x => x.UrlRecord).WithOne(x => x.Article).HasForeignKey<UrlRecord>(x => x.ArticleId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(x => x.Id, "tag_id").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.Description).HasMaxLength(250);
                entity.Property(a => a.MetaDescription).HasMaxLength(250);
                entity.Property(a => a.MetaDescription).HasMaxLength(100);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByTags).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByTags).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByTags).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.UrlRecord).WithOne(x => x.Tag).HasForeignKey<UrlRecord>(x => x.TagId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<TagMapping>(entity =>
            {
                entity.HasIndex(x => x.Id, "tagMapping_id").IsUnique();
                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByTagMappings).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByTagMappings).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByTagMappings).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.Tag).WithMany(x => x.Mappings).HasForeignKey(x => x.TagId);
                entity.HasOne(x => x.Article).WithMany(x => x.TagMappings).HasForeignKey(x => x.ArticleId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasIndex(x => x.Id, "topic_id").IsUnique();

                entity.Property(a => a.PageTitle).HasMaxLength(100);
                entity.Property(a => a.Title).HasMaxLength(100);
                entity.Property(a => a.Image).HasMaxLength(100);
                entity.Property(a => a.MetaDescription).HasMaxLength(250);
                entity.Property(a => a.MetaKeywords).HasMaxLength(250);
                entity.Property(a => a.Slug).HasMaxLength(100);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByTopics).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByTopics).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByTopics).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.UrlRecord).WithOne(x => x.Topic).HasForeignKey<UrlRecord>(x => x.TopicId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<MenuGroup>(entity =>
            {
                entity.HasIndex(x => x.Id, "menuGroup_id").IsUnique();
                entity.HasIndex(a => a.Key, "key").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.Key).HasMaxLength(50);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByMenuGroups).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByMenuGroups).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByMenuGroups).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasIndex(x => x.Id, "menuItem_id").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.Link).HasMaxLength(100);


                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByMenuItems).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByMenuItems).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByMenuItems).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.Group).WithMany(x => x.Items).HasForeignKey(x => x.GroupId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<MenuSubItem>(entity =>
            {
                entity.HasIndex(x => x.Id, "menuSubItem_id").IsUnique();

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.Link).HasMaxLength(100);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByMenuSubItems).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByMenuSubItems).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByMenuSubItems).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.Item).WithMany(x => x.SubItems).HasForeignKey(x => x.ItemId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasIndex(x => x.Id, "setting_id").IsUnique();
                entity.HasIndex(a => a.Key, "key").IsUnique();

                entity.Property(a => a.Key).HasMaxLength(50);
                entity.Property(a => a.GroupName).HasMaxLength(50);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedBySettings).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedBySettings).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedBySettings).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<RedirectRecord>(entity =>
            {
                entity.HasIndex(x => x.Id, "redirectRecord_id").IsUnique();

                entity.Property(a => a.OldUrl).HasMaxLength(250);
                entity.Property(a => a.NewUrl).HasMaxLength(150);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByRedirectRecords).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByRedirectRecords).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByRedirectRecords).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<PageNotFoundLog>(entity =>
            {
                entity.HasIndex(x => x.Id, "pageNotFoundLog_id").IsUnique();

                entity.Property(a => a.PageUrl).HasMaxLength(250);
                entity.Property(a => a.ReferrerUrl).HasMaxLength(250);
                entity.Property(a => a.UserIp).HasMaxLength(50);
                entity.Property(a => a.UserAgent).HasMaxLength(350);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByPageNotFoundLogs).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByPageNotFoundLogs).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByPageNotFoundLogs).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasIndex(x => x.Id, "errorLog_id").IsUnique();

                entity.Property(a => a.ExceptionType).HasMaxLength(250);
                entity.Property(a => a.ExceptionMessage).HasMaxLength(500);
                entity.Property(a => a.UserIp).HasMaxLength(50);
                entity.Property(a => a.PageUrl).HasMaxLength(250);
                entity.Property(a => a.ReferrerUrl).HasMaxLength(250);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByErrorLogs).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByErrorLogs).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByErrorLogs).HasForeignKey(x => x.DeletedByUserId);
                entity.HasQueryFilter(x => !x.Deleted);
            });

            modelBuilder.Entity<UrlRecord>(entity =>
            {
                entity.HasIndex(x => x.Id, "urlRecord_id").IsUnique();
                entity.HasIndex(a => a.Slug, "slug").IsUnique();

                entity.Property(a => a.Slug).HasMaxLength(100);

                entity.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedByUrlRecords).HasForeignKey(x => x.CreatedByUserId);
                entity.HasOne(x => x.UpdatedByUser).WithMany(x => x.UpdatedByUrlRecords).HasForeignKey(x => x.UpdatedByUserId);
                entity.HasOne(x => x.DeletedByUser).WithMany(x => x.DeletedByUrlRecords).HasForeignKey(x => x.DeletedByUserId);
                entity.HasOne(x => x.Category).WithOne(x => x.UrlRecord).HasForeignKey<Category>(x => x.UrlRecordId);
                entity.HasOne(x => x.Tag).WithOne(x => x.UrlRecord).HasForeignKey<Tag>(x => x.UrlRecordId);
                entity.HasOne(x => x.Article).WithOne(x => x.UrlRecord).HasForeignKey<Article>(x => x.UrlRecordId);
                entity.HasOne(x => x.Topic).WithOne(x => x.UrlRecord).HasForeignKey<Topic>(x => x.UrlRecordId);
                
                entity.HasQueryFilter(x => !x.Deleted);
            });
        }
    }
}
