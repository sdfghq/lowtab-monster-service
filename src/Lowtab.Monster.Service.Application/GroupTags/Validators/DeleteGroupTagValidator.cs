using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.GroupTags.Commands;

namespace Lowtab.Monster.Service.Application.GroupTags.Validators;

internal class DeleteGroupTagValidator : NotNullRequestValidator<DeleteGroupTagCommand>
{
    public DeleteGroupTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
