using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.GroupTags.Commands;

namespace Lowtab.Monster.Service.Application.GroupTags.Validators;

internal class CreateGroupTagValidator : NotNullRequestValidator<CreateGroupTagCommand>
{
    public CreateGroupTagValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}
