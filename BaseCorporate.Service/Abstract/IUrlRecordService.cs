using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IUrlRecordService
    {
        List<UrlRecordModel> GetList();
        UrlRecordModel Get(int id);
        UrlRecordModel Get(string slug);
        UrlRecordModel AddOrUpdate(UrlRecordModel model);
        List<SiteMapUrl> GetUrlForSiteMap();
    }
}
