using HSS.Admin.Models.Settings;
using HSS.Core;
using HSS.Core.Domain.Common;
using HSS.Models.CommonEnum;
using HSS.Models.Configuration;
using HSS.Services.Common;
using HSS.Services.Configuration;
using HSS.Services.Localization;
using HSS.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSS.Admin.Controllers
{
    public partial class SettingController : BaseAdminController
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;

        #endregion

        #region Constructors

        public SettingController(ISettingService settingService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IGenericAttributeService genericAttributeService)
        {
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._genericAttributeService = genericAttributeService;
        }

        #endregion

        #region Method

        /// <summary>
        /// 公共配置
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneralCommon()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
            //    return AccessDeniedView();
            
            this.Server.ScriptTimeout = 300;

            var model = new GeneralCommonSettingsModel();

            var commonSettings = _settingService.LoadSetting<CommonSettings>();

            //seo settings
            var seoSettings = _settingService.LoadSetting<SeoSettings>();
            model.SeoSettings.PageTitleSeparator = seoSettings.PageTitleSeparator;
            model.SeoSettings.PageTitleSeoAdjustment = (int)seoSettings.PageTitleSeoAdjustment;
            model.SeoSettings.PageTitleSeoAdjustmentValues = seoSettings.PageTitleSeoAdjustment.ToSelectList();
            model.SeoSettings.DefaultTitle = seoSettings.DefaultTitle;
            model.SeoSettings.DefaultMetaKeywords = seoSettings.DefaultMetaKeywords;
            model.SeoSettings.DefaultMetaDescription = seoSettings.DefaultMetaDescription;
            model.SeoSettings.ConvertNonWesternChars = seoSettings.ConvertNonWesternChars;
            model.SeoSettings.CanonicalUrlsEnabled = seoSettings.CanonicalUrlsEnabled;
            model.SeoSettings.WwwRequirement = (int)seoSettings.WwwRequirement;
            model.SeoSettings.WwwRequirementValues = seoSettings.WwwRequirement.ToSelectList();
            model.SeoSettings.EnableJsBundling = seoSettings.EnableJsBundling;
            model.SeoSettings.EnableCssBundling = seoSettings.EnableCssBundling;
            //model.SeoSettings.OpenGraphMetaTags = seoSettings.OpenGraphMetaTags;
            
            //security settings
            var securitySettings = _settingService.LoadSetting<SecuritySettings>();

            model.SecuritySettings.EncryptionKey = securitySettings.EncryptionKey;
            if (securitySettings.AdminAreaAllowedIpAddresses != null)
                for (int i = 0; i < securitySettings.AdminAreaAllowedIpAddresses.Count; i++)
                {
                    model.SecuritySettings.AdminAreaAllowedIpAddresses += securitySettings.AdminAreaAllowedIpAddresses[i];
                    if (i != securitySettings.AdminAreaAllowedIpAddresses.Count - 1)
                        model.SecuritySettings.AdminAreaAllowedIpAddresses += ",";
                }
            model.SecuritySettings.ForceSslForAllPages = securitySettings.ForceSslForAllPages;
            model.SecuritySettings.EnableXsrfProtectionForPublicStore = securitySettings.EnableXsrfProtectionForPublicStore;


            //localization
            var localizationSettings = _settingService.LoadSetting<LocalizationSettings>();
            model.LocalizationSettings.SeoFriendlyUrlsForLanguagesEnabled = localizationSettings.SeoFriendlyUrlsForLanguagesEnabled;
            model.LocalizationSettings.AutomaticallyDetectLanguage = localizationSettings.AutomaticallyDetectLanguage;
            model.LocalizationSettings.LoadAllLocaleRecordsOnStartup = localizationSettings.LoadAllLocaleRecordsOnStartup;
            model.LocalizationSettings.LoadAllUrlRecordsOnStartup = localizationSettings.LoadAllUrlRecordsOnStartup;
            


            return View(model);
        }

        [HttpPost]
        //[FormValueRequired("save")]
        public ActionResult GeneralCommon(GeneralCommonSettingsModel model)
        {            //seo settings
            var seoSettings = _settingService.LoadSetting<SeoSettings>();
            seoSettings.PageTitleSeparator = model.SeoSettings.PageTitleSeparator;
            seoSettings.PageTitleSeoAdjustment = (PageTitleSeoAdjustment)model.SeoSettings.PageTitleSeoAdjustment;
            seoSettings.DefaultTitle = model.SeoSettings.DefaultTitle;
            seoSettings.DefaultMetaKeywords = model.SeoSettings.DefaultMetaKeywords;
            seoSettings.DefaultMetaDescription = model.SeoSettings.DefaultMetaDescription;
            seoSettings.ConvertNonWesternChars = model.SeoSettings.ConvertNonWesternChars;
            seoSettings.CanonicalUrlsEnabled = model.SeoSettings.CanonicalUrlsEnabled;
            seoSettings.WwwRequirement = (WwwRequirement)model.SeoSettings.WwwRequirement;
            seoSettings.EnableJsBundling = model.SeoSettings.EnableJsBundling;
            seoSettings.EnableCssBundling = model.SeoSettings.EnableCssBundling;

            _settingService.SaveSetting(seoSettings);

            _settingService.ClearCache();
            
            //security settings
            var securitySettings = _settingService.LoadSetting<SecuritySettings>();
           
            if (securitySettings.AdminAreaAllowedIpAddresses == null)
                securitySettings.AdminAreaAllowedIpAddresses = new List<string>();
            securitySettings.AdminAreaAllowedIpAddresses.Clear();
            if (!String.IsNullOrEmpty(model.SecuritySettings.AdminAreaAllowedIpAddresses))
                foreach (string s in model.SecuritySettings.AdminAreaAllowedIpAddresses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    if (!String.IsNullOrWhiteSpace(s))
                        securitySettings.AdminAreaAllowedIpAddresses.Add(s.Trim());
            securitySettings.ForceSslForAllPages = model.SecuritySettings.ForceSslForAllPages;
            securitySettings.EnableXsrfProtectionForPublicStore = model.SecuritySettings.EnableXsrfProtectionForPublicStore;
            _settingService.SaveSetting(securitySettings);
          
            

            //localization settings
            var localizationSettings = _settingService.LoadSetting<LocalizationSettings>();
            
            localizationSettings.AutomaticallyDetectLanguage = model.LocalizationSettings.AutomaticallyDetectLanguage;
            localizationSettings.LoadAllLocaleRecordsOnStartup = model.LocalizationSettings.LoadAllLocaleRecordsOnStartup;
            localizationSettings.LoadAllUrlRecordsOnStartup = model.LocalizationSettings.LoadAllUrlRecordsOnStartup;
            _settingService.SaveSetting(localizationSettings);

            //full-text
            //commonSettings.FullTextMode = (FulltextSearchMode)model.FullTextSettings.SearchMode;
            //_settingService.SaveSetting(commonSettings);

            //activity log
            //_customerActivityService.InsertActivity("EditSettings", _localizationService.GetResource("ActivityLog.EditSettings"));
            

            //selected tab
            SaveSelectedTabIndex();

            return RedirectToAction("GeneralCommon");
            
        }

        /// <summary>
        /// 用户配置
        /// </summary>
        public ActionResult CustomerSettings() {

            this.Server.ScriptTimeout = 300;

            var model = new CustomerSettingsModel();

            var customerSettings = _settingService.LoadSetting<CustomerSettings>();

            model.AccessPathEnabled = customerSettings.AccessPathEnabled;
            model.AllowUsersToChangeUsernames = customerSettings.AllowUsersToChangeUsernames;
            model.DefaultPasswordFormat = (int)customerSettings.DefaultPasswordFormat;
            model.DefaultPasswordFormatValues = customerSettings.DefaultPasswordFormat.ToSelectList();
            model.HashedPasswordFormat = customerSettings.HashedPasswordFormat;
            model.PasswordMinLength = customerSettings.PasswordMinLength;
            model.StoreLastVisitedPage = customerSettings.StoreLastVisitedPage;
            model.UsernameMaxLenght = customerSettings.UsernameMaxLenght;
            model.UsernameMinLength = customerSettings.UsernameMinLength;            

            return View(model);
        }


        [HttpPost]
        //[FormValueRequired("save")]
        public ActionResult CustomerSettings(CustomerSettingsModel model)
        {
            var customerSettings = _settingService.LoadSetting<CustomerSettings>();

            customerSettings.AccessPathEnabled = model.AccessPathEnabled;
            customerSettings.AllowUsersToChangeUsernames = model.AllowUsersToChangeUsernames;
            customerSettings.DefaultPasswordFormat = (PasswordFormat)model.DefaultPasswordFormat;            
            customerSettings.HashedPasswordFormat = model.HashedPasswordFormat;
            customerSettings.PasswordMinLength = model.PasswordMinLength;
            customerSettings.StoreLastVisitedPage = model.StoreLastVisitedPage;
            customerSettings.UsernameMaxLenght = model.UsernameMaxLenght;
            customerSettings.UsernameMinLength = model.UsernameMinLength;

            _settingService.SaveSetting(customerSettings);
            _settingService.ClearCache();

            return RedirectToAction("CustomerSettings");
        }
        #endregion
    }
}