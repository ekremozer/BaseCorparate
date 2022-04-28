using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IPageNotFoundLogService
    {
        PagedList<PageNotFoundLogListItem> GetList(int page);
        PageNotFoundLogListItem AddOrUpdate(PageNotFoundLogListItem model);
        PageNotFoundLogListItem Get(string pageUrl);
        bool DeleteAll();
        bool Delete(BaseModel model);
    }
}
