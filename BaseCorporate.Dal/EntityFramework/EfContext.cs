using BaseCorporate.Entities.Entities;
using BaseCorporate.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Dal.EntityFramework
{
    public class EfContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagMapping> TagMappings { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuSubItem> MenuSubItems { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<RedirectRecord> RedirectRecords { get; set; }
        public DbSet<PageNotFoundLog> PageNotFoundLogs { get; set; }
        public DbSet<UrlRecord> UrlRecords { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SqlServerDbContextOptionsExtensions.UseSqlServer(optionsBuilder, ConnectionString);
            optionsBuilder.UseSqlServer(AppParameter.AppSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderHelper.OnModelCreating(modelBuilder);
        }
    }
}