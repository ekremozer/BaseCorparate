using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Helper;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class TopicService : ITopicService
    {
        private readonly IUrlRecordService _urlRecordService;

        public TopicService(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }

        public PagedList<TopicModel> GetList(int page)
        {
            using var context = new EfContext();
            var model = new PagedList<TopicModel>();
            var skip = model.GetSkipCount(page);

            model.Items = context.Topics.OrderByDescending(x => x.CreatedAt).Select(x => new TopicModel
            {
                Id = x.Id,
                PageTitle = x.PageTitle,
                Title = x.Title,
                Image = x.Image,
                Body = x.Body,
                MetaDescription = x.MetaDescription,
                MetaKeywords = x.MetaKeywords,
                Slug = x.Slug,
                SiteMapPriority = x.SiteMapPriority
            }).Skip(skip).Take(model.ItemsPerPage).AsNoTracking().ToList();
            return model;
        }

        public TopicModel Get(int id)
        {
            using var context = new EfContext();
            var model = context.Topics.Select(x => new TopicModel
            {
                Id = x.Id,
                PageTitle = x.PageTitle,
                Title = x.Title,
                Image = x.Image,
                Body = x.Body,
                MetaDescription = x.MetaDescription,
                MetaKeywords = x.MetaKeywords,
                Slug = x.Slug,
                SiteMapPriority = x.SiteMapPriority,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return null;
            }
            model.Head = HeadHelper.GetTopicHeadModel(model);
            return model;
        }

        public TopicModel GetWithSlug(string slug)
        {
            using var context = new EfContext();
            var model = context.Topics.Select(x => new TopicModel
            {
                Id = x.Id,
                PageTitle = x.PageTitle,
                Title = x.Title,
                Image = x.Image,
                Body = x.Body,
                MetaDescription = x.MetaDescription,
                MetaKeywords = x.MetaKeywords,
                Slug = x.Slug,
                SiteMapPriority = x.SiteMapPriority,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
            if (model == null)
            {
                return null;
            }
            model.Head = HeadHelper.GetTopicHeadModel(model);
            return model;
        }

        public TopicModel AddOrUpdate(TopicModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var topic = model.Id > 0 ? context.Topics.FirstOrDefault(x => x.Id == model.Id) : new Topic();
            if (topic == null)
            {
                return null;
            }

            topic.PageTitle = model.PageTitle;
            topic.Title = model.Title;
            topic.Image = model.Image;
            topic.Body = model.Body;
            topic.MetaDescription = model.MetaDescription;
            topic.MetaKeywords = model.MetaKeywords;
            topic.Slug = model.Slug;
            topic.SiteMapPriority = model.SiteMapPriority;

            if (model.Id > 0)
            {
                topic.UpdatedByUserId = model.SessionUserId;
                topic.UpdatedAt = DateTime.Now;
            }
            else
            {
                topic.CreatedByUserId = model.SessionUserId;
                context.Topics.Add(topic);
            }
            context.SaveChanges();

            #region UrlRecord
            var urlRecordModel = model.Id > 0 ?
                new UrlRecordModel { Id = (int)topic.UrlRecordId, TopicId = topic.Id, Slug = topic.Slug, SessionUserId = model.SessionUserId } :
                new UrlRecordModel { TopicId = topic.Id, Slug = topic.Slug, SessionUserId = model.SessionUserId };

            urlRecordModel = _urlRecordService.AddOrUpdate(urlRecordModel);
            topic.Slug = urlRecordModel.Slug;
            topic.UrlRecordId = urlRecordModel.Id;
            context.SaveChanges();
            #endregion

            transaction.CommitOrRollback();
            model.Id = topic.Id;
            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var topic = context.Topics.Include(x => x.UrlRecord).FirstOrDefault(x => x.Id == model.Id);
            if (topic == null)
            {
                return false;
            }

            topic.Slug = $"{topic.UrlRecord.Slug}-{topic.Id}";
            topic.Deleted = true;
            topic.DeletedByUserId = model.SessionUserId;
            topic.DeletedAt = DateTime.Now;

            topic.UrlRecord.Slug = $"{topic.UrlRecord.Slug}-{topic.Id}";
            topic.UrlRecord.Deleted = true;
            topic.UrlRecord.DeletedByUserId = model.SessionUserId;
            topic.UrlRecord.DeletedAt = DateTime.Now;

            context.SaveChanges();
            return true;
        }
    }
}
