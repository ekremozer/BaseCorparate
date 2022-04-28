using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface ISettingService
    {
        List<SettingModel> GetList(string groupName = null);
        SettingModel AddOrUpdate(SettingModel model);
        SettingModel Get(int id);
        bool Delete(BaseModel model);
    }
}
