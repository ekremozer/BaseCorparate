using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Service.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class RedirectRecordService : IRedirectRecordService
    {
        public List<RedirectRecordModel> GetList()
        {
            using var context = new EfContext();

            var list = context.RedirectRecords.OrderByDescending(x => x.CreatedAt).Select(x => new RedirectRecordModel
            {
                Id = x.Id,
                OldUrl = x.OldUrl,
                NewUrl = x.NewUrl,
                RedirectCount = x.RedirectCount,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().ToList();
            return list;
        }

        public RedirectRecordModel Get(int id)
        {
            using var context = new EfContext();

            var model = context.RedirectRecords.Select(x => new RedirectRecordModel
            {
                Id = x.Id,
                OldUrl = x.OldUrl,
                NewUrl = x.NewUrl,
                RedirectCount = x.RedirectCount,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            return model;
        }

        public RedirectRecordModel Get(string oldUrl)
        {
            using var context = new EfContext();

            var model = context.RedirectRecords.Select(x => new RedirectRecordModel
            {
                Id = x.Id,
                OldUrl = x.OldUrl,
                NewUrl = x.NewUrl,
                RedirectCount = x.RedirectCount,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.OldUrl == oldUrl);
            return model;
        }

        public RedirectRecordModel AddOrUpdate(RedirectRecordModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var redirectRecord = model.Id > 0 ? context.RedirectRecords.FirstOrDefault(x => x.Id == model.Id) : new RedirectRecord();
            if (redirectRecord == null)
            {
                return null;
            }

            redirectRecord.OldUrl = model.OldUrl;
            redirectRecord.NewUrl = model.NewUrl;
            redirectRecord.RedirectCount++;

            if (model.Id > 0)
            {
                if (model.SessionUserId>0)
                {
                    redirectRecord.UpdatedByUserId = model.SessionUserId;
                }
                redirectRecord.UpdatedAt = DateTime.Now;
            }
            else
            {
                if (model.SessionUserId > 0)
                {
                    redirectRecord.CreatedByUserId = model.SessionUserId;
                }
                context.RedirectRecords.Add(redirectRecord);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();

            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var redirectRecord = context.RedirectRecords.FirstOrDefault(x => x.Id == model.Id);
            if (redirectRecord == null) return false;

            redirectRecord.Deleted = true;
            redirectRecord.DeletedByUserId = model.SessionUserId;
            redirectRecord.DeletedAt = DateTime.Now;
            context.SaveChanges();
            return true;

        }
    }
}
