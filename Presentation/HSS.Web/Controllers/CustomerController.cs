using HSS.Core;
using HSS.Models.Configuration;
using HSS.Services.Authentication;
using HSS.Services.Customers;
using HSS.Services.Localization;
using HSS.Services.Logging;
using HSS.Web.Framework.Security;
using HSS.Web.Models.Customer;
using System.Web.Mvc;
using ZhengHe.Web.Framework.Security;

namespace HSS.Web.Controllers
{
    public class CustomerController : BasePublicController
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly IWebHelper _webHelper;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerRegistrationService _customerRegistrationService;

        private readonly LocalizationSettings _localizationSettings;
        private readonly SecuritySettings _securitySettings;

        #endregion

        #region Ctor

        public CustomerController(IAuthenticationService authenticationService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            ICustomerService customerService,
            RewardPointsSettings rewardPointsSettings,
            CustomerSettings customerSettings,
            IWebHelper webHelper,
            ICustomerActivityService customerActivityService,
            ICustomerRegistrationService customerRegistrationService,
            LocalizationSettings localizationSettings,
            SecuritySettings securitySettings)
        {
            this._authenticationService = authenticationService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._rewardPointsSettings = rewardPointsSettings;
            this._customerSettings = customerSettings;
            this._webHelper = webHelper;
            this._customerActivityService = customerActivityService;
            this._customerRegistrationService = customerRegistrationService;
            this._localizationSettings = localizationSettings;
            this._securitySettings = securitySettings;
        }

        #endregion

        #region Login / Register /Loginout

        [HttpsRequirement(SslRequirement.Yes)]
        public ActionResult Register() {
            var model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        //[HoneypotValidator]
        [PublicAntiForgery]
        [ValidateInput(false)]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                _authenticationService.SignOut();

                _workContext.CurrentCustomer = _customerService.InsertGuestCustomer();
            }
            var customer = _workContext.CurrentCustomer;


            if (ModelState.IsValid)
            {
                //model.LoginName = model.LoginName.Trim();

                //var registrationRequest = new CustomerRegistrationRequest(customer, model.LoginName, model.Password, _customerSettings.DefaultPasswordFormat, false);
                //var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);
                //if (registrationResult.Success)
                //{
                //    //login customer now
                //    //if (isApproved)
                //    _authenticationService.SignIn(customer, true);

                //    return RedirectToRoute("RegisterResult");
                //}

                ////errors
                //foreach (var error in registrationResult.Errors)
                //    ModelState.AddModelError("", error);
            }

            return View(model);
        }
        
        /// <summary>
        /// 反馈信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterResult()
        {
            var resultText = _localizationService.GetResource("Account.Register.Result.Standard");
            var model = new RegisterResultModel
            {
                Result = resultText
            };
            return View(model);
        }


        #endregion

    }
}