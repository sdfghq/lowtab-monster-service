using FluentValidation;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Validators;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class UpdateArticleValidator : NotNullRequestValidator<UpdateArticleCommand>
{
    public UpdateArticleValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotNull().NotEmpty();
    }
}
