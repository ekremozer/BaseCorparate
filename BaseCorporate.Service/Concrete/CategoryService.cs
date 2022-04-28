using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Service.Helper;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUrlRecordService _urlRecordService;

        public CategoryService(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }

        public CategoryModel AddOrUpdate(CategoryModel model)
        {
            using var context = new EfContext();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            var category = model.Id > 0 ? context.Categories.FirstOrDefault(x => x.Id == model.Id) : new Category();
            if (category == null)
            {
                return null;
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.MetaDescription = model.MetaDescription;
            category.ParentCategoryId = model.ParentCategoryId == 0 ? null : model.ParentCategoryId;
            category.Slug = model.Name.ToUrl();

            if (model.Id > 0)
            {
                category.UpdatedByUserId = model.SessionUserId;
                category.UpdatedAt = DateTime.Now;
            }
            else
            {
                category.CreatedByUserId = model.SessionUserId;
                context.Categories.Add(category);
            }
            context.SaveChanges();

            //category.DisplayName = category.ParentCategoryId == 0 ? category.Name : CategoryHelper.GetDisplayName(category.Id);
            //category.BreadCrumb = CategoryHelper.GetBreadCrumb(category.Id);
            context.SaveChanges();

            #region UrlRecord
            var urlRecordModel = model.Id > 0 ?
                new UrlRecordModel { Id = (int)category.UrlRecordId, CategoryId = category.Id, Slug = category.Slug, SessionUserId = model.SessionUserId } :
                new UrlRecordModel { CategoryId = category.Id, Slug = category.Slug, SessionUserId = model.SessionUserId };
            urlRecordModel = _urlRecordService.AddOrUpdate(urlRecordModel);
            urlRecordModel = _urlRecordService.AddOrUpdate(urlRecordModel);
            category.Slug = urlRecordModel.Slug;
            category.UrlRecordId = urlRecordModel.Id;
            context.SaveChanges();
            #endregion

            transaction.CommitOrRollback();

            return model;
        }

        public bool Delete(BaseModel model)
        {
            using var context = new EfContext();
            var category = context.Categories.Include(x => x.UrlRecord).FirstOrDefault(x => x.Id == model.Id);
            if (category == null) return false;

            category.Deleted = true;
            category.DeletedByUserId = model.SessionUserId;
            category.DeletedAt = DateTime.Now;
            category.Slug = $"{category.UrlRecord.Slug}-{category.Id}";

            category.UrlRecord.Deleted = true;
            category.UrlRecord.DeletedByUserId = model.SessionUserId;
            category.UrlRecord.DeletedAt = DateTime.Now;
            category.UrlRecord.Slug = $"{category.UrlRecord.Slug}-{category.Id}";

            var newCategoryId = category.ParentCategoryId ?? context.Categories.Where(x => x.Deleted == false).Select(x => x.Id).FirstOrDefault();

            if (newCategoryId > 0)
            {
                var articles = context.Articles.Where(x => x.CategoryId == model.Id).ToList();

                foreach (var article in articles)
                {
                    article.CategoryId = newCategoryId;
                    article.UpdatedAt = DateTime.Now;
                    article.UpdatedByUserId = model.SessionUserId;
                }
            }

            context.SaveChanges();
            return true;

        }

        public List<CategoryModel> GetList()
        {
            using var context = new EfContext();
            var result = context.Categories.OrderByDescending(x => x.CreatedAt).Include(x => x.CreatedByUser).Include(x => x.UpdatedByUser).AsNoTracking().Select(x => new CategoryModel
            {
                Id = x.Id,
                DisplayName = x.ParentCategoryId == 0 ? x.Name : CategoryHelper.GetDisplayName(x.Id),
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedByUser != null ? x.CreatedByUser.Name : string.Empty,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedByUser != null ? x.UpdatedByUser.Name : string.Empty
            }).ToList();
            return result;
        }

        public CategoryModel Get(int id)
        {
            using var context = new EfContext();
            var category = context.Categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                ParentCategoryId = x.ParentCategoryId,
                ParentCategories = CategoryHelper.GetParentCategoriesList(true, x.Id),
                Slug = x.Slug,
                BreadCrumb = x.BreadCrumb
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            return category;
        }

        public CategoryModel AddModel()
        {
            var categoryModel = new CategoryModel
            {
                ParentCategories = CategoryHelper.GetParentCategoriesList(true)
            };
            return categoryModel;
        }

        public List<CategoryThreeModel> GetCategoriesThree(int categoryId)
        {
            return CategoryHelper.GetCategoriesThree(categoryId);
        }
    }
}
