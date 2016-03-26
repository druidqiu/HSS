using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Models.Configuration;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSS.Web.Framework.Security
{
    public class AdminValidateIpAddressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null)
                return;

            //不是过滤器的子方法
            if (filterContext.IsChildAction)
                return;
            bool ok = false;
            var ipAddresses = EngineContext.Current.Resolve<SecuritySettings>().AdminAreaAllowedIpAddresses;
            if (ipAddresses != null && ipAddresses.Count > 0)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                foreach (string ip in ipAddresses)
                    if (ip.Equals(webHelper.GetCurrentIpAddress(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        ok = true;
                        break;
                    }
            }
            else
            {
                ok = true;
            }

            if (!ok)
            {
                //确保不是访问被拒绝页面
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                var thisPageUrl = webHelper.GetThisPageUrl(false);
                if (!thisPageUrl.StartsWith(string.Format("{0}/security/accessdenied", webHelper.GetStoreLocation()), StringComparison.InvariantCultureIgnoreCase))
                {
                    filterContext.Result = new RedirectResult(webHelper.GetStoreLocation() + "security/accessdenied");
                    //filterContext.Result = RedirectToAction("AccessDenied", "Security");
                }
            }
        }
    }
}
