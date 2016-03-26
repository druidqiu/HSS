using System.Web.Mvc;

namespace HSS.Web.Controllers
{
    public class TopicController : Controller
    {
        // GET: Topic
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopupTopic(string seName) {
            //var model =new model();
            // return PartialView(model);
            return PartialView();
        }

        public ActionResult BlockTopic(string seName)
        {
            //var model =new model();
            // return PartialView(model);
            return PartialView();
        }
    }
}