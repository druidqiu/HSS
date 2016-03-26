using System.Globalization;
using FluentValidation;
using HSS.Admin.Models.Localization;
using HSS.Services.Localization;
using HSS.Web.Framework.Validators;

namespace HSS.Admin.Validators.Localization
{
    public class LanguageValidator : BaseValidator<LanguageModel>
    {
        public LanguageValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Fields.Name.Required"));
            RuleFor(x => x.LanguageCulture)
                .Must(x =>
                          {
                              try
                              {
                                  var culture = new CultureInfo(x);
                                  return true;
                              }
                              catch
                              {
                                  return false;
                              }
                          })
                .WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Fields.LanguageCulture.Validation"));

            RuleFor(x => x.UniqueSeoCode).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Fields.UniqueSeoCode.Required"));
            RuleFor(x => x.UniqueSeoCode).Length(2).WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Fields.UniqueSeoCode.Length"));

        }
    }
}