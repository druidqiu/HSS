using HSS.Services.Topics;
using HSS.Web.Framework.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSS.Admin.Controllers
{
    public class TopicController : BaseAdminController
    {
        public readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            this._topicService = topicService;
        }

        public ActionResult Index()
        {
            return List();
        }

        public ActionResult List() {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var topics = _topicService.GetAllTopics(pageIndex: command.Page - 1, pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                //Data = languages.Select(x => x.ToModel()),
                Data = topics.Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsSystem = x.IsSystem,
                    SystemName = x.SystemName,

                }),
                Total = topics.TotalCount,
            };
            return Json(gridModel);
        }
    }
}