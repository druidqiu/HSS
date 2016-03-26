using HSS.Core;
using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Catalog
{
    /// <summary>
    /// ������ӿ�
    /// </summary>
    public partial interface ICategoryService
    {
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteCategory(Category category);

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="categoryName">�������</param>
        /// <param name="pageIndex">Page </param>
        /// <param name="pageSize">Page </param>
        /// <param name="showHidden">�Ƿ���ʾ���ص�ֵ</param>
        /// <returns>Categories</returns>
        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// ��ȡ���������˵ĸ����ʶ��
        /// </summary>
        /// <param name="parentCategoryId">���ڵ�</param>
        /// <param name="showHidden">�Ƿ�����ʾ���ص�ֵ</param>
        /// <returns>Categories</returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false);
                        
        /// <summary>
        /// ����������ȡ���
        /// </summary>
        /// <param name="categoryId">Category ����</param>
        /// <returns>Category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="category">Category</param>
        void InsertCategory(Category category);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateCategory(Category category);
        
    }
}
