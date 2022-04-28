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
using BaseCorporate.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class MenuItemService : IMenuItemService
    {
        public List<MenuItemModel> GetList(int groupId = 0)
        {
            using var context = new EfContext();
            var list = context.MenuItems.OrderByDescending(x => x.CreatedAt).Include(x => x.Group).Where(x => groupId == 0 || x.GroupId == groupId).Select(x => new MenuItemModel
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                GroupId = x.GroupId,
                GroupName = x.Group != null ? x.Group.Name : string.Empty,
                External = x.External,
                OrderBy = x.OrderBy,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().ToList();
            return list;
        }

        public MenuItemModel AddOrUpdate(MenuItemModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var menuItem = model.Id > 0 ? context.MenuItems.FirstOrDefault(x => x.Id == model.Id) : new MenuItem();
            if (menuItem == null)
            {
                return null;
            }

            menuItem.Name = model.Name;
            menuItem.Link = model.Link == "#" ? model.Link : $"/{model.Link.ToUrl()}";
            menuItem.External = model.External;
            menuItem.OrderBy = model.OrderBy;
            menuItem.GroupId = model.GroupId;

            if (model.Id > 0)
            {
                menuItem.UpdatedAt = DateTime.Now;
                menuItem.UpdatedByUserId = model.SessionUserId;
            }
            else
            {
                menuItem.CreatedByUserId = model.SessionUserId;
                context.MenuItems.Add(menuItem);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();

            model.Id = menuItem.Id;
            return model;
        }

        public MenuItemModel Get(int id)
        {
            using var context = new EfContext();
            var menuGroups = context.MenuGroups.Select(x => new ListItem { Id = x.Id, Value = x.Name }).ToList();
            var menuItem = context.MenuItems.Select(x => new MenuItemModel
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                External = x.External,
                OrderBy = x.OrderBy,
                GroupId = x.GroupId,
                GroupName = x.Group != null ? x.Group.Name : string.Empty,
                GroupKey = x.Group != null ? x.Group.Key : string.Empty,
                CreatedAt = x.CreatedAt,
                Groups = menuGroups
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return menuItem;
        }

        public MenuItemModel AddModel(int id)
        {
            using var context = new EfContext();
            var menuGroups = context.MenuGroups.Select(x => new ListItem { Id = x.Id, Value = $"{x.Name} ({x.Key})" }).AsNoTracking().ToList();
            var menuItem = new MenuItemModel
            {
                GroupId = id,
                Groups = menuGroups
            };

            return menuItem;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var menuItem = context.MenuItems.FirstOrDefault(x => x.Id == model.Id);
            if (menuItem != null)
            {
                menuItem.Deleted = true;
                menuItem.DeletedAt = DateTime.Now;
                menuItem.DeletedByUserId = model.SessionUserId;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
