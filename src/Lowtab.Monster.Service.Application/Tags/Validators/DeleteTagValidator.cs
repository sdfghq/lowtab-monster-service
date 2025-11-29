using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Tags.Commands;

namespace Lowtab.Monster.Service.Application.Tags.Validators;

internal class DeleteTagValidator : NotNullRequestValidator<DeleteTagCommand>
{
    public DeleteTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
