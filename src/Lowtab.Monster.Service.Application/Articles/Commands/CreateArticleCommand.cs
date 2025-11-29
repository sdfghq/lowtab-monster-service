using Lowtab.Monster.Service.Contracts.Articles.CreateArticle;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

/// <inheritdoc cref="CreateArticleRequest" />
public record CreateArticleCommand : CreateArticleRequest, ICommand<CreateArticleResponse>;
