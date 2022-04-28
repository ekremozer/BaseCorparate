using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Helper;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IUrlRecordService _urlRecordService;

        public ArticleService(ICategoryService categoryService, ITagService tagService, IUrlRecordService urlRecordService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _urlRecordService = urlRecordService;
        }

        public ArticleModel AddModel()
        {
            using var context = new EfContext();
            var articleModel = new ArticleModel
            {
                Categories = CategoryHelper.GetSelectList(),
            };
            return articleModel;
        }

        public ArticleModel AddOrUpdate(ArticleModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var article = model.Id > 0 ? context.Articles.Include(x => x.TagMappings).FirstOrDefault(x => x.Id == model.Id) : new Article();
            if (article == null)
            {
                return null;
            }

            #region AddOrUpdate
            article.PageTitle = model.PageTitle;
            article.Title = model.Title;
            article.Image = !string.IsNullOrEmpty(model.Image) ? model.Image : article.Image;
            article.Body = model.Body;
            article.MetaDescription = model.MetaDescription;
            article.MetaKeywords = model.MetaKeywords;
            article.CategoryId = model.CategoryId;
            article.AllowComment = model.AllowComment == true;
            article.IsActive = model.IsActive;
            article.Slug = model.Slug.ToUrl();
            article.SiteMapPriority = model.SiteMapPriority;

            if (model.Id > 0)
            {
                article.UpdatedByUserId = model.SessionUserId;
                article.UpdatedAt = DateTime.Now;
            }
            else
            {
                article.CreatedByUserId = model.SessionUserId;
                context.Articles.Add(article);
            }

            context.SaveChanges();
            #endregion

            #region AddOrRemoveTags
            if (!string.IsNullOrEmpty(model.Tags))
            {
                var currentTagIdList = new List<int>();
                var tags = model.Tags.Contains(',') ? model.Tags.Split(',').ToList() : new List<string> { model.Tags };
                var articleTags = context.Tags.Where(x => tags.Contains(x.Name)).ToList();

                foreach (var tag in tags)
                {
                    var articleTag = articleTags.FirstOrDefault(x => x.Name == tag);
                    int tagId;
                    if (articleTag == null)
                    {
                        #region AddNewTag
                        var newArticleTag = new Tag
                        {
                            Name = tag,
                            Slug = tag.ToUrl(),
                            CreatedByUserId = model.SessionUserId
                        };

                        context.Tags.Add(newArticleTag);
                        context.SaveChanges();
                        #endregion

                        #region UrlRecord
                        var tagUrlRecordModel = new UrlRecordModel { TagId = newArticleTag.Id, Slug = newArticleTag.Slug, SessionUserId = model.SessionUserId };
                        tagUrlRecordModel = _urlRecordService.AddOrUpdate(tagUrlRecordModel);
                        newArticleTag.Slug = tagUrlRecordModel.Slug;
                        newArticleTag.UrlRecordId = tagUrlRecordModel.Id;
                        context.SaveChanges();
                        #endregion

                        tagId = newArticleTag.Id;
                    }
                    else
                    {
                        tagId = articleTag.Id;
                    }

                    #region AddNewTagMapping
                    if (article.TagMappings.Any(x => x.TagId == tagId) == false)
                    {
                        var tagMapping = new TagMapping
                        {
                            TagId = tagId,
                            ArticleId = article.Id,
                            CreatedByUserId = model.SessionUserId
                        };
                        context.TagMappings.Add(tagMapping);
                    }
                    #endregion

                    currentTagIdList.Add(tagId);
                    context.SaveChanges();
                }

                #region RemoveOldTagMappings
                foreach (var tagMapping in article.TagMappings)
                {
                    if (currentTagIdList.Any(x => x == tagMapping.TagId) == false)
                    {
                        tagMapping.Deleted = true;
                        tagMapping.DeletedAt = DateTime.Now;
                        tagMapping.DeletedByUserId = model.SessionUserId;
                    }
                }
                #endregion
            }
            else
            {
                #region RemoveAllTagMappings
                foreach (var tagMapping in article.TagMappings)
                {
                    tagMapping.Deleted = true;
                    tagMapping.DeletedAt = DateTime.Now;
                    tagMapping.DeletedByUserId = model.SessionUserId;
                }
                context.SaveChanges();
                #endregion
            }
            #endregion

            #region UrlRecord
            var urlRecordModel = model.Id > 0 ?
                new UrlRecordModel { Id = (int)article.UrlRecordId, ArticleId = article.Id, Slug = article.Slug, SessionUserId = model.SessionUserId } :
                new UrlRecordModel { ArticleId = article.Id, Slug = article.Slug, SessionUserId = model.SessionUserId };
            urlRecordModel = _urlRecordService.AddOrUpdate(urlRecordModel);
            article.Slug = urlRecordModel.Slug;
            article.UrlRecordId = urlRecordModel.Id;
            context.SaveChanges();
            #endregion

            transaction.CommitOrRollback();

            model.Id = article.Id;
            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var article = context.Articles.Include(x => x.UrlRecord).Include(x => x.TagMappings).FirstOrDefault(x => x.Id == model.Id);
            if (article != null)
            {
                article.Slug = $"{article.UrlRecord.Slug}-{article.Id}";
                article.Deleted = true;
                article.DeletedAt = DateTime.Now;
                article.DeletedByUserId = model.SessionUserId;

                article.UrlRecord.Slug = $"{article.UrlRecord.Slug}-{article.Id}";
                article.UrlRecord.Deleted = true;
                article.UrlRecord.DeletedAt = DateTime.Now;
                article.UrlRecord.DeletedByUserId = model.SessionUserId;

                foreach (var tagMapping in article.TagMappings)
                {
                    tagMapping.Deleted = true;
                    tagMapping.DeletedAt = DateTime.Now;
                    tagMapping.DeletedByUserId = model.SessionUserId;
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public ArticleModel UpdateModel(int id)
        {
            using var context = new EfContext();
            var article = context.Articles
                .Include(x => x.TagMappings).ThenInclude(x => x.Tag).AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
            if (article != null)
            {
                var model = new ArticleModel
                {
                    Id = article.Id,
                    Uid = article.Uid,
                    PageTitle = article.PageTitle,
                    Title = article.Title,
                    Image = article.Image,
                    Body = article.Body,
                    MetaDescription = article.MetaDescription,
                    MetaKeywords = article.MetaKeywords,
                    CategoryId = article.CategoryId,
                    AllowComment = article.AllowComment,
                    IsActive = article.IsActive,
                    Slug = article.Slug,
                    SiteMapPriority = article.SiteMapPriority,
                    Categories = CategoryHelper.GetSelectList(),
                    Tags = string.Join(',', article.TagMappings.Select(x => x.Tag?.Name).ToList())
                };
                return model;
            }

            return null;
        }
        public PagedList<ArticleAdminListItem> GetList(int page, string title, int? categoryId, bool isActive = true)
        {
            var model = new PagedList<ArticleAdminListItem> { Items = new List<ArticleAdminListItem>() };
            using var context = new EfContext();
            var query = context.Articles.Where(x => (categoryId == null || x.CategoryId == categoryId) && x.IsActive == isActive);
            model.TotalCount = query.Count();

            var skip = model.GetSkipCount(page);

            model.Items = query.Select(x => new ArticleAdminListItem
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category != null
                    ? x.Category.ParentCategoryId == 0 ? x.Category.Name :
                    CategoryHelper.GetDisplayName((int)x.CategoryId)
                    : string.Empty,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).OrderByDescending(x => x.CreatedAt).Skip(skip).Take(model.ItemsPerPage).AsNoTracking().ToList();

            return model;
        }

        public List<ArticleListItem> GetListForRss()
        {
            using var context = new EfContext();
            var result = context.Articles.Select(x => new ArticleListItem
            {
                Title = x.Title,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt
            }).Where(x => x.IsActive).OrderByDescending(x => x.CreatedAt).AsNoTracking().ToList();
            return result;
        }

        public List<ArticleListItem> GetLastAdded(int count)
        {
            using var context = new EfContext();
            var result = context.Articles.OrderByDescending(x => x.CreatedAt).Select(x => new ArticleListItem
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt,
                Image = x.Image,
                Slug = x.Slug
            }).AsNoTracking().Take(count).ToList();
            return result;
        }

        public List<ArticleListItem> GetByCategoryForDetail(int? categoryId, int articleId, int count)
        {
            using var context = new EfContext();
            var result = context.Articles.Where(x => x.CategoryId == categoryId && x.Id != articleId && x.IsActive).OrderByDescending(x => Guid.NewGuid()).Select(x => new ArticleListItem
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt,
                Image = x.Image,
                Slug = x.Slug
            }).AsNoTracking().Take(count).ToList();
            return result;
        }

        public ArticleListView GetListByHomePage(int page)
        {
            using var context = new EfContext();
            var totalCount = context.Articles.Count();
            var pagingInfo = GetPagingInfo(page, totalCount);
            var skipItems = page == 0 ? 0 : (page - 1) * pagingInfo.ItemsPerPage;
            var list = context.Articles.Where(x => x.IsActive).AsNoTracking().OrderByDescending(x => x.CreatedAt).Select(x => new ArticleListViewItem
            {
                Title = x.Title,
                Slug = x.Slug,
                Image = x.Image,
                Category = x.Category != null ? x.Category.Name : string.Empty,
                CategoryUrl = x.Category != null ? x.Category.Slug : string.Empty,
                Summary = x.Body.BodySummaryWithLength(175),
                CreatedAt = x.CreatedAt
            }).Skip(skipItems).Take(pagingInfo.ItemsPerPage).ToList();

            var model = new ArticleListView
            {
                ArticleList = list,
                PagingInfo = pagingInfo,
                HeadModel = HeadHelper.GetDefaultHeadModel(page),
                PageTitle = "",
                Slug = ""
            };

            return model;
        }

        public ArticleListView GetListByCategory(int categoryId, int page)
        {
            using var context = new EfContext();
            var totalCount = context.Articles.Count(x => x.CategoryId == categoryId);
            var pagingInfo = GetPagingInfo(page, totalCount);
            var skipItems = page == 0 ? 0 : (page - 1) * pagingInfo.ItemsPerPage;
            var categoryModel = _categoryService.Get(categoryId);
            var childCategories = CategoryHelper.GetChildCategoryId(categoryId);
            childCategories.Add(categoryId);
            var list = context.Articles.AsNoTracking().OrderByDescending(x => x.CreatedAt).Where(x => childCategories.Contains((int)x.CategoryId) && x.IsActive).Select(x => new ArticleListViewItem
            {
                Title = x.Title,
                Slug = x.Slug,
                Image = x.Image,
                Summary = x.Body.BodySummaryWithLength(175),
                CreatedAt = x.CreatedAt,
                Category = categoryModel.Name,
                CategoryUrl = categoryModel.Slug
            }).Skip(skipItems).Take(pagingInfo.ItemsPerPage).ToList();

            var model = new ArticleListView
            {
                ArticleList = list,
                PagingInfo = pagingInfo,
                HeadModel = HeadHelper.GetCategoryListHeadModel(categoryModel, page),
                BreadCrumb = CategoryHelper.GetBreadCrumb(categoryId),
                PageTitle = categoryModel.Name,
                Slug = categoryModel.Slug,
                CategoryId = categoryModel.Id,
                CategoryLink = false
            };

            return model;
        }

        public ArticleListView GetListByTag(int tagId, int page)
        {
            using var context = new EfContext();
            var totalCount = context.TagMappings.Count(x => x.TagId == tagId);
            page = page == 0 ? 1 : page;
            var pagingInfo = GetPagingInfo(page, totalCount);
            var skipItems = (page - 1) * pagingInfo.ItemsPerPage;
            var tagModel = _tagService.Get(tagId);
            var list = context.TagMappings.AsNoTracking().OrderByDescending(x => x.Article.CreatedAt).Where(x => x.TagId == tagId && x.IsActive).Select(x => new ArticleListViewItem
            {
                Title = x.Article.Title,
                Slug = x.Article.Slug,
                Image = x.Article.Image,
                Summary = x.Article.Body.BodySummaryWithLength(175),
                Category = x.Article.Category != null ? x.Article.Category.Name : string.Empty,
                CategoryUrl = x.Article.Category != null ? x.Article.Category.Slug : string.Empty,
                CreatedAt = x.Article.CreatedAt
            }).Skip(skipItems).Take(pagingInfo.ItemsPerPage).ToList();

            var model = new ArticleListView
            {
                ArticleList = list,
                PagingInfo = pagingInfo,
                HeadModel = HeadHelper.GetTagListHeadModel(tagModel, page),
                BreadCrumb = CategoryHelper.GetBreadCrumbListElement(tagModel.Slug, tagModel.Name, "2"),
                PageTitle = tagModel.Name,
                Slug = tagModel.Slug,
                CategoryId = 0,
            };

            return model;
        }

        public ArticleListView GetListBySearch(string q, int categoryId, int page)
        {
            using var context = new EfContext();

            var childCategories = categoryId > 0 ? CategoryHelper.GetChildCategoryId(categoryId) : new List<int>();
            if (categoryId > 0)
            {
                childCategories.Add(categoryId);
            }

            var query = context.Articles.AsNoTracking().Where(x =>
                x.Title.Contains(q)
                || x.PageTitle.Contains(q)
                || x.Body.Contains(q)
                || x.MetaDescription.Contains(q)
                || x.MetaKeywords.Contains(q)
                || x.TagMappings.Any(y => y.Tag.Name.Contains(q))
                && (categoryId > 0 && childCategories.Contains((int)x.CategoryId) || categoryId == 0) && x.IsActive).Select(x =>
               new ArticleListViewItem
               {
                   Title = x.Title,
                   Slug = x.Slug,
                   Image = x.Image,
                   Summary = x.Body.BodySummaryWithLength(175),
                   Category = x.Category != null ? x.Category.Name : string.Empty,
                   CategoryUrl = x.Category != null ? x.Category.Slug : string.Empty,
                   CreatedAt = x.CreatedAt
               }).OrderByDescending(x => x.CreatedAt);

            var totalCount = query.Count();
            page = page == 0 ? 1 : page;
            var pagingInfo = GetPagingInfo(page, totalCount);
            var skipItems = (page - 1) * pagingInfo.ItemsPerPage;

            var list = query.Skip(skipItems).Take(pagingInfo.ItemsPerPage).ToList();

            var slug = categoryId == 0 ? $"/ara?q={q}" : $"/ara?q={q}&c={categoryId}";

            var model = new ArticleListView
            {
                ArticleList = list,
                PagingInfo = pagingInfo,
                HeadModel = HeadHelper.GetSearchListHeadModel(q, page),
                BreadCrumb = CategoryHelper.GetBreadCrumbListElement(slug, q, "2"),
                PageTitle = q,
                Slug = slug,
                CategoryId = 0
            };

            return model;
        }

        public ArticleViewModel GetDetail(int id)
        {
            using var context = new EfContext();
            var article = context.Articles.AsNoTracking().Where(x => x.Id == id && x.IsActive).Select(x => new ArticleViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Title = x.Title,
                PageTitle = x.PageTitle,
                Body = x.Body,
                Slug = x.Slug,
                Image = x.Image,
                BreadCrumb = CategoryHelper.GetBreadCrumb((int)x.CategoryId),
                CreatedAt = x.CreatedAt
            }).FirstOrDefault();
            if (article != null)
            {
                article.HeadModel = HeadHelper.GetArticleDetailHeadModel(article);
                article.BreadCrumb = CategoryHelper.GetBreadCrumbByArticle(article.BreadCrumb, article);
            }
            return article;
        }

        public PagingInfo GetPagingInfo(int page, int totalItem)
        {
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 12,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / 12)
            };
            return pagingInfo;
        }
        public List<ArchivePeriodModel> GetArchivePeriods()
        {
            using var context = new EfContext();
            var archivePeriods = context.Articles.Where(x => x.IsActive).OrderByDescending(x => x.CreatedAt.Year).ThenByDescending(x => x.CreatedAt.Month).GroupBy(x => new
            {
                x.CreatedAt.Year,
                x.CreatedAt.Month
            }).Select(x => new ArchivePeriodModel
            {
                DisplayName = new DateTime(x.Key.Year, x.Key.Month, 1).ToString("MMMM yyyy", new CultureInfo("tr-TR")),
                Count = x.Count(),
                Year = x.Key.Year,
                Month = x.Key.Month
            }).ToList();

            return archivePeriods;
        }

        public ArticleListView GeArchiveList(int year, int month, int page)
        {
            using var context = new EfContext();
            var totalCount = context.Articles.Count(x => x.CreatedAt.Year == year && x.CreatedAt.Month == month);
            var pagingInfo = GetPagingInfo(page, totalCount);
            var skipItems = page == 0 ? 0 : (page - 1) * pagingInfo.ItemsPerPage;
            var slug = $"/arsiv/{year}/{month}";
            var pageTitle = $"{new DateTime(year, month, 1).ToString("MMMM yyyy", new CultureInfo("tr-TR"))}";

            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);

            var list = context.Articles.AsNoTracking().OrderByDescending(x => x.CreatedAt).Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate && x.IsActive).Select(x => new ArticleListViewItem
            {
                Title = x.Title,
                Slug = x.Slug,
                Image = x.Image,
                Summary = x.Body.BodySummaryWithLength(175),
                Category = x.Category != null ? x.Category.Name : string.Empty,
                CategoryUrl = x.Category != null ? x.Category.Slug : string.Empty,
                CreatedAt = x.CreatedAt
            }).Skip(skipItems).Take(pagingInfo.ItemsPerPage).ToList();

            var breadCrumb = CategoryHelper.GetBreadCrumbListElement("/arsiv", "Arşiv", "2");
            breadCrumb += CategoryHelper.GetBreadCrumbListElement(slug, pageTitle, "3");

            var model = new ArticleListView
            {
                ArticleList = list,
                PagingInfo = pagingInfo,
                HeadModel = HeadHelper.GetCustomHeadModel(pageTitle, slug),
                BreadCrumb = breadCrumb,
                PageTitle = pageTitle,
                Slug = slug,
                CategoryId = 1
            };

            return model;
        }
    }
}
