using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IMenuGroupService
    {
        List<MenuGroupModel> GetList();
        MenuGroupModel AddOrUpdate(MenuGroupModel model);
        MenuGroupModel Get(int id);
        MenuGroupUIModel Get(string key);
        bool Delete(BaseModel model);
    }
}
