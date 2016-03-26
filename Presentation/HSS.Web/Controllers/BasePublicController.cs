using HSS.Core.Infrastructure;
using HSS.Web.Framework.Contoller;
using System.Web.Mvc;
using System.Web.Routing;

namespace HSS.Web.Controllers
{
    //[StoreClosed]
    //[PublicStoreAllowNavigation]
    //[LanguageSeoCode]
    //[HttpsRequirement(SslRequirement.NoMatter)]
    //[WwwRequirement]
    public abstract partial class BasePublicController : BaseController
    {
        protected virtual ActionResult InvokeHttp404()
        {
            //调用目标控制器（错误页面）
            IController errorController = EngineContext.Current.Resolve<CommonController>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }

    }
}