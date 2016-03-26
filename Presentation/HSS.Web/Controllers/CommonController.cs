using HSS.Models.Configuration;
using HSS.Web.Framework;
using System.Web.Mvc;

namespace HSS.Web.Controllers
{
    public class CommonController : BasePublicController
    {
        #region Fields
        private readonly CommonSettings _commonSettings;

        #endregion
        
        #region Constructors
        public CommonController(CommonSettings commonSettings)
        {
            this._commonSettings = commonSettings;
        }
        #endregion

        #region Method
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }
        
        public ActionResult PublishProduct() {
            return View();
        }

        #endregion
    }
}