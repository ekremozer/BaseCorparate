using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Service.Helper;
using BaseCorporate.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class TagService : ITagService
    {
        private readonly IUrlRecordService _urlRecordService;

        public TagService(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }

        public TagModel AddOrUpdate(TagModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var tag = context.Tags.FirstOrDefault(x => x.Id == model.Id);
            if (tag == null)
            {
                return model;
            }
            tag.Name = model.Name;
            tag.Slug = model.Name.ToUrl();
            tag.UpdatedAt = DateTime.Now;
            tag.UpdatedByUserId = model.SessionUserId;
            context.SaveChanges();

            #region UrlRecord
            var urlRecordModel = model.Id > 0 ?
                new UrlRecordModel { Id = (int)tag.UrlRecordId, TagId = tag.Id, Slug = tag.Slug, SessionUserId = model.SessionUserId } :
                new UrlRecordModel { TagId = tag.Id, Slug = tag.Slug, SessionUserId = model.SessionUserId };
            urlRecordModel = _urlRecordService.AddOrUpdate(urlRecordModel);
            tag.Slug = urlRecordModel.Slug;
            tag.UrlRecordId = urlRecordModel.Id;
            context.SaveChanges();
            #endregion

            transaction.CommitOrRollback();

            return model;
        }

        public PagedList<TagModel> GetList(int page)
        {
            using var context = new EfContext();
            var model = new PagedList<TagModel>();
            var skip = model.GetSkipCount(page);
            model.Items = context.Tags.OrderByDescending(x => x.CreatedAt).Include(x => x.Mappings).Select(x =>
                new TagModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ArticleCount = x.Mappings.Count
                }).Skip(skip).Take(model.ItemsPerPage).AsNoTracking().ToList();

            return model;
        }

        public TagModel Get(int id)
        {
            using var context = new EfContext();
            var tag = context.Tags.Select(x => new TagModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return tag;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var tag = context.Tags.Include(x => x.Mappings).Include(x => x.UrlRecord).FirstOrDefault(x => x.Id == model.Id);
            if (tag == null)
            {
                return false;
            }

            tag.Slug = $"{tag.UrlRecord.Slug}-{tag.Id}";
            tag.Deleted = true;
            tag.DeletedByUserId = model.SessionUserId;
            tag.DeletedAt = DateTime.Now;

            tag.UrlRecord.Slug = $"{tag.UrlRecord.Slug}-{tag.Id}";
            tag.UrlRecord.Deleted = true;
            tag.UrlRecord.DeletedByUserId = model.SessionUserId;
            tag.UrlRecord.DeletedAt = DateTime.Now;

            foreach (var map in tag.Mappings)
            {
                map.Deleted = true;
                map.DeletedByUserId = model.SessionUserId;
                map.DeletedAt = DateTime.Now;
            }
            context.SaveChanges();
            return true;
        }
    }
}
