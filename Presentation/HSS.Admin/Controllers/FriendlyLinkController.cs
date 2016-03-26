using HSS.Admin.Models.Seo;
using HSS.Services.Seo;
using HSS.Web.Framework.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSS.Admin.Controllers
{
    public class FriendlyLinkController : BaseAdminController
    {
        public readonly IFriendlyLinkService _linkService;

        public FriendlyLinkController(IFriendlyLinkService linkService)
        {
            this._linkService = linkService;
        }

        public ActionResult Index()
        {
            return List();
        }

        public ActionResult List()
        {
            var model = new FriendlyLinkListModel();
            
            model.AvaiListTarget.Add(new SelectListItem
            {
                Selected = true,
                Text = "请选择打开类型",
                Value = null,
            });

            model.AvaiListTarget.Add(new SelectListItem
            {
                Selected = false,
                Text = "Target",
                Value = bool.TrueString,
            });

            model.AvaiListTarget.Add(new SelectListItem
            {
                Selected = true,
                Text = "None",
                Value = bool.FalseString,
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, FriendlyLinkListModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
            //    return AccessDeniedView();

            var languages = _linkService.GetAllLinks(name: model.Keywords,
                                                        showHidden:true,
                                                        pageIndex: command.Page - 1,
                                                        pageSize: command.PageSize); //_languageService.GetAllLanguages(true);
            var gridModel = new DataSourceResult
            {
                //Data = languages.Select(x => x.ToModel()),
                Data = languages.Select(x => new FriendlyLinkModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Link = x.Link,
                    
                }),
                Total = languages.TotalCount,
            };
            return Json(gridModel);
        }
    }
}