using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Models;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HSS.Services.Localization
{
    public static class LocalizationExtensions
    {
        /// <summary>
        /// 得到一个实体的本地化属性
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>Localized property</returns>
        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector)
            where T : BaseEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            return GetLocalized(entity, keySelector);
        }

        public static string GetLocalized<T>(this T entity, 
            Expression<Func<T, string>> keySelector,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true) 
            where T : BaseEntity
        {
            return GetLocalized<T, string>(entity, keySelector, returnDefaultValue, ensureTwoPublishedLanguages);
        }
        public static TPropType GetLocalized<T, TPropType>(this T entity,
            Expression<Func<T, TPropType>> keySelector,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            TPropType result = default(TPropType);
            string resultStr = string.Empty;

            //load localized value
            string localeKeyGroup = typeof(T).Name;
            string localeKey = propInfo.Name;
            

            //set default value if required
            if (String.IsNullOrEmpty(resultStr) && returnDefaultValue)
            {
                var localizer = keySelector.Compile();
                result = localizer(entity);
            }
            
            return result;
        }
        
        
        /// <summary>
        /// Get localized value of enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, IWorkContext workContext)
            where T : struct
        {
            if (workContext == null)
                throw new ArgumentNullException("workContext");

            return GetLocalizedEnum(enumValue, localizationService, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        /// 枚举转换为本地化资源
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="localizationService">本地化服务接口</param>
        /// <param name="languageId">当前语言主键</param>
        /// <returns>本地化值</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, int languageId)
            where T : struct
        {
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");

            if (!typeof(T).IsEnum) throw new ArgumentException("T 必须是枚举类型");

            //本地化值字符串组建
            string resourceName = string.Format("Enums.{0}.{1}", 
                typeof(T).ToString(), 
                //Convert.ToInt32(enumValue)
                enumValue.ToString());
            string result = localizationService.GetResource(resourceName, languageId, false, "", true);

            //set default value if required
            if (String.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }


        /// <summary>
        /// Get localized value of permission
        /// We don't have UI to manage permission localizable name. That's why we're using this extension method
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, IWorkContext workContext)
        {
            if (workContext == null)
                throw new ArgumentNullException("workContext");

            return GetLocalizedPermissionName(permissionRecord, localizationService, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        /// Get localized value of enum
        /// We don't have UI to manage permission localizable name. That's why we're using this extension method
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedPermissionName(this PermissionRecord permissionRecord, 
            ILocalizationService localizationService, int languageId)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException("permissionRecord");

            if (localizationService == null)
                throw new ArgumentNullException("localizationService");

            //localized value
            string resourceName = string.Format("Permission.{0}", permissionRecord.SystemName);
            string result = localizationService.GetResource(resourceName, languageId, false, "", true);

            //set default value if required
            if (String.IsNullOrEmpty(result))
                result = permissionRecord.Name;

            return result;
        }
        /// <summary>
        /// Save localized name of a permission
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        public static void SaveLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, ILanguageService languageService)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException("permissionRecord");
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");
            if (languageService == null)
                throw new ArgumentNullException("languageService");

            string resourceName = string.Format("Permission.{0}", permissionRecord.SystemName);
            string resourceValue = permissionRecord.Name;

            foreach (var lang in languageService.GetAllLanguages(true))
            {
                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr == null)
                {
                    lsr = new LocaleStringResource
                    {
                        LanguageId = lang.Id,
                        ResourceName = resourceName,
                        ResourceValue = resourceValue
                    };
                    localizationService.InsertLocaleStringResource(lsr);
                }
                else
                {
                    lsr.ResourceValue = resourceValue;
                    localizationService.UpdateLocaleStringResource(lsr);
                }
            }
        }
        /// <summary>
        /// Delete a localized name of a permission
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        public static void DeleteLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, ILanguageService languageService)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException("permissionRecord");
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");
            if (languageService == null)
                throw new ArgumentNullException("languageService");

            string resourceName = string.Format("Permission.{0}", permissionRecord.SystemName);
            foreach (var lang in languageService.GetAllLanguages(true))
            {
                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr != null)
                    localizationService.DeleteLocaleStringResource(lsr);
            }
        }
        
    }
}
