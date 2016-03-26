using HSS.Web.Framework.Localization;
using HSS.Web.Framework.Mvc.Routes;
using System.Web.Routing;

namespace HSS.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapLocalizedRoute("HomePage",
                            "",
                            new { controller = "Home", action = "Index" },
                            new[] { "HSS.Web.Controllers" });


            //login
            routes.MapLocalizedRoute("Login",
                            "login/",
                            new { controller = "Customer", action = "Login" },
                            new[] { "HSS.Web.Controllers" });
            //register
            routes.MapLocalizedRoute("Register",
                            "register/",
                            new { controller = "Customer", action = "Register" },
                            new[] { "HSS.Web.Controllers" });

            routes.MapLocalizedRoute("Logout",
                            "logout/",
                            new { controller = "Customer", action = "Logout" },
                            new[] { "HSS.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}