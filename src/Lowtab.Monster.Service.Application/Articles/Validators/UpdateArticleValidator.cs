using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Articles.Commands;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class UpdateArticleValidator : NotNullRequestValidator<UpdateArticleCommand>
{
    public UpdateArticleValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
