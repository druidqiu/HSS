using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using HSS.Core;
using HSS.Core.Caching;
using HSS.Core.Data;
using HSS.Core.Fakes;
using HSS.Core.Infrastructure;
using HSS.Core.Infrastructure.DependencyManagement;
using HSS.Data;
using HSS.Models.Configuration;
using HSS.Services.Authentication;
using HSS.Services.Common;
using HSS.Services.Configuration;
using HSS.Services.Customers;
using HSS.Services.Localization;
using HSS.Services.Logging;
using HSS.Services.Security;
using HSS.Services.Seo;
using HSS.Services.Topics;
using HSS.Web.Framework.Mvc.Routes;
using HSS.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HSS.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(c =>
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            builder.RegisterAssemblyTypes(typeFinder.GetAssemblies().ToArray()).AsImplementedInterfaces();

            //data layer
            var config = ConfigurationManager.ConnectionStrings["hssEntities"];

            //builder.Register(c => new hssEntities(config.ConnectionString)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            
            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("zh_cache_static").SingleInstance();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();


            //store context
            builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();
            //services


            //builder.RegisterType<SearchTermService>().As<ISearchTermService>().InstancePerLifetimeScope();
            //builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<FulltextService>().As<IFulltextService>().InstancePerLifetimeScope();


            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();

            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());


            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();

            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            builder.RegisterType<FriendlyLinkService>().As<IFriendlyLinkService>().InstancePerLifetimeScope();            

            builder.RegisterType<UrlRecordService>().As<IUrlRecordService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            //builder.RegisterType<CustomerActivityService>().As<ICustomerActivityService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("zh_cache_static"))
            //    .InstancePerLifetimeScope();
            

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            

        }

        public int Order
        {
            get { return 0; }
        }
    }


    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>();
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }

}
