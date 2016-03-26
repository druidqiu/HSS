using FluentValidation;
using HSS.Models.Configuration;
using HSS.Services.Localization;
using HSS.Web.Framework.Validators;
using HSS.Web.Models.Customer;

namespace HSS.Web.Validators.Customer
{
    public class RegisterValidator : BaseValidator<RegisterModel>
    {
        public RegisterValidator(ILocalizationService localizationService,
            CustomerSettings customerSettings)
        {
            RuleFor(x => x.LoginName).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.LoginName.Required"));
            RuleFor(x => x.LoginName).Length(customerSettings.UsernameMinLength, customerSettings.UsernameMaxLenght).WithMessage(string.Format(localizationService.GetResource("Account.Fields.LoginName.LengthValidation"), customerSettings.UsernameMinLength, customerSettings.UsernameMaxLenght));

            RuleFor(x => x.Password).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Password.Required"));
            RuleFor(x => x.Password).Length(customerSettings.PasswordMinLength, 50).WithMessage(string.Format(localizationService.GetResource("Account.Fields.Password.LengthValidation"), customerSettings.PasswordMinLength));
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ConfirmPassword.Required"));
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(localizationService.GetResource("Account.Fields.Password.EnteredPasswordsDoNotMatch"));
            
            RuleFor(x=>x.Mobile).Length(11,11).WithMessage(string.Format(localizationService.GetResource("Account.Fields.Mobile.LengthValidation")));
        }
    }
}