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
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class PageNotFoundLogService : IPageNotFoundLogService
    {
        public PagedList<PageNotFoundLogListItem> GetList(int page)
        {
            var model = new PagedList<PageNotFoundLogListItem> { Items = new List<PageNotFoundLogListItem>() };
            using var context = new EfContext();
            var query = context.PageNotFoundLogs;

            model.TotalCount = query.Count();

            var skip = model.GetSkipCount(page);

            model.Items = query.OrderByDescending(x => x.CreatedAt).Select(x => new PageNotFoundLogListItem
            {
                Id = x.Id,
                PageUrl = x.PageUrl,
                ReferrerUrl = x.ReferrerUrl,
                UserIp = x.UserIp,
                UserAgent = x.UserAgent,
                Count = x.Count,
                CreatedAt = x.CreatedAt
            }).Skip(skip).Take(model.ItemsPerPage).AsNoTracking().ToList();

            return model;
        }

        public PageNotFoundLogListItem AddOrUpdate(PageNotFoundLogListItem model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var pageNotFoundLog = model.Id > 0 ? context.PageNotFoundLogs.FirstOrDefault(x => x.Id == model.Id) : new PageNotFoundLog();

            if (pageNotFoundLog == null) return model;

            pageNotFoundLog.PageUrl = model.PageUrl;
            pageNotFoundLog.ReferrerUrl = model.ReferrerUrl;
            pageNotFoundLog.UserIp = model.UserIp;
            pageNotFoundLog.UserAgent = model.UserAgent;

            if (model.Id > 0)
            {
                pageNotFoundLog.Count++;
                pageNotFoundLog.UpdatedAt = DateTime.Now;
            }
            else
            {
                pageNotFoundLog.Count = 1;
                context.PageNotFoundLogs.Add(pageNotFoundLog);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();
            model.Id = pageNotFoundLog.Id;

            return model;
        }

        public PageNotFoundLogListItem Get(string pageUrl)
        {
            using var context = new EfContext();
            var model = context.PageNotFoundLogs.Select(x => new PageNotFoundLogListItem
            {
                Id = x.Id,
                PageUrl = x.PageUrl,
                ReferrerUrl = x.ReferrerUrl,
                UserIp = x.UserIp,
                UserAgent = x.UserAgent,
                Count = x.Count,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.PageUrl == pageUrl);
            return model;
        }

        public bool DeleteAll()
        {
            using var context = new EfContext();
            var list = context.PageNotFoundLogs.ToList();
            foreach (var item in list)
            {
                context.PageNotFoundLogs.Remove(item);
            }
            context.SaveChanges();
            return true;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var pageNotFoundLog = context.PageNotFoundLogs.FirstOrDefault(x => x.Id == model.Id);
            if (pageNotFoundLog != null)
            {
                context.PageNotFoundLogs.Remove(pageNotFoundLog);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
