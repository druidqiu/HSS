using FluentValidation;

namespace HSS.Web.Framework.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseValidator()
        {
            PostInitialize();
        }
        
        protected virtual void PostInitialize()
        {
            
        }
    }
}