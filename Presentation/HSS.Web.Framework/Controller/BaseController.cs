using System;
using System.IO;
using System.Web.Mvc;
using HSS.Core;
using HSS.Services.Logging;
using HSS.Core.Infrastructure;

namespace HSS.Web.Framework.Contoller
{
    /// <summary>
    /// 基础控制器
    /// </summary>

    [StoreIpAddress]
    [CustomerLastActivity]
    [StoreLastVisitedPage]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 渲染部分视图到字符串
        /// </summary>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }
        /// <summary>
        /// 渲染部分视图到字符串
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// 渲染部分视图到字符串
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// 渲染部分视图到字符串
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="model">实体</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }


        /// <summary>
        /// 日志异常
        /// </summary>
        /// <param name="exc">Exception</param>
        protected void LogException(Exception exc)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var logger = EngineContext.Current.Resolve<ILogger>();

            var customer = workContext.CurrentCustomer;

            logger.Error(exc.Message, workContext.Source.Value, exc, customer.Id);
        }
        
    }
}
