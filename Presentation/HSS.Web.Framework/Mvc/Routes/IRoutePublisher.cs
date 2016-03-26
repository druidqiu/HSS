
using System.Web.Routing;

namespace HSS.Web.Framework.Mvc.Routes
{
    /// <summary>
    /// 路由发布接口
    /// </summary>
    public interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(RouteCollection routes);
    }
}
