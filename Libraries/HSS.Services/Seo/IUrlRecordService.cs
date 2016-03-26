using HSS.Core;
using HSS.Models;

namespace HSS.Services.Seo
{
    /// <summary>
    /// 网址服务接口
    /// </summary>
    public partial interface  IUrlRecordService
    {
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void DeleteUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 根据主键获取记录
        /// </summary>
        /// <param name="urlRecordId">主键</param>
        /// <returns>URL record</returns>
        UrlRecord GetUrlRecordById(int urlRecordId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="urlRecord">URL 记录</param>
        void InsertUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="urlRecord">URL 记录</param>
        void UpdateUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 查找URL记录
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecord GetBySlug(string slug);

        /// <summary>
        /// Find URL record (cached version).
        /// This method works absolutely the same way as "GetBySlug" one but caches the results.
        /// Hence, it's used only for performance optimization in public store
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecordService.UrlRecordForCaching GetBySlugCached(string slug);

        /// <summary>
        /// Gets all URL records
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>URL records</returns>
        IPagedList<UrlRecord> GetAllUrlRecords(string slug = "", int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        string GetActiveSlug(int entityId, string entityName, int languageId);

        /// <summary>
        ///  存储
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        /// <param name="languageId">Language ID</param>
        void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity;
    }
}