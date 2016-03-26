using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Models;
using HSS.Models.Configuration;
using HSS.Models.SystemNames;
using HSS.Services.Common;
using System;
using System.Web.Mvc;

namespace HSS.Web.Framework
{
    public class StoreLastVisitedPageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;
            
            if (filterContext.IsChildAction)
                return;
            
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var customerSettings = EngineContext.Current.Resolve<CustomerSettings>();
            if (!customerSettings.StoreLastVisitedPage)
                return;

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var pageUrl = webHelper.GetThisPageUrl(true);
            if (!String.IsNullOrEmpty(pageUrl))
            {
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();

                var entity = new BaseEntity
                {
                    Id = workContext.CurrentCustomer.Id,
                    type = workContext.CurrentCustomer.GetType(),
                };

                var previousPageUrl = entity.GetAttribute<string>(SystemCustomerAttributeNames.LastVisitedPage);
                if (!pageUrl.Equals(previousPageUrl) && entity.Id > 0)
                {
                    genericAttributeService.SaveAttribute(entity, SystemCustomerAttributeNames.LastVisitedPage, pageUrl);
                }
            }
        }
    }
}
