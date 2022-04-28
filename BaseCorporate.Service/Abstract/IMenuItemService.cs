using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IMenuItemService
    {
        List<MenuItemModel> GetList(int groupId = 0);
        MenuItemModel AddOrUpdate(MenuItemModel model);
        MenuItemModel Get(int id);
        bool Delete(BaseModel model);
        MenuItemModel AddModel(int id);
    }
}
