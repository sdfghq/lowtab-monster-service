using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.Articles.Commands;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class DeleteArticleValidator : NotNullRequestValidator<DeleteArticleCommand>
{
    public DeleteArticleValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
