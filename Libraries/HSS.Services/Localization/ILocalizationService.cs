using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Localization
{
    /// <summary>
    /// 本地化资源管理器接口
    /// </summary>
    public partial interface ILocalizationService
    {
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="localeStringResource">本地化资源</param>
        void DeleteLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        /// <param name="localeStringResourceId">本地化资源主键</param>
        /// <returns>本地化资源</returns>
        LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId);

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        /// <param name="resourceName">资源键</param>
        /// <returns>本地化资源</returns>
        LocaleStringResource GetLocaleStringResourceByName(string resourceName);

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        /// <param name="resourceName">资源键</param>
        /// <param name="languageId">语言主键</param>
        /// <param name="logIfNotFound">信息找不到时是否记录错误信息</param>
        /// <returns>本地化资源</returns>
        LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId,
            bool logIfNotFound = true);

        /// <summary>
        /// 根据语言主键获取本地化资源
        /// </summary>
        /// <param name="languageId">语言主键</param>
        /// <returns>本地化资源es</returns>
        IList<LocaleStringResource> GetAllResources(int languageId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="localeStringResource">本地化资源</param>
        void InsertLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="localeStringResource">本地化资源</param>
        void UpdateLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// 获取本地化资源键值对
        /// </summary>
        /// <param name="languageId">语言主键</param>
        /// <returns>本地化资源</returns>
        Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId);

        /// <summary>
        /// 获取本地化资源值信息
        /// </summary>
        /// <param name="resourceName">资源键</param>
        /// <returns>资源信息</returns>
        string GetResource(string resourceKey);

        /// <summary>
        /// 获取本地化提示资源信息
        /// </summary>
        /// <param name="resourceName">资源键</param>
        /// <param name="languageId">语言主键</param>
        /// <param name="logIfNotFound">信息找不到时是否记录错误信息</param>
        /// <param name="defaultValue">默认信息</param>
        string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);
        
    }
}
