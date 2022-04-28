using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IErrorLogService
    {
        List<ErrorLogModel> GetList();
        bool Add(ErrorLogModel model);
        bool DeleteAll();
        bool Delete(BaseModel model);
    }
}
