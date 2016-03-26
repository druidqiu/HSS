using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using HSS.Core.Infrastructure;

namespace HSS.Web.Framework.Mvc.Routes
{

    /// <summary>
    /// 路由发布实现类
    /// </summary>
    public class RoutePublisher : IRoutePublisher
    {
        protected readonly ITypeFinder typeFinder;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="typeFinder"></param>
        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
    }
}
