using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace BaseCorporate.Web.Infrastructure
{
    public class RouteValueTransformer : DynamicRouteValueTransformer
    {
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            var slug = values["slug"]?.ToString();
            //var page = httpContext.Request.Query["page"].ToString();
            //var hasPageParameter = int.TryParse(values["page"]?.ToString(), out var page);
            var hasPageParameter = int.TryParse(httpContext.Request.Query["page"].ToString(), out var page);
            page = hasPageParameter ? page : 1;
            if (!string.IsNullOrEmpty(slug))
            {
                var urlRecordService = ServiceLocator.ServiceProvider.GetService<IUrlRecordService>();
                var urlRecord = urlRecordService.Get(slug);
                if (urlRecord is { EntityIsActive: true })
                {
                    if (urlRecord.ArticleId != null)
                    {
                        values["controller"] = "article";
                        values["action"] = "detail";
                        values["id"] = urlRecord.ArticleId;
                    }
                    else if (urlRecord.CategoryId != null)
                    {
                        values["controller"] = "article";
                        values["action"] = "category";
                        values["id"] = urlRecord.CategoryId;
                        values["page"] = page;
                    }
                    else if (urlRecord.TagId != null)
                    {
                        values["controller"] = "article";
                        values["action"] = "tag";
                        values["id"] = urlRecord.TagId;
                        values["page"] = page;
                    }
                    else if (urlRecord.TopicId != null)
                    {
                        values["controller"] = "topic";
                        values["action"] = "detail";
                        values["id"] = urlRecord.TopicId;
                    }
                }
            }
            return values;
        }
    }
}
