using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;

namespace Lowtab.Monster.Service.Application.GroupTags.Validators;

internal class GetGroupTagValidator : NotNullRequestValidator<GetGroupTagQuery>
{
    public GetGroupTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
