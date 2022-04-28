using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class UrlRecordService : IUrlRecordService
    {
        public List<UrlRecordModel> GetList()
        {
            using var context = new EfContext();
            var list = context.UrlRecords.OrderByDescending(x => x.CreatedAt).AsNoTracking().Select(x => new UrlRecordModel
            {
                Id = x.Id,
                ArticleId = x.ArticleId,
                CategoryId = x.CategoryId,
                TagId = x.TagId,
                TopicId = x.TopicId,
                Slug = x.Slug
            }).ToList();

            return list;
        }
        public UrlRecordModel Get(int id)
        {
            using var context = new EfContext();
            var model = context.UrlRecords.AsNoTracking().Where(x => x.Id == id).Select(x => new UrlRecordModel
            {
                Id = x.Id,
                ArticleId = x.ArticleId,
                CategoryId = x.CategoryId,
                TagId = x.TagId,
                TopicId = x.TopicId,
                Slug = x.Slug
            }).FirstOrDefault();

            return model;
        }
        public UrlRecordModel Get(string slug)
        {
            using var context = new EfContext();
            var model = context.UrlRecords.AsNoTracking().Where(x => x.Slug == slug).Select(x => new UrlRecordModel
            {
                Id = x.Id,
                ArticleId = x.ArticleId,
                CategoryId = x.CategoryId,
                TagId = x.TagId,
                TopicId = x.TopicId,
                Slug = x.Slug
            }).FirstOrDefault();

            return model;
        }

        public UrlRecordModel AddOrUpdate(UrlRecordModel model)
        {
            using var context = new EfContext();
            var urlRecord = model.Id > 0 ? context.UrlRecords.FirstOrDefault(x => x.Id == model.Id) : new UrlRecord();

            if (urlRecord == null)
            {
                return model;
            }
            var oldSlug = urlRecord.Slug;
            var count = 1;
            var newSlug = model.Slug;
            while (true)
            {
                if (context.UrlRecords.Any(x => x.Slug == newSlug && x.Id != urlRecord.Id))
                {
                    count++;
                    newSlug = $"{model.Slug}-{count}";
                    continue;
                }
                break;
            }

            urlRecord.ArticleId = model.ArticleId;
            urlRecord.CategoryId = model.CategoryId;
            urlRecord.TagId = model.TagId;
            urlRecord.TopicId = model.TopicId;
            urlRecord.Slug = newSlug;

            if (model.Id > 0)
            {
                urlRecord.UpdatedAt = DateTime.Now;
                urlRecord.UpdatedByUserId = model.SessionUserId;
            }
            else
            {
                urlRecord.CreatedByUserId = model.SessionUserId;
                context.UrlRecords.Add(urlRecord);
            }
            context.SaveChanges();

            if (model.Id > 0 && !string.IsNullOrEmpty(oldSlug) && oldSlug != urlRecord.Slug)
            {
                #region RedirectRecord
                var redirectRecordModel = new RedirectRecord
                {
                    OldUrl = oldSlug,
                    NewUrl = urlRecord.Slug,
                    RedirectCount = 0,
                    CreatedByUserId = model.SessionUserId
                };
                context.RedirectRecords.Add(redirectRecordModel);
                #endregion

                #region UpdateEntitiy
                if (model.UpdateEntity)
                {
                    if (urlRecord.ArticleId != null)
                    {
                        var article = context.Articles.FirstOrDefault(x => x.Id == urlRecord.ArticleId);
                        if (article != null)
                        {
                            article.Slug = urlRecord.Slug;
                            article.UpdatedAt = DateTime.Now;
                            article.UpdatedByUserId = model.SessionUserId;
                        }
                    }
                    else if (urlRecord.CategoryId != null)
                    {
                        var category = context.Categories.FirstOrDefault(x => x.Id == urlRecord.CategoryId);
                        if (category != null)
                        {
                            category.Slug = urlRecord.Slug;
                            category.UpdatedAt = DateTime.Now;
                            category.UpdatedByUserId = model.SessionUserId;
                        }
                    }
                    else if (urlRecord.TagId != null)
                    {
                        var tag = context.Tags.FirstOrDefault(x => x.Id == urlRecord.TagId);
                        if (tag != null)
                        {
                            tag.Slug = urlRecord.Slug;
                            tag.UpdatedAt = DateTime.Now;
                            tag.UpdatedByUserId = model.SessionUserId;
                        }
                    }
                    else if (urlRecord.TopicId != null)
                    {
                        var topic = context.Topics.FirstOrDefault(x => x.Id == urlRecord.TopicId);
                        if (topic != null)
                        {
                            topic.Slug = urlRecord.Slug;
                            topic.UpdatedAt = DateTime.Now;
                            topic.UpdatedByUserId = model.SessionUserId;
                        }
                    }
                }
                #endregion

                context.SaveChanges();
            }

            #region DeleteNewUrlRedirect
            var currentRedirectRecord = context.RedirectRecords.FirstOrDefault(x => x.OldUrl == urlRecord.Slug);
            if (currentRedirectRecord != null)
            {
                currentRedirectRecord.Deleted = true;
                currentRedirectRecord.DeletedAt = DateTime.Now;
                currentRedirectRecord.DeletedByUserId = model.SessionUserId;
            }
            #endregion

            model.Id = urlRecord.Id;
            return model;
        }

        public List<SiteMapUrl> GetUrlForSiteMap()
        {
            using var context = new EfContext();
            var list = context.UrlRecords.OrderByDescending(x => x.CreatedAt).Select(x => new SiteMapUrl
            {
                Url = x.Slug,
                Modified = x.UpdatedAt ?? x.CreatedAt
            }).ToList();
            return list;
        }
    }
}
