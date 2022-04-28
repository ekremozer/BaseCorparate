using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface ITopicService
    {
        PagedList<TopicModel> GetList(int page);
        TopicModel Get(int id);
        TopicModel GetWithSlug(string slug);
        TopicModel AddOrUpdate(TopicModel model);
        bool Delete(BaseModel model);
    }
}
