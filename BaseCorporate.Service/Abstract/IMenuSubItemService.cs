using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IMenuSubItemService
    {
        List<MenuSubItemModel> GetList(int itemId = 0);
        MenuSubItemModel AddOrUpdate(MenuSubItemModel model);
        MenuSubItemModel Get(int id);
        MenuSubItemModel AddModel(int id);
        bool Delete(BaseModel model);
    }
}
