using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCorporate.Dal.EntityFramework;
using BaseCorporate.Entities.Entities;
using BaseCorporate.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseCorporate.Service.Helper
{
    public static class CategoryHelper
    {
        public static string GetDisplayName(int categoryId)
        {
            using var context = new EfContext();

            var allCategories = context.Categories.AsNoTracking().ToList();
            var category = allCategories.FirstOrDefault(x => x.Id == categoryId);
            var displayName = string.Empty;

            if (category == null) return displayName;

            var parentCategoryId = category.ParentCategoryId;
            displayName = category.Name;
            while (true)
            {
                var parentCategory = allCategories.FirstOrDefault(x => x.Id == parentCategoryId);
                if (parentCategory == null) return displayName;
                displayName = $"{parentCategory.Name} >> {displayName}";
                if (parentCategory.ParentCategoryId != null)
                {
                    parentCategoryId = parentCategory.ParentCategoryId;
                    continue;
                }
                break;
            }
            return displayName;
        }

        public static List<ListItem> GetSelectList()
        {
            using var context = new EfContext();
            var list = context.Categories.AsNoTracking().Select(x => new ListItem
            {
                Id = x.Id,
                Value = CategoryHelper.GetDisplayName(x.Id)
            }).ToList();

            return list;
        }

        public static string GetBreadCrumb(int categoryId)
        {
            using var context = new EfContext();
            var allCategories = context.Categories.AsNoTracking().ToList();
            var homePageLink = "/";
            var currentCategory = allCategories.FirstOrDefault(x => x.Id == categoryId);
            if (currentCategory == null) return string.Empty;
            var sbBreadCrumb = new StringBuilder();
            var parentIds = new List<int?>();
            var topParentCategoryId = currentCategory.ParentCategoryId;
            parentIds.Add(topParentCategoryId);
            while (true)
            {
                if (topParentCategoryId == null) break;
                var parentCategory = allCategories.FirstOrDefault(x => x.Id == topParentCategoryId);
                if (parentCategory?.ParentCategoryId == null) break;
                topParentCategoryId = parentCategory.ParentCategoryId;
                parentIds.Add(topParentCategoryId);
            }
            var index = 3;
            if (topParentCategoryId == null)
            {
                var categoryListElement = GetBreadCrumbListElement($"{homePageLink}{currentCategory.Slug}", currentCategory.Name, "2");
                sbBreadCrumb.Append(categoryListElement);
            }
            else
            {
                var topParentCategory = allCategories.FirstOrDefault(x => x.Id == topParentCategoryId);
                if (topParentCategory == null) return sbBreadCrumb.ToString();
                var topParentCategoryListElement = GetBreadCrumbListElement($"{homePageLink}{topParentCategory.Slug}", topParentCategory.Name, "2");
                sbBreadCrumb.Append(topParentCategoryListElement);

                while (true)
                {
                    var category = allCategories.FirstOrDefault(x => x.ParentCategoryId == topParentCategoryId && parentIds.Contains(x.Id));
                    if (category == null) break;
                    var categoryListElement = GetBreadCrumbListElement($"{homePageLink}{currentCategory.Slug}", category.Name, index.ToString());
                    sbBreadCrumb.Append(categoryListElement);
                    topParentCategoryId = category.Id;
                    index++;
                }
            }
            return sbBreadCrumb.ToString();
        }

        public static string GetBreadCrumbListElement(string link, string linkText, string linkIndex)
        {
            var sbBreadCrumbListElement = new StringBuilder();
            sbBreadCrumbListElement.Append("<li itemprop=\"itemListElement\" itemscope=\"\" itemtype=\"http://schema.org/ListItem\">");
            sbBreadCrumbListElement.Append($"<a itemprop=\"item\" href=\"{link}\"><span itemprop=\"name\">{linkText}</span></a>");
            sbBreadCrumbListElement.Append($"<meta itemprop=\"position\" content=\"{linkIndex}\">");
            sbBreadCrumbListElement.Append("</li>");
            return sbBreadCrumbListElement.ToString();
        }

        public static string GetBreadCrumbByArticle(string breadCrumb, ArticleViewModel article)
        {
            var e = breadCrumb.Split(new[] { "</li>" }, StringSplitOptions.None).ToList();
            var linkIndex = breadCrumb.Split(new[] { "</li>" }, StringSplitOptions.None).ToList().Count + 1;
            var sbBreadCrumbListElement = new StringBuilder();
            sbBreadCrumbListElement.Append("<li itemprop=\"itemListElement\" itemscope=\"\" itemtype=\"http://schema.org/ListItem\">");
            sbBreadCrumbListElement.Append($"<a itemprop=\"item\" href=\"{article.Slug}\"><span itemprop=\"name\">{article.Title}</span></a>");
            sbBreadCrumbListElement.Append($"<meta itemprop=\"position\" content=\"{linkIndex}\">");
            sbBreadCrumbListElement.Append("</li>");
            breadCrumb += sbBreadCrumbListElement.ToString();
            return breadCrumb;
        }

        public static List<ListItem> GetParentCategoriesList(bool categoryController, int excludingId = -1)
        {
            using var context = new EfContext();
            var categoryList = new List<ListItem>();
            if (categoryController) categoryList.Add(new ListItem { Id = 0, Value = "Yok" });
            var allCategories = context.Categories.Where(x => x.Id != excludingId).AsNoTracking().ToList();
            foreach (var category in allCategories.Where(category => category != null))
            {
                if (category.ParentCategoryId != null)
                {
                    var parentCategoryItem = new ListItem();
                    ParentCategoryItemBind(allCategories, category.Id, (int)category.ParentCategoryId, parentCategoryItem);
                    if (parentCategoryItem.Value != null)
                    {
                        categoryList.Add(parentCategoryItem);
                    }
                }
                else
                {
                    categoryList.Add(new ListItem { Id = category.Id, Value = CategoryHelper.GetDisplayName(category.Id) });
                }
            }
            return categoryList;
        }

        public static void ParentCategoryItemBind(List<Category> allCategories, int categoryId, int parentCategoryId, ListItem parentItem)
        {
            var category = allCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category == null) return;
            parentItem.Value = category.Name;
            while (true)
            {
                var parentCategory = allCategories.FirstOrDefault(x => x.Id == parentCategoryId);
                if (parentCategory == null) return;
                if (parentCategory.ParentCategoryId == null)
                {
                    parentItem.Value = GetDisplayName(categoryId);
                    parentItem.Id = category.Id;
                }
                else
                {
                    parentItem.Value = GetDisplayName(parentCategoryId);
                    if (parentCategory.ParentCategoryId != null)
                    {
                        parentCategoryId = (int)parentCategory.ParentCategoryId;
                    }
                    continue;
                }
                break;
            }
        }

        public static List<CategoryThreeModel> GetCategoriesThree(int categoryId)
        {
            using var context = new EfContext();
            if (categoryId == 0)
            {
                var categoriesThreeByTag = context.Categories.AsNoTracking().Where(x => x.ParentCategoryId == null).Select(x =>
                    new CategoryThreeModel
                    {
                        Name = x.Name,
                        Slug = x.Slug
                    }).ToList();
                return categoriesThreeByTag;
            }
            var categories = context.Categories.AsNoTracking().Select(x =>
                 new CategoryThreeModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     ParentCategoryId = x.ParentCategoryId
                 }).ToList();

            var categoriesThree = categories.Where(x => x.ParentCategoryId == categoryId).Select(x =>
                 new CategoryThreeModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     ParentCategoryId = x.ParentCategoryId
                 }).ToList();

            #region BindSubCategories
            foreach (var item in categoriesThree)
            {
                item.SubCategories = categories.Where(x => x.ParentCategoryId == item.Id).ToList();

                foreach (var item1 in item.SubCategories)
                {
                    item1.SubCategories = categories.Where(x => x.ParentCategoryId == item1.Id).ToList();

                    foreach (var item2 in item.SubCategories)
                    {
                        item2.SubCategories = categories.Where(x => x.ParentCategoryId == item2.Id).ToList();

                        foreach (var item3 in item.SubCategories)
                        {
                            item3.SubCategories = categories.Where(x => x.ParentCategoryId == item3.Id).ToList();

                            foreach (var item4 in item.SubCategories)
                            {
                                item4.SubCategories = categories.Where(x => x.ParentCategoryId == item4.Id).ToList();

                                foreach (var item5 in item.SubCategories)
                                {
                                    item5.SubCategories = categories.Where(x => x.ParentCategoryId == item5.Id).ToList();
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            if (categoriesThree.Count == 0)
            {
                var currentParentCategoryId = categories.FirstOrDefault(x => x.Id == categoryId)?.ParentCategoryId;
                categoriesThree = categories.Where(x => x.ParentCategoryId == currentParentCategoryId).Select(x =>
                    new CategoryThreeModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Slug = x.Slug,
                        ParentCategoryId = x.ParentCategoryId
                    }).ToList();
            }
            return categoriesThree;
        }

        public static List<int> GetChildCategoryId(int id)
        {
            using var context = new EfContext();
            var categories = context.Categories.Select(x => new CategoryThreeModel
            {
                Id = x.Id,
                ParentCategoryId = x.ParentCategoryId
            }).ToList();
            var childCategories = categories.Where(x => x.ParentCategoryId == id).Select(x => x.Id).ToList();

            var result = new List<int>();
            result.AddRange(childCategories);

            foreach (var item in childCategories)
            {
                var child = categories.Where(x => x.ParentCategoryId == item).Select(x => x.Id).ToList();
                result.AddRange(child);
                while (child.Count > 0)
                {
                    foreach (var item2 in child)
                    {
                        child = categories.Where(x => x.ParentCategoryId == item2).Select(x => x.Id).ToList();
                        result.AddRange(child);
                    }
                }
            }

            return childCategories;
        }
    }
}
