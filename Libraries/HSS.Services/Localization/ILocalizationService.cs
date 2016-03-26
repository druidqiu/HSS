using HSS.Models;
using System.Collections.Generic;

namespace HSS.Services.Localization
{
    /// <summary>
    /// ���ػ���Դ�������ӿ�
    /// </summary>
    public partial interface ILocalizationService
    {
        /// <summary>
        /// ɾ����Դ
        /// </summary>
        /// <param name="localeStringResource">���ػ���Դ</param>
        void DeleteLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// ��ȡ���ػ���Դ
        /// </summary>
        /// <param name="localeStringResourceId">���ػ���Դ����</param>
        /// <returns>���ػ���Դ</returns>
        LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId);

        /// <summary>
        /// ��ȡ���ػ���Դ
        /// </summary>
        /// <param name="resourceName">��Դ��</param>
        /// <returns>���ػ���Դ</returns>
        LocaleStringResource GetLocaleStringResourceByName(string resourceName);

        /// <summary>
        /// ��ȡ���ػ���Դ
        /// </summary>
        /// <param name="resourceName">��Դ��</param>
        /// <param name="languageId">��������</param>
        /// <param name="logIfNotFound">��Ϣ�Ҳ���ʱ�Ƿ��¼������Ϣ</param>
        /// <returns>���ػ���Դ</returns>
        LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId,
            bool logIfNotFound = true);

        /// <summary>
        /// ��������������ȡ���ػ���Դ
        /// </summary>
        /// <param name="languageId">��������</param>
        /// <returns>���ػ���Դes</returns>
        IList<LocaleStringResource> GetAllResources(int languageId);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="localeStringResource">���ػ���Դ</param>
        void InsertLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="localeStringResource">���ػ���Դ</param>
        void UpdateLocaleStringResource(LocaleStringResource localeStringResource);

        /// <summary>
        /// ��ȡ���ػ���Դ��ֵ��
        /// </summary>
        /// <param name="languageId">��������</param>
        /// <returns>���ػ���Դ</returns>
        Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId);

        /// <summary>
        /// ��ȡ���ػ���Դֵ��Ϣ
        /// </summary>
        /// <param name="resourceName">��Դ��</param>
        /// <returns>��Դ��Ϣ</returns>
        string GetResource(string resourceKey);

        /// <summary>
        /// ��ȡ���ػ���ʾ��Դ��Ϣ
        /// </summary>
        /// <param name="resourceName">��Դ��</param>
        /// <param name="languageId">��������</param>
        /// <param name="logIfNotFound">��Ϣ�Ҳ���ʱ�Ƿ��¼������Ϣ</param>
        /// <param name="defaultValue">Ĭ����Ϣ</param>
        string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);
        
    }
}
