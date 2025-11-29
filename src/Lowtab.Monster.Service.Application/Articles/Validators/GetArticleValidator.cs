using FluentValidation;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Application.Common.Validators;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class GetArticleValidator : NotNullRequestValidator<GetArticleQuery>
{
    public GetArticleValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
