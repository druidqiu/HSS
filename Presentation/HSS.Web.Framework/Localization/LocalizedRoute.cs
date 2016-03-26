using System.Web;
using System.Web.Routing;
using HSS.Core;
using HSS.Models.Configuration;
using HSS.Core.Infrastructure;

namespace HSS.Web.Framework.Localization
{
    /// <summary>
    /// 本地化路由
    /// </summary>
    public class LocalizedRoute : Route
    {
        #region Fields

        private bool? _seoFriendlyUrlsForLanguagesEnabled;

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="routeHandler">处理请求的对象</param>
        public LocalizedRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="defaults">参数</param>
        /// <param name="routeHandler">处理请求的对象</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="defaults">参数</param>
        /// <param name="constraints">有效值的正则表达式</param>
        /// <param name="routeHandler">处理路由请求的对象</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="defaults">参数</param>
        /// <param name="constraints">有效值的正则表达式</param>
        /// <param name="dataTokens">token</param>
        /// <param name="routeHandler">处理路由请求的对象</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 返回有关请求路由的信息
        /// </summary>
        /// <param name="httpContext">一个对象封装在HTTP请求信息</param>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
                string applicationPath = httpContext.Request.ApplicationPath;
                if (virtualPath.IsLocalizedUrl(applicationPath, false))
                {
                    string rawUrl = httpContext.Request.RawUrl;
                    var newVirtualPath = rawUrl.RemoveLanguageSeoCodeFromRawUrl(applicationPath);
                    if (string.IsNullOrEmpty(newVirtualPath))
                        newVirtualPath = "/";
                    newVirtualPath = newVirtualPath.RemoveApplicationPathFromRawUrl(applicationPath);
                    newVirtualPath = "~" + newVirtualPath;
                    httpContext.RewritePath(newVirtualPath, true);
                }
            }
            RouteData data = base.GetRouteData(httpContext);
            return data;
        }

        /// <summary>
        /// 返回与路由相关联的网址的信息
        /// </summary>
        /// <param name="requestContext">一个对象封装了请求的路由信息</param>
        /// <param name="values">一个包含路由参数的对象</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData data = base.GetVirtualPath(requestContext, values);

            if (data != null && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string rawUrl = requestContext.HttpContext.Request.RawUrl;
                string applicationPath = requestContext.HttpContext.Request.ApplicationPath;
                if (rawUrl.IsLocalizedUrl(applicationPath, true))
                {
                    data.VirtualPath = string.Concat(rawUrl.GetLanguageSeoCodeFromUrl(applicationPath, true), "/",
                        data.VirtualPath);
                }
            }
            return data;
        }

        public virtual void ClearSeoFriendlyUrlsCachedValue()
        {
            _seoFriendlyUrlsForLanguagesEnabled = null;
        }

        #endregion

        #region Properties

        protected bool SeoFriendlyUrlsForLanguagesEnabled
        {
            get
            {
                if (!_seoFriendlyUrlsForLanguagesEnabled.HasValue)
                    _seoFriendlyUrlsForLanguagesEnabled = EngineContext.Current.Resolve<LocalizationSettings>().SeoFriendlyUrlsForLanguagesEnabled;

                return _seoFriendlyUrlsForLanguagesEnabled.Value;
            }
        }

        #endregion
    }
}