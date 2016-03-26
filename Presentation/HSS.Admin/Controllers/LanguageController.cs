using HSS.Admin.Models.Localization;
using HSS.Core;
using HSS.Core.Caching;
using HSS.Models;
using HSS.Services.Localization;
using HSS.Services.Security;
using HSS.Web.Framework;
using HSS.Web.Framework.Controllers;
using HSS.Web.Framework.Grid;
using HSS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZhengHe.Web.Extensions;

namespace HSS.Admin.Controllers
{

    public class LanguageController : BaseAdminController
    {

        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Constructors

        public LanguageController(ILanguageService languageService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._languageService = languageService;
            this._permissionService = permissionService;
            this._webHelper = webHelper;
        }


        #endregion

        #region Languages

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var languages = _languageService.GetAllLanguages(true);
            var gridModel = new DataSourceResult
            {
                Data = languages.Select(x => x.ToModel()),
                Total = languages.Count()
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var model = new LanguageModel();
            //默认值
            model.Published = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(LanguageModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var language = model.ToEntity();
                _languageService.InsertLanguage(language);

                
                return continueEditing ? RedirectToAction("Edit", new { id = language.Id }) : RedirectToAction("List");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var language = _languageService.GetLanguageById(id);
            if (language == null)
                return RedirectToAction("List");

            this.Server.ScriptTimeout = 300;

            var model = language.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(LanguageModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var language = _languageService.GetLanguageById(model.Id);
            if (language == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var allLanguages = _languageService.GetAllLanguages();
                if (allLanguages.Count == 1 && allLanguages[0].Id == language.Id &&
                    !model.Published)
                {
                    return RedirectToAction("Edit", new { id = language.Id });
                }

                //update
                language = model.ToEntity(language);
                _languageService.UpdateLanguage(language);

                
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = language.Id });
                }
                return RedirectToAction("List");
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var language = _languageService.GetLanguageById(id);
            if (language == null)
                return RedirectToAction("List");

            var allLanguages = _languageService.GetAllLanguages();
            if (allLanguages.Count == 1 && allLanguages[0].Id == language.Id)
            {
                return RedirectToAction("Edit", new { id = language.Id });
            }

            _languageService.DeleteLanguage(language);
            
            return RedirectToAction("List");
        }

        #endregion

        #region Resources

        public ActionResult Resources(int languageId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            //TODO do not use ViewBag, create a model
            ViewBag.AllLanguages = _languageService.GetAllLanguages(true)
                .Select(x => new SelectListItem
                {
                    Selected = (x.Id.Equals(languageId)),
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            var language = _languageService.GetLanguageById(languageId);
            ViewBag.LanguageId = languageId;
            ViewBag.LanguageName = language.Name;

            return View();
        }

        [HttpPost]
        //[AdminAntiForgery(true)]
        public ActionResult Resources(int languageId, DataSourceRequest command,
            Web.Framework.Grid.Filter filter = null, IEnumerable<Sort> sort = null)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var language = _languageService.GetLanguageById(languageId);

            var resources = _localizationService
                .GetAllResourceValues(languageId)
                .OrderBy(x => x.Key)
                .Select(x => new LanguageResourceModel
                {
                    LanguageId = languageId,
                    LanguageName = language.Name,
                    Id = x.Value.Key,
                    Name = x.Key,
                    Value = x.Value.Value,
                })
                    .AsQueryable()
                    .Filter(filter)
                    .Sort(sort);

            var gridModel = new DataSourceResult
            {
                Data = resources.PagedForCommand(command),
                Total = resources.Count()
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult ResourceUpdate(LanguageResourceModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            if (model.Name != null)
                model.Name = model.Name.Trim();
            if (model.Value != null)
                model.Value = model.Value.Trim();

            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            }

            var resource = _localizationService.GetLocaleStringResourceById(model.Id);

            if (!resource.ResourceName.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                var res = _localizationService.GetLocaleStringResourceByName(model.Name, model.LanguageId, false);
                if (res != null && res.Id != resource.Id)
                {
                    return Json(new DataSourceResult { Errors = string.Format(_localizationService.GetResource("Admin.Configuration.Languages.Resources.NameAlreadyExists"), res.ResourceName) });
                }
            }

            resource.ResourceName = model.Name;
            resource.ResourceValue = model.Value;
            _localizationService.UpdateLocaleStringResource(resource);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult ResourceAdd(int languageId, [Bind(Exclude = "Id")] LanguageResourceModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            if (model.Name != null)
                model.Name = model.Name.Trim();
            if (model.Value != null)
                model.Value = model.Value.Trim();

            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            }

            var res = _localizationService.GetLocaleStringResourceByName(model.Name, model.LanguageId, false);
            if (res == null)
            {
                var resource = new LocaleStringResource { LanguageId = languageId };
                resource.ResourceName = model.Name;
                resource.ResourceValue = model.Value;
                _localizationService.InsertLocaleStringResource(resource);
            }
            else
            {
                return Json(new DataSourceResult { Errors = string.Format(_localizationService.GetResource("Admin.Configuration.Languages.Resources.NameAlreadyExists"), model.Name) });
            }

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult ResourceDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var resource = _localizationService.GetLocaleStringResourceById(id);
            if (resource == null)
                throw new ArgumentException("No resource found with the specified id");
            _localizationService.DeleteLocaleStringResource(resource);

            return new NullJsonResult();
        }

        #endregion
    }
}