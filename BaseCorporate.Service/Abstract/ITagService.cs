using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface ITagService
    {
        TagModel AddOrUpdate(TagModel model);
        PagedList<TagModel> GetList(int page);
        TagModel Get(int id);
        bool Delete(BaseModel model);
    }
}
