using HSS.Admin.Controllers;
using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Services.Logging;
using HSS.Web.Framework.Mvc.Routes;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HSS.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //注册用户路由
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "HSS.Web.Controllers" }
            );
        }


        protected void Application_Start()
        {
            EngineContext.Initialize();


            //注册MVC常规内容
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            try
            {
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("系统管理后台启动", HSS.Models.CommonEnum.LogSource.Admin, null, null);
            }
            catch (Exception)
            {
            }

        }


        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //日志错误
            LogException(exception);

            //HTTP404错误
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                if (!webHelper.IsStaticResource(this.Request))
                {
                    Response.Clear();
                    Server.ClearError();
                    Response.TrySkipIisCustomErrors = true;

                    //回调404控制器
                    IController errorController = EngineContext.Current.Resolve<CommonController>();

                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Common");
                    routeData.Values.Add("action", "PageNotFound");

                    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                }
            }
        }


        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            //忽略404错误
            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
                return;

            try
            {
                //日志
                var logger = EngineContext.Current.Resolve<ILogger>();
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                logger.Error(exc.Message, HSS.Models.CommonEnum.LogSource.Admin, exc, workContext.CurrentCustomer.Id);
            }
            catch (Exception)
            {
            }
        }
    }
}
