using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Services.Customers;
using System;
using System.Web.Mvc;

namespace HSS.Web.Framework
{
    /// <summary>
    /// 用户最后活动日期记录
    /// </summary>
    public class CustomerLastActivityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;

            //过滤子方法
            if (filterContext.IsChildAction)
                return;

            //仅仅记录GET请求
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var customer = workContext.CurrentCustomer;

            //更新最后活动日期
            if (customer.LastActivityDate.AddMinutes(1.0) < DateTime.Now)
            {
                var customerService = EngineContext.Current.Resolve<ICustomerService>();
                customer.LastActivityDate = DateTime.Now;
                customerService.UpdateCustomer(customer);
            }
        }
    }
}
