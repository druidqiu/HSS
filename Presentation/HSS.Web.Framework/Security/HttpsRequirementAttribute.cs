using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Models.Configuration;
using System;
using System.Web.Mvc;
using ZhengHe.Web.Framework.Security;

namespace HSS.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public HttpsRequirementAttribute(SslRequirement sslRequirement)
        {
            this.SslRequirement = sslRequirement;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (filterContext.IsChildAction)
                return;

            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;
            
            var securitySettings = EngineContext.Current.Resolve<SecuritySettings>();
            if (securitySettings.ForceSslForAllPages)

                this.SslRequirement = SslRequirement.Yes;

            switch (this.SslRequirement)
            {
                case SslRequirement.Yes:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (!currentConnectionSecured)
                        {
                            var storeContext = EngineContext.Current.Resolve<IStoreContext>();
                            if (storeContext.SslEnable)
                            {
                                //string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                                string url = webHelper.GetThisPageUrl(true, true);

                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                    }
                    break;
                case SslRequirement.No:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            //string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                            string url = webHelper.GetThisPageUrl(true, false);

                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                    break;
                case SslRequirement.NoMatter:
                    {
                        //do nothing
                    }
                    break;
                default:
                    throw new Exception("不支持SSL参数");
            }
        }

        public SslRequirement SslRequirement { get; set; }
    }
}
