using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.GroupTags.Commands;

namespace Lowtab.Monster.Service.Application.GroupTags.Validators;

internal class UpdateGroupTagValidator : NotNullRequestValidator<UpdateGroupTagCommand>
{
    public UpdateGroupTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}
