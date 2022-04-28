using Microsoft.EntityFrameworkCore;
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

namespace BaseCorporate.Service.Concrete
{
    public class ErrorLogService : IErrorLogService
    {
        public bool Add(ErrorLogModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var errorLog = new ErrorLog
            {
                ExceptionType = model.ExceptionType,
                ExceptionMessage = model.ExceptionMessage,
                UserIp = model.UserIp,
                PageUrl = model.PageUrl,
                ReferrerUrl = model.ReferrerUrl
            };
            context.ErrorLogs.Add(errorLog);
            context.SaveChanges();
            return transaction.CommitOrRollback();
        }

        public List<ErrorLogModel> GetList()
        {
            using var context = new EfContext();
            var list = context.ErrorLogs.OrderByDescending(x => x.CreatedAt).Select(x => new ErrorLogModel
            {
                Id=x.Id,
                ExceptionType = x.ExceptionType,
                ExceptionMessage = x.ExceptionMessage,
                UserIp = x.UserIp,
                PageUrl = x.PageUrl,
                ReferrerUrl = x.ReferrerUrl,
                CreatedAt = x.CreatedAt
            }).ToList();

            return list;
        }

        public bool DeleteAll()
        {
            using var context = new EfContext();
            var list = context.ErrorLogs.ToList();
            foreach (var item in list)
            {
                context.ErrorLogs.Remove(item);
            }
            context.SaveChanges();
            return true;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var errorLog = context.ErrorLogs.FirstOrDefault(x => x.Id == model.Id);
            if (errorLog != null)
            {
                context.ErrorLogs.Remove(errorLog);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
