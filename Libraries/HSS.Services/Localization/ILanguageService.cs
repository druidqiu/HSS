using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Localization
{
    /// <summary>
    /// 语言接口
    /// </summary>
    public partial interface ILanguageService
    {
        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="language">语言实体</param>
        void DeleteLanguage(Language language);

        /// <summary>
        /// 获取所有语言
        /// </summary>
        /// <param name="showHidden">是否显示隐藏的值</param>
        /// <returns>语言集合</returns>
        IList<Language> GetAllLanguages(bool showHidden = false);

        /// <summary>
        /// 根据主键获取语言
        /// </summary>
        /// <param name="languageId">语言主键</param>
        /// <returns>语言实体</returns>
        Language GetLanguageById(int languageId);

        /// <summary>
        /// 新增语言
        /// </summary>
        /// <param name="language">语言实体</param>
        void InsertLanguage(Language language);

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="language">语言实体</param>
        void UpdateLanguage(Language language);
    }
}
