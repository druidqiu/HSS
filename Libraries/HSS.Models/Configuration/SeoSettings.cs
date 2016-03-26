using HSS.Models.CommonEnum;
using System.Collections.Generic;

namespace HSS.Models.Configuration
{
    public partial class SeoSettings :ISettings
    {

        /// <summary>
        /// 标题分隔符
        /// </summary>
        public string PageTitleSeparator { get; set; }
        /// <summary>
        /// 页面标题调整
        /// </summary>
        public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }
        /// <summary>
        /// Default title
        /// </summary>
        public string DefaultTitle { get; set; }
        /// <summary>
        /// 默认关键字
        /// </summary>
        public string DefaultMetaKeywords { get; set; }
        /// <summary>
        /// 默认说明
        /// </summary>
        public string DefaultMetaDescription { get; set; }
        /// <summary>
        /// 标准字符
        /// </summary>
        public bool ConvertNonWesternChars { get; set; }
        /// <summary>
        /// 是否允许Unicode字符
        /// </summary>
        public bool AllowUnicodeCharsInUrls { get; set; }
        /// <summary>
        /// 是否应该使用规范的网址标记
        /// </summary>
        public bool CanonicalUrlsEnabled { get; set; }
        /// <summary>
        /// WWW要求
        /// </summary>
        public WwwRequirement WwwRequirement { get; set; }
        /// <summary>
        /// js压缩是否启用
        /// </summary>
        public bool EnableJsBundling { get; set; }
        /// <summary>
        /// CSS压缩是否启用
        /// </summary>
        public bool EnableCssBundling { get; set; }
        /// <summary>
        /// 是否应该生成打开的图形元标记
        /// </summary>
        public bool OpenGraphMetaTags { get; set; }
        /// <summary>
        /// sename需求
        /// </summary>
        public List<string> ReservedUrlRecordSlugs { get; set; }
    }
}
