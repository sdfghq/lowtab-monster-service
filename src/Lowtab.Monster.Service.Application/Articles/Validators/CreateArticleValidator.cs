using FluentValidation;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Validators;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class CreateArticleValidator : NotNullRequestValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
    }
}
