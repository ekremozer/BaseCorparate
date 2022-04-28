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
    public class MenuSubItemService : IMenuSubItemService
    {
        public List<MenuSubItemModel> GetList(int itemId = 0)
        {
            using var context = new EfContext();
            var list = context.MenuSubItems.OrderByDescending(x => x.CreatedAt).Where(x => itemId == 0 || x.ItemId == itemId).Select(x => new MenuSubItemModel
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                ItemId = x.ItemId,
                ItemName = x.Item != null ? x.Item.Name : string.Empty,
                External = x.External,
                OrderBy = x.OrderBy,
                CreatedAt = x.CreatedAt
            }).AsNoTracking().ToList();
            return list;
        }

        public MenuSubItemModel AddOrUpdate(MenuSubItemModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var menuSubItem = model.Id > 0 ? context.MenuSubItems.FirstOrDefault(x => x.Id == model.Id) : new MenuSubItem();
            if (menuSubItem == null)
            {
                return null;
            }

            menuSubItem.Name = model.Name;
            menuSubItem.Link = model.Link == "#" ? model.Link : model.Link.ToUrl();
            menuSubItem.External = model.External;
            menuSubItem.OrderBy = model.OrderBy;
            menuSubItem.ItemId = model.ItemId;

            if (model.Id > 0)
            {
                menuSubItem.UpdatedAt = DateTime.Now;
                menuSubItem.UpdatedByUserId = model.SessionUserId;
            }
            else
            {
                menuSubItem.CreatedByUserId = model.SessionUserId;
                context.MenuSubItems.Add(menuSubItem);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();

            model.Id = menuSubItem.Id;
            return model;
        }

        public MenuSubItemModel Get(int id)
        {
            using var context = new EfContext();
            var muItems = context.MenuItems.AsNoTracking().Select(x => new ListItem { Id = x.Id, Value = x.Name }).ToList();
            var menuItem = context.MenuSubItems.Select(x => new MenuSubItemModel
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                External = x.External,
                OrderBy = x.OrderBy,
                ItemId = x.ItemId,
                GroupKey = x.Item.Group != null ? x.Item.Group.Key : string.Empty,
                CreatedAt = x.CreatedAt,
                Items = muItems
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return menuItem;
        }

        public MenuSubItemModel AddModel(int id)
        {
            using var context = new EfContext();
            var muItems = context.MenuItems.AsNoTracking().Select(x => new ListItem { Id = x.Id, Value = x.Name }).ToList();
            var menuSubItem = new MenuSubItemModel
            {
                ItemId = id,
                Items = muItems
            };

            return menuSubItem;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var menuItem = context.MenuSubItems.FirstOrDefault(x => x.Id == model.SessionUserId);
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
