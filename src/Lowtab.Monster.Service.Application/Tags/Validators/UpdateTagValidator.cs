using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Tags.Commands;

namespace Lowtab.Monster.Service.Application.Tags.Validators;

internal class UpdateTagValidator : NotNullRequestValidator<UpdateTagCommand>
{
    public UpdateTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
