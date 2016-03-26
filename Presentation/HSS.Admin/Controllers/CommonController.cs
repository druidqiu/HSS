using HSS.Core;
using HSS.Core.Caching;
using HSS.Services.Configuration;
using HSS.Services.Customers;
using HSS.Services.Localization;
using HSS.Services.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSS.Admin.Controllers
{
    public class CommonController : BaseAdminController
    {
        #region Fields
        
        private readonly ICustomerService _customerService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        private readonly ICacheManager _cacheManager;
        private readonly HttpContextBase _httpContext;

        #endregion

        #region Constructors

        public CommonController(
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            ILanguageService languageService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ILocalizationService localizationService,
            ISettingService settingService,
            ICacheManager cacheManager,
            HttpContextBase httpContext)
        {
            this._customerService = customerService;
            this._urlRecordService = urlRecordService;
            this._webHelper = webHelper;
            this._languageService = languageService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._localizationService = localizationService;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._httpContext = httpContext;
        }

        #endregion

        #region Action
        public ActionResult PageNotFound() {
            return View();           
        }

        public ActionResult ClearCache()
        {
            _cacheManager.Clear();
            return RedirectToAction("Index");
        }


        #endregion

    }
}