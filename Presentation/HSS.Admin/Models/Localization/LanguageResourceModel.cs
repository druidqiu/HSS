using System.Web.Mvc;
using FluentValidation.Attributes;
using HSS.Web.Framework.Mvc;
using HSS.Web.Framework;
using HSS.Admin.Validators.Localization;

namespace HSS.Admin.Models.Localization
{
    [Validator(typeof(LanguageResourceValidator))]
    public partial class LanguageResourceModel : BaseEntityModel
    {
        [ResourceDisplayName("Admin.Configuration.Languages.Resources.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [ResourceDisplayName("Admin.Configuration.Languages.Resources.Fields.Value")]
        [AllowHtml]
        public string Value { get; set; }

        [ResourceDisplayName("Admin.Configuration.Languages.Resources.Fields.LanguageName")]
        [AllowHtml]
        public string LanguageName { get; set; }

        public int LanguageId { get; set; }
    }
}