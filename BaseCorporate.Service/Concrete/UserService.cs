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
    public class UserService : IUserService
    {
        public UserModel Get(string email, string password)
        {
            using var context = new EfContext();
            var user = context.Users.OrderByDescending(x => x.CreatedAt).Select(x => new UserModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Avatar = x.Avatar,
                Password = x.Password
            }).AsNoTracking().FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public UserModel Get(int id)
        {
            using var context = new EfContext();
            var user = context.Users.Select(x => new UserModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                IsActive = x.IsActive,
                Avatar = x.Avatar,
                Password = x.Password
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            return user;
        }

        public List<UserListItem> GetList()
        {
            using var context = new EfContext();
            var userList = context.Users.Select(x => new UserListItem
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Avatar = x.Avatar
            }).AsNoTracking().ToList();
            return userList;
        }

        public UserModel AddOrUpdate(UserModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var user = model.Id > 0 ? context.Users.FirstOrDefault(x => x.Id == model.Id) : new User();
            if (user == null)
            {
                return null;
            }

            var avatar = string.IsNullOrEmpty(model.Avatar)
                ? string.IsNullOrEmpty(user.Avatar) ? "default.jpg" : user.Avatar
                : model.Avatar;

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.Avatar = avatar;
            user.Password = !string.IsNullOrEmpty(model.Password) ? model.Password : user.Password;

            if (model.Id > 0)
            {
                user.UpdatedByUserId = model.SessionUserId;
                user.UpdatedAt = DateTime.Now;
            }
            else
            {
                user.CreatedByUserId = model.SessionUserId;
                context.Users.Add(user);
            }
            context.SaveChanges();
            transaction.CommitOrRollback();

            model.Avatar = !string.IsNullOrEmpty(model.Avatar) ? model.Avatar : user.Avatar;
            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
            if (user != null)
            {
                user.Deleted = true;
                user.DeletedAt = DateTime.Now;
                user.DeletedByUserId = model.SessionUserId;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
