using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Web.Infrastructure;

namespace BaseCorporate.Web.Controllers
{
    public class TopicController : BaseWebController
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public IActionResult Detail(int id)
        {
            return View(_topicService.Get(id));
        }
    }
}
