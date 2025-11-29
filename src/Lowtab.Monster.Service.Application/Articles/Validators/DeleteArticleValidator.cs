using FluentValidation;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Validators;

namespace Lowtab.Monster.Service.Application.Articles.Validators;

internal class DeleteArticleValidator : NotNullRequestValidator<DeleteArticleCommand>
{
    public DeleteArticleValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
