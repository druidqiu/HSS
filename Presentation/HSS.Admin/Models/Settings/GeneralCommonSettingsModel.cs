using HSS.Web.Framework;
using HSS.Web.Framework.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HSS.Admin.Models.Settings
{
    public partial class GeneralCommonSettingsModel : BaseModel
    {
        public GeneralCommonSettingsModel()
        {
            SeoSettings = new SeoSettingsModel();
            SecuritySettings = new SecuritySettingsModel();
            LocalizationSettings = new LocalizationSettingsModel();
            FullTextSettings = new FullTextSettingsModel();
        }
        
        public SeoSettingsModel SeoSettings { get; set; }
        public SecuritySettingsModel SecuritySettings { get; set; }
        public LocalizationSettingsModel LocalizationSettings { get; set; }
        public FullTextSettingsModel FullTextSettings { get; set; }
        
        public int ActiveStoreScopeConfiguration { get; set; }


        #region Nested classes
        

        public partial class SeoSettingsModel : BaseModel
        {
            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.PageTitleSeparator")]
            [AllowHtml]
            public string PageTitleSeparator { get; set; }
            
            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.PageTitleSeoAdjustment")]
            public int PageTitleSeoAdjustment { get; set; }
            public SelectList PageTitleSeoAdjustmentValues { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.DefaultTitle")]
            [AllowHtml]
            public string DefaultTitle { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.DefaultMetaKeywords")]
            [AllowHtml]
            public string DefaultMetaKeywords { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.DefaultMetaDescription")]
            [AllowHtml]
            public string DefaultMetaDescription { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.ConvertNonWesternChars")]
            public bool ConvertNonWesternChars { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.CanonicalUrlsEnabled")]
            public bool CanonicalUrlsEnabled { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.WwwRequirement")]
            public int WwwRequirement { get; set; }
            public SelectList WwwRequirementValues { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.EnableJsBundling")]
            public bool EnableJsBundling { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.EnableCssBundling")]
            public bool EnableCssBundling { get; set; }
        }

        public partial class SecuritySettingsModel : BaseModel
        {
            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.EncryptionKey")]
            [AllowHtml]
            public string EncryptionKey { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.AdminAreaAllowedIpAddresses")]
            [AllowHtml]
            public string AdminAreaAllowedIpAddresses { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.ForceSslForAllPages")]
            public bool ForceSslForAllPages { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.EnableXSRFProtectionForPublicStore")]
            public bool EnableXsrfProtectionForPublicStore { get; set; }

        }        

        public partial class LocalizationSettingsModel : BaseModel
        {

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.SeoFriendlyUrlsForLanguagesEnabled")]
            public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.AutomaticallyDetectLanguage")]
            public bool AutomaticallyDetectLanguage { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.LoadAllLocaleRecordsOnStartup")]
            public bool LoadAllLocaleRecordsOnStartup { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.LoadAllUrlRecordsOnStartup")]
            public bool LoadAllUrlRecordsOnStartup { get; set; }
        }

        public partial class FullTextSettingsModel : BaseModel
        {
            public bool Supported { get; set; }

            public bool Enabled { get; set; }

            [ResourceDisplayName("Admin.Configuration.Settings.GeneralCommon.FullTextSettings.SearchMode")]
            public int SearchMode { get; set; }
            public SelectList SearchModeValues { get; set; }
        }
        
        #endregion
    }
}