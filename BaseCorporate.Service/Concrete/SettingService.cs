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
    public class SettingService : ISettingService
    {
        public List<SettingModel> GetList(string groupName = null)
        {
            using var context = new EfContext();
            var list = context.Settings.OrderByDescending(x => x.CreatedAt).Select(x => new SettingModel
            {
                Id = x.Id,
                Key = x.Key,
                Value = x.Value,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().Where(x => string.IsNullOrEmpty(groupName) || x.GroupName == groupName).ToList();
            return list;
        }

        public SettingModel AddOrUpdate(SettingModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var setting = model.Id > 0 ? context.Settings.FirstOrDefault(x => x.Id == model.Id) : new Setting();
            if (setting == null)
            {
                return null;
            }

            setting.Value = model.Value;
            setting.GroupName = model.GroupName;

            if (model.Id > 0)
            {
                setting.UpdatedAt = DateTime.Now;
                setting.UpdatedByUserId = model.SessionUserId;
            }
            else
            {
                setting.Key = model.Key;
                setting.CreatedByUserId = model.SessionUserId;
                context.Settings.Add(setting);
            }

            context.SaveChanges();
            transaction.CommitOrRollback();

            model.Id = setting.Id;
            return model;
        }

        public SettingModel Get(int id)
        {
            using var context = new EfContext();
            var settingModel = context.Settings.Select(x => new SettingModel
            {
                Id = x.Id,
                Key = x.Key,
                Value = x.Value,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return settingModel;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var setting = context.Settings.FirstOrDefault(x => x.Id == model.Id);
            if (setting != null)
            {
                setting.Deleted = true;
                setting.DeletedAt = DateTime.Now;
                setting.DeletedByUserId = model.SessionUserId;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}