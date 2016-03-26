using HSS.Core.Infrastructure;
using HSS.Models.Configuration;
using System;
using System.Web.Mvc;

namespace HSS.Web.Framework.Security
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PublicAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        /// <summary>
        /// Anti-fogery security attribute
        /// </summary>
        /// <param name="ignore">Pass false in order to ignore this security validation</param>
        public PublicAntiForgeryAttribute(bool ignore = false)
        {
            this._ignore = ignore;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_ignore)
                return;
            
            if (filterContext.IsChildAction)
                return;
            
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return;
            
            var securitySettings = EngineContext.Current.Resolve<SecuritySettings>();
            if (!securitySettings.EnableXsrfProtectionForPublicStore)
                return;

            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}
