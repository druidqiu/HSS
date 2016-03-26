using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Infrastructure;
using HSS.Web.Framework.Contoller;
using HSS.Web.Framework.Mvc;
using HSS.Web.Framework.Security;
using System.Web.Mvc;
using ZhengHe.Web.Framework.Security;

namespace HSS.Admin.Controllers
{
    [HttpsRequirement(SslRequirement.Yes)]
    [AdminValidateIpAddress]
    //[AdminAuthorize]
    //[AdminAntiForgery]
    public class BaseAdminController : BaseController
    {
        
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            EngineContext.Current.Resolve<IWorkContext>().Source = HSS.Models.CommonEnum.LogSource.Admin;

            var cache = EngineContext.Current.Resolve<ICacheManager>();
            cache.Set(CommonCacheNames.WEBNAME, HSS.Models.CommonEnum.LogSource.Admin, 0);

            base.Initialize(requestContext);
        }

        
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
                LogException(filterContext.Exception);
            base.OnException(filterContext);
        }

        
        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
        }

        
        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            if (!index.HasValue)
            {
                int tmp;
                if (int.TryParse(this.Request.Form["selected-tab-index"], out tmp))
                {
                    index = tmp;
                }
            }
            if (index.HasValue)
            {
                string dataKey = "zh.selected-tab-index";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = index;
                }
                else
                {
                    ViewData[dataKey] = index;
                }
            }
        }
    }
}