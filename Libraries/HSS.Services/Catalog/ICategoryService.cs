using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Catalog
{
    /// <summary>
    /// 类别服务接口
    /// </summary>
    public partial interface ICategoryService
    {
        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteCategory(Category category);

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="pageIndex">Page </param>
        /// <param name="pageSize">Page </param>
        /// <param name="showHidden">是否显示隐藏的值</param>
        /// <returns>Categories</returns>
        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// 获取所有类别过滤的父类标识符
        /// </summary>
        /// <param name="parentCategoryId">父节点</param>
        /// <param name="showHidden">是否已显示隐藏的值</param>
        /// <returns>Categories</returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false);
                        
        /// <summary>
        /// 根据主键获取类别
        /// </summary>
        /// <param name="categoryId">Category 主键</param>
        /// <returns>Category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="category">Category</param>
        void InsertCategory(Category category);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateCategory(Category category);
        
    }
}
