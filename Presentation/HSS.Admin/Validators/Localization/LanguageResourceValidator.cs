using FluentValidation;
using HSS.Admin.Models.Localization;
using HSS.Services.Localization;
using HSS.Web.Framework.Validators;

namespace HSS.Admin.Validators.Localization
{
    public class LanguageResourceValidator : BaseValidator<LanguageResourceModel>
    {
        public LanguageResourceValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Resources.Fields.Name.Required"));
            RuleFor(x => x.Value).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Resources.Fields.Value.Required"));
        }
    }
}