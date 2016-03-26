using System.Web.Mvc;

namespace HSS.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}