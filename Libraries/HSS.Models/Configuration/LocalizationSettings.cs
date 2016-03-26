namespace HSS.Models.Configuration
{
    /// <summary>
    /// 本地化语言配置
    /// </summary>
    public partial class LocalizationSettings:ISettings
    {
        /// <summary>
        /// 默认语言
        /// </summary>
        public int DefaultLanguageId { get; set; }

        /// <summary>
        /// 是否自动检测用户所属语言
        /// </summary>
        public bool AutomaticallyDetectLanguage { get; set; }

        /// <summary>
        /// 是否启动加载所有语言模块
        /// </summary>
        public bool LoadAllLocaleRecordsOnStartup { get; set; }

        /// <summary>
        /// 搜索引擎优化的友好网址与多个语言是启用
        /// </summary>
        public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

        /// <summary>
        /// 是否加载所有搜索引擎友好名称
        /// </summary>
        public bool LoadAllUrlRecordsOnStartup { get; set; }
    }
}
