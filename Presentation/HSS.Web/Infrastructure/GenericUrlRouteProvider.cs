using HSS.Web.Framework.Localization;
using HSS.Web.Framework.Mvc.Routes;
using HSS.Web.Framework.Seo;
using System.Web.Routing;

namespace HSS.Web.Infrastructure
{

    public partial class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //通用 URLs
            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Nop.Web.Controllers" });
                       
            routes.MapLocalizedRoute("NewsItem",
                            "{SeName}",
                            new { controller = "News", action = "NewsItem" },
                            new[] { "HSS.Web.Controllers" });
            

            routes.MapLocalizedRoute("Topic",
                            "{SeName}",
                            new { controller = "Topic", action = "TopicDetails" },
                            new[] { "HSS.Web.Controllers" });


            
        }

        public int Priority
        {
            get
            {
                return -1000000;
            }
        }
    }
}