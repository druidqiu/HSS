
using HSS.Services.Logging;
using HSS.Web.Framework.Grid;
using System.Web.Mvc;
using System.Linq;
using HSS.Models.CommonEnum;
using HSS.Services.Localization;
using HSS.Core;

namespace HSS.Admin.Controllers
{
    public class LogController : BaseAdminController
    {
        private readonly ILogger _logService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        public LogController(ILogger logService,
            ILocalizationService localizationService,
            IWorkContext workContext)
        {
            this._logService = logService;
            this._localizationService = localizationService;
            this._workContext = workContext;
        }


        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var logs = _logService.GetAllLogs(pageIndex: command.Page - 1, pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = logs.Select(x => new
                {
                    Id = x.Id,
                    LogLevel = ((LogLevel)x.LogLevelId).GetLocalizedEnum(_localizationService, _workContext.WorkingLanguage.Id),
                    Source = ((LogSource)x.Source).GetLocalizedEnum(_localizationService, _workContext.WorkingLanguage.Id),
                    DateString = x.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                }),
                Total = logs.TotalCount,
            };
            return new JsonResult
            {
                Data = gridModel
            };
            
        }

    }

}