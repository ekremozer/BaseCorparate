using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Entities.Entities;

namespace BaseCorporate.Service.Abstract
{
    public interface IUserService
    {
        UserModel Get(string email, string password);
        UserModel Get(int id);
        List<UserListItem> GetList();
        UserModel AddOrUpdate(UserModel model);

        bool Delete(BaseModel model);
    }
}
