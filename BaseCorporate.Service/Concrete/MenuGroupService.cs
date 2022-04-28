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
    public class MenuGroupService : IMenuGroupService
    {
        public List<MenuGroupModel> GetList()
        {
            using var context = new EfContext();
            var list = context.MenuGroups.OrderByDescending(x => x.CreatedAt).Select(x => new MenuGroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Key = x.Key,
                ItemCount = x.Items.Count,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().ToList();
            return list;
        }

        public MenuGroupModel AddOrUpdate(MenuGroupModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var menuGroup = model.Id > 0 ? context.MenuGroups.FirstOrDefault(x => x.Id == model.Id) : new MenuGroup();
            if (menuGroup == null)
            {
                return null;
            }

            menuGroup.Name = model.Name;
            menuGroup.Key = model.Key;

            if (model.Id > 0)
            {
                menuGroup.UpdatedAt = DateTime.Now;
                menuGroup.UpdatedByUserId = model.SessionUserId;
            }
            else
            {
                menuGroup.CreatedByUserId = model.SessionUserId;
                context.MenuGroups.Add(menuGroup);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();

            model.Id = menuGroup.Id;
            return model;
        }

        public MenuGroupModel Get(int id)
        {
            using var context = new EfContext();
            var menuGroup = context.MenuGroups.Select(x => new MenuGroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Key = x.Key
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return menuGroup;
        }

        public MenuGroupUIModel Get(string key)
        {
            using var context = new EfContext();
            var model = context.MenuGroups.AsNoTracking().Where(x => x.Key == key).Select(x => new MenuGroupUIModel
            {
                Name = x.Name,
                Key = x.Key,
                Items = x.Items.OrderBy(menuItem => menuItem.OrderBy).Select(i => new MenuItemUIModel
                {
                    Name = i.Name,
                    Link = i.Link,
                    External = i.External,
                    SubItems = i.SubItems.OrderBy(menuSubItem => menuSubItem.OrderBy).Select(s => new MenuSubItemUIModel
                    {
                        Name = s.Name,
                        Link = s.Link,
                        External = s.External
                    }).ToList()
                }).ToList()
            }).FirstOrDefault();

            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var menuGroup = context.MenuGroups.FirstOrDefault(x => x.Id == model.Id);
            if (menuGroup != null)
            {
                menuGroup.Deleted = true;
                menuGroup.DeletedAt = DateTime.Now;
                menuGroup.DeletedByUserId = model.SessionUserId;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
