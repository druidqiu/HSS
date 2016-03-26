using Autofac;
using Autofac.Core;
using HSS.Core.Caching;
using HSS.Core.Infrastructure;
using HSS.Core.Infrastructure.DependencyManagement;
using HSS.Web.Controllers;

namespace HSS.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //builder.RegisterType<BlogController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            //builder.RegisterType<CatalogController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            //builder.RegisterType<CountryController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            builder.RegisterType<CommonController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            //builder.RegisterType<NewsController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            //builder.RegisterType<ProductController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            builder.RegisterType<TopicController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"));
            
        }

        public int Order
        {
            get { return 2; }
        }
    }
}