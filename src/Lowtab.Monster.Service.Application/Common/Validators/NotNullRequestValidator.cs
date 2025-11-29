using FluentValidation;
using FluentValidation.Results;

namespace Lowtab.Monster.Service.Application.Common.Validators;

internal abstract class NotNullRequestValidator<T> : AbstractValidator<T>
{
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
    {
        if (context.InstanceToValidate != null)
        {
            return true;
        }

        context.AddFailure(string.Empty, "A non-null instance must be passed to the validator");
        return false;
    }
}
