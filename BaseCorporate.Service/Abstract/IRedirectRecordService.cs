using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IRedirectRecordService
    {
        List<RedirectRecordModel> GetList();
        RedirectRecordModel Get(int id);
        RedirectRecordModel Get(string oldUrl);
        RedirectRecordModel AddOrUpdate(RedirectRecordModel model);
        bool Delete(BaseModel model);
    }
}
