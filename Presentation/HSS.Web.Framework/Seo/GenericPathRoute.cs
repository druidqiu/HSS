using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Services.Seo;
using HSS.Web.Framework.Localization;
using System;
using System.Web;
using System.Web.Routing;

namespace HSS.Web.Framework.Seo
{

    public partial class GenericPathRoute : LocalizedRoute
    {
        #region Constructors
        
        public GenericPathRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }
        
        public GenericPathRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }
        
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }
        
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods
        
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData data = base.GetRouteData(httpContext);
            if (data != null)
            {
                var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
                var slug = data.Values["generic_se_name"] as string;
                var urlRecord = urlRecordService.GetBySlugCached(slug);
                if (urlRecord == null)
                {

                    data.Values["controller"] = "Common";
                    data.Values["action"] = "PageNotFound";
                    return data;
                }
                if (!urlRecord.IsActive)
                {
                    var activeSlug = urlRecordService.GetActiveSlug(urlRecord.EntityId, urlRecord.EntityName, urlRecord.LanguageId);
                    if (string.IsNullOrWhiteSpace(activeSlug))
                    {
                        data.Values["controller"] = "Common";
                        data.Values["action"] = "PageNotFound";
                        return data;
                    }
                    
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var response = httpContext.Response;
                    response.Status = "301 Moved Permanently";
                    response.RedirectLocation = string.Format("{0}{1}", webHelper.GetStoreLocation(false), activeSlug);
                    response.End();
                    return null;
                }
                
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                var slugForCurrentLanguage = SeoExtensions.GetSeName(urlRecord.EntityId, urlRecord.EntityName, workContext.WorkingLanguage.Id);
                if (!String.IsNullOrEmpty(slugForCurrentLanguage) &&
                    !slugForCurrentLanguage.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var response = httpContext.Response;
                    response.Status = "302 Moved Temporarily";
                    response.RedirectLocation = string.Format("{0}{1}", webHelper.GetStoreLocation(false), slugForCurrentLanguage);
                    response.End();
                    return null;
                }

                //process URL
                switch (urlRecord.EntityName.ToLowerInvariant())
                {
                    case "product":
                        {
                            data.Values["controller"] = "Product";
                            data.Values["action"] = "ProductDetails";
                            data.Values["productid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "category":
                        {
                            data.Values["controller"] = "Catalog";
                            data.Values["action"] = "Category";
                            data.Values["categoryid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "newsitem":
                        {
                            data.Values["controller"] = "News";
                            data.Values["action"] = "NewsItem";
                            data.Values["newsItemId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "topic":
                        {
                            data.Values["controller"] = "Topic";
                            data.Values["action"] = "TopicDetails";
                            data.Values["topicId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                }
            }
            return data;
        }

        #endregion
    }
}
