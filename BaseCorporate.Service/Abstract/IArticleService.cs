using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Model;

namespace BaseCorporate.Service.Abstract
{
    public interface IArticleService
    {
        ArticleModel AddModel();

        ArticleModel AddOrUpdate(ArticleModel model);

        PagedList<ArticleAdminListItem> GetList(int page, string title, int? categoryId, bool isActive = true);
        List<ArticleListItem> GetListForRss();
        bool Delete(BaseModel model);
        ArticleModel UpdateModel(int id);
        List<ArticleListItem> GetLastAdded(int count);
        List<ArticleListItem> GetByCategoryForDetail(int? categoryId, int articleId, int count);
        ArticleListView GetListByHomePage(int page);
        ArticleListView GetListByCategory(int categoryId, int page);
        ArticleListView GetListByTag(int categoryId, int page);
        ArticleListView GetListBySearch(string q, int categoryId, int page);
        ArticleViewModel GetDetail(int id);
        List<ArchivePeriodModel> GetArchivePeriods();
        ArticleListView GeArchiveList(int year, int month, int page);
    }
}
