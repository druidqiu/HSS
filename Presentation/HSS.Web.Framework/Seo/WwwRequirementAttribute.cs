using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Models.CommonEnum;
using HSS.Models.Configuration;
using System;
using System.Web.Mvc;

namespace HSS.Web.Framework.Seo
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class WwwRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            
            if (filterContext.IsChildAction)
                return;
            
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;
            
            if (filterContext.HttpContext.Request.IsLocal)
                return;
            
            var seoSettings = EngineContext.Current.Resolve<SeoSettings>();

            switch (seoSettings.WwwRequirement)
            {
                case WwwRequirement.WithWww:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        string url = webHelper.GetThisPageUrl(true);
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            bool startsWith3W = url.StartsWith("https://www.", StringComparison.OrdinalIgnoreCase);
                            if (!startsWith3W)
                            {
                                url = url.Replace("https://", "https://www.");
                                
                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                        else
                        {
                            bool startsWith3W = url.StartsWith("http://www.", StringComparison.OrdinalIgnoreCase);
                            if (!startsWith3W)
                            {
                                url = url.Replace("http://", "http://www.");
                                
                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                    }
                    break;
                case WwwRequirement.WithoutWww:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        string url = webHelper.GetThisPageUrl(true);
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            bool startsWith3W = url.StartsWith("https://www.", StringComparison.OrdinalIgnoreCase);
                            if (startsWith3W)
                            {
                                url = url.Replace("https://www.", "https://");
                                
                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                        else
                        {
                            bool startsWith3W = url.StartsWith("http://www.", StringComparison.OrdinalIgnoreCase);
                            if (startsWith3W)
                            {
                                url = url.Replace("http://www.", "http://");
                                
                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                    }
                    break;
                case WwwRequirement.NoMatter:
                    {
                    }
                    break;
                default:
                    throw new Exception("Not supported WwwRequirement parameter");
            }
        }
    }
}

