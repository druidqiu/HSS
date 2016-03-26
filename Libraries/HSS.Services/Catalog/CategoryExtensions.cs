using HSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HSS.Services.Catalog
{
    /// <summary>
    /// 类别扩展类
    /// </summary>
    public static class CategoryExtensions
    {

        /// <summary>
        /// 格式化类的面包屑对象
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="categoryService">类别服务</param>
        /// <param name="separator">Separator</param>
        /// <returns>Formatted breadcrumb</returns>
        public static string GetFormattedBreadCrumb(this Category category,
            ICategoryService categoryService,
            string separator = ">>")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, categoryService, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Name;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : string.Format("{0} {1} {2}", result, separator, categoryName);
            }

            return result;
        }

        /// <summary>
        /// 格式化类的面包屑对象
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="allCategories">所有类别</param>
        /// <param name="separator">Separator</param>
        /// <returns>Formatted breadcrumb</returns>
        public static string GetFormattedBreadCrumb(this Category category,
            IList<Category> allCategories,
            string separator = ">>")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, allCategories, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Name;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : string.Format("{0} {1} {2}", result, separator, categoryName);
            }

            return result;
        }

        /// <summary>
        /// 获取类的面包屑
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="categoryService">Category service</param>
        /// <param name="showHidden">是否显示隐藏的值</param>
        /// <returns>Category breadcrumb </returns>
        public static IList<Category> GetCategoryBreadCrumb(this Category category,
            ICategoryService categoryService,
            bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<Category>();
            
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                !category.Deleted && //not deleted
                (showHidden || category.Published) && //published
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(category);

                alreadyProcessedCategoryIds.Add(category.Id);

                category = categoryService.GetCategoryById(category.ParentCategoryId);
            }
            result.Reverse();
            return result;
        }

        /// <summary>
        /// 获取类的面包屑
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="storeMappingService">Store mapping service</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>Category breadcrumb </returns>
        public static IList<Category> GetCategoryBreadCrumb(this Category category,
            IList<Category> allCategories,
            bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<Category>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                !category.Deleted && //not deleted
                (showHidden || category.Published) && //published
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(category);

                alreadyProcessedCategoryIds.Add(category.Id);

                category = (from c in allCategories
                            where c.Id == category.ParentCategoryId
                            select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }
    }
}
