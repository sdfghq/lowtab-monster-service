using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Tags.Queryes;

namespace Lowtab.Monster.Service.Application.Tags.Validators;

internal class GetTagValidator : NotNullRequestValidator<GetTagQuery>
{
    public GetTagValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
