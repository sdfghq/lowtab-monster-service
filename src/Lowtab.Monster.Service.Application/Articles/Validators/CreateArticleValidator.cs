using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Articles.Commands;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class CreateArticleValidator : NotNullRequestValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
