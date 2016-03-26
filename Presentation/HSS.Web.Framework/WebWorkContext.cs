using HSS.Core;
using HSS.Core.Caching;
using HSS.Models;
using HSS.Models.CommonEnum;
using HSS.Models.Configuration;
using HSS.Models.SystemNames;
using HSS.Services.Common;
using HSS.Services.Customers;
using HSS.Services.Localization;
using HSS.Web.Framework.Mvc;
using System;
using System.Linq;
using System.Web;

namespace HSS.Web.Framework
{
    /// <summary>
    /// 网络应用程序的工作环境实现类
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "zh.customer";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILanguageService _languageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly ICacheManager _cacheManager;

        private Customer _cachedCustomer;
        private Language _cachedLanguage;
        private LogSource? _source;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            ILanguageService languageService,
            ICacheManager cacheManager,
            LocalizationSettings localizationSettings)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._genericAttributeService = genericAttributeService;
            this._languageService = languageService;
            this._cacheManager = cacheManager;
            this._localizationSettings = localizationSettings;
        }

        #endregion

        #region Utilities

        protected virtual HttpCookie GetCustomerCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[CustomerCookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerGuid.ToString();
                if (customerGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(CustomerCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        protected virtual Language GetLanguageFromBrowserSettings()
        {
            if (_httpContext == null ||
                _httpContext.Request == null ||
                _httpContext.Request.UserLanguages == null)
                return null;

            var userLanguage = _httpContext.Request.UserLanguages.FirstOrDefault();
            if (String.IsNullOrEmpty(userLanguage))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published)
            {
                return language;
            }

            return null;
        }
        #endregion

        #region Properties

        /// <summary>
        /// 当前访问用户
        /// </summary>
        public virtual Customer CurrentCustomer
        {
            get
            {
                if (_cachedCustomer != null)
                    return _cachedCustomer;

                Customer customer = null;
                
                if (customer == null)
                {
                    var customerCookie = GetCustomerCookie();
                    if (customerCookie != null && !String.IsNullOrEmpty(customerCookie.Value))
                    {
                        Guid customerGuid;
                        if (Guid.TryParse(customerCookie.Value, out customerGuid))
                        {
                            var customerByCookie = _customerService.GetCustomerByGuid(customerGuid);
                            if (customerByCookie != null)
                                customer = customerByCookie;
                        }
                    }
                }

                //如果是匿名用户的话
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    customer = _customerService.InsertGuestCustomer();
                }
                
                //验证用户是否删除或是未激活
                if (!customer.Deleted && customer.Active)
                {
                    SetCustomerCookie(customer.CustomerGuid);
                    _cachedCustomer = customer;
                }

                return _cachedCustomer;
            }
            set
            {
                SetCustomerCookie(value.CustomerGuid);
                _cachedCustomer = value;
            }
        }


        /// <summary>
        /// 当前工作环境语言
        /// </summary>
        public virtual Language WorkingLanguage
        {
            get
            {
                if (_cachedLanguage != null)
                    return _cachedLanguage;

                var baseEntity = new BaseEntity {
                    Id = this.CurrentCustomer.Id,
                    type = this.CurrentCustomer.GetType(),
                };

                Language detectedLanguage = null;
                if (detectedLanguage == null && _localizationSettings.AutomaticallyDetectLanguage)
                {
                    if (!baseEntity.GetAttribute<bool>(SystemCustomerAttributeNames.LanguageAutomaticallyDetected, _genericAttributeService))
                    {
                        detectedLanguage = GetLanguageFromBrowserSettings();
                        if (detectedLanguage != null)
                        {
                            _genericAttributeService.SaveAttribute(baseEntity, SystemCustomerAttributeNames.LanguageAutomaticallyDetected, true);
                        }
                    }
                }
                if (detectedLanguage != null)
                {
                    //检测到的语言进行存储
                    if (baseEntity.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId, _genericAttributeService) != detectedLanguage.Id)
                    {
                        _genericAttributeService.SaveAttribute(baseEntity, SystemCustomerAttributeNames.LanguageId,
                            detectedLanguage.Id);
                    }
                }

                var allLanguages = _languageService.GetAllLanguages();
                //查找当前用户语言
                var languageId = baseEntity.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId,
                    _genericAttributeService);
                var language = allLanguages.FirstOrDefault(x => x.Id == languageId);
                if (language == null)
                {
                    //未找到语言，指定第一个
                    language = allLanguages.FirstOrDefault();
                }
                if (language == null)
                {
                    //未找到语言，指定第一个
                    language = _languageService.GetAllLanguages().FirstOrDefault();
                }

                //cache
                _cachedLanguage = language;
                return _cachedLanguage;
            }
            set
            {

                var baseEntity = new BaseEntity
                {
                    Id = this.CurrentCustomer.Id,
                    type = this.CurrentCustomer.GetType(),
                };

                var languageId = value != null ? value.Id : 0;
                _genericAttributeService.SaveAttribute(baseEntity,
                    SystemCustomerAttributeNames.LanguageId, languageId);

                //重置缓存
                _cachedLanguage = null;
            }
        }
        public virtual LogSource? Source { get {

                if (_source.HasValue)
                    return _source;

                _source = _cacheManager.Get<LogSource>(CommonCacheNames.WEBNAME);                
                return _source;

            } set {
                _source = null;
            } }
        #endregion
    }
}
