using HSS.Models;
using HSS.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HSS.Services.Configuration
{
    /// <summary>
    /// 配置服务接口
    /// </summary>
    public partial interface ISettingService
    {
        /// <summary>
        /// 根据主键获取配置
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        Settings GetSettingById(int settingId);
        
        void DeleteSetting(Settings setting);

        Settings GetSetting(string key, bool loadSharedValueIfNotFound = false);
        
        T GetSettingByKey<T>(string key, T defaultValue = default(T), 
            bool loadSharedValueIfNotFound = false);
        
        void SetSetting<T>(string key, T value, bool clearCache = true);
        
        IList<Settings> GetAllSettings();
        
        bool SettingExists<T, TPropType>(T settings, 
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new();
        
        T LoadSetting<T>() where T : ISettings, new();
        
        void SaveSetting<T>(T settings) where T : ISettings, new();
        
        void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            bool clearCache = true) where T : ISettings, new();
        
        void DeleteSetting<T>() where T : ISettings, new();
        
        void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();
        
        void ClearCache();
    }
}
