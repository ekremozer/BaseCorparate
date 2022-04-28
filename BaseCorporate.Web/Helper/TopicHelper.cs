using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Model;
using BaseCorporate.Web.Infrastructure;

namespace BaseCorporate.Web.Helper
{
    public static class TopicHelper
    {
        public static TopicModel GetWithSlug(string slug)
        {
            var topicService = ServiceLocator.ServiceProvider.GetService<ITopicService>();
            var model = topicService.GetWithSlug(slug);
            return model;
        }
    }
}
